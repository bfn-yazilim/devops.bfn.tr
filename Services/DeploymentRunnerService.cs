using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Bfn.DevOps.Data;
using Bfn.DevOps.Models;
using Microsoft.EntityFrameworkCore;

namespace Bfn.DevOps.Services;

public interface IDeploymentRunner
{
    void QueueRun(int deploymentRunId);
}

public sealed class DeploymentRunnerService(IServiceScopeFactory scopeFactory, ILogger<DeploymentRunnerService> logger) : IDeploymentRunner
{
    public void QueueRun(int deploymentRunId) => _ = Task.Run(() => ExecuteAsync(deploymentRunId));

    private async Task ExecuteAsync(int runId)
    {
        using var scope = scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var iis = scope.ServiceProvider.GetRequiredService<IIisService>();
        var run = await db.DeploymentRuns.Include(x => x.Project).FirstOrDefaultAsync(x => x.Id == runId);
        if (run is null) return;

        var steps = await db.DeploymentSteps.Include(x => x.StepType)
            .Where(x => x.ProjectId == run.ProjectId && x.IsEnabled).OrderBy(x => x.SortOrder).ToListAsync();

        var stepRuns = steps.Select(s => new DeploymentRunStep { DeploymentRunId = run.Id, DeploymentStepId = s.Id, Status = "Pending" }).ToList();
        db.DeploymentRunSteps.AddRange(stepRuns);
        await db.SaveChangesAsync();

        var log = new StringBuilder();
        void Append(string line) { log.AppendLine($"[{DateTime.UtcNow:HH:mm:ss}] {line}"); }

        Append($"Deployment başladı: {run.Project.Name}");
        if (steps.Count == 0) Append("Uyarı: Aktif adım tanımlanmamış.");
        var success = true;
        foreach (var step in steps)
        {
            var stepRun = stepRuns.Single(x => x.DeploymentStepId == step.Id);
            if (!success) { stepRun.Status = "Skipped"; continue; }
            stepRun.Status = "Running"; stepRun.StartedAtUtc = DateTime.UtcNow;
            await db.SaveChangesAsync();
            Append($"-> {step.Name} ({step.StepType.Name})");
            try
            {
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(step.TimeoutSeconds));
                await ExecuteStepAsync(step, run.Project, iis, Append, cts.Token);
                Append("   Tamamlandı.");
                stepRun.Status = "Succeeded";
            }
            catch (Exception ex)
            {
                Append($"   HATA: {ex.Message}");
                logger.LogError(ex, "Deployment step failed: {Step}", step.Name);
                stepRun.Status = "Failed";
                if (!step.ContinueOnError) success = false;
            }
            stepRun.FinishedAtUtc = DateTime.UtcNow;
            run.Log = log.ToString();
            await db.SaveChangesAsync();
        }
        run.Status = success ? "Succeeded" : "Failed";
        run.FinishedAtUtc = DateTime.UtcNow;
        run.Log = log.ToString();
        await db.SaveChangesAsync();
    }

    private static async Task ExecuteStepAsync(DeploymentStep step, DevOpsProject project, IIisService iis, Action<string> log, CancellationToken ct)
    {
        JsonElement settings;
        try { settings = JsonDocument.Parse(string.IsNullOrWhiteSpace(step.SettingsJson) ? "{}" : step.SettingsJson).RootElement; }
        catch (JsonException) { settings = JsonDocument.Parse("{}").RootElement; }

        string Get(string name, string fallback = "") => settings.TryGetProperty(name, out var v) && v.ValueKind == JsonValueKind.String ? v.GetString() ?? fallback : fallback;
        var workDir = string.IsNullOrWhiteSpace(project.WorkingDirectory) ? Directory.GetCurrentDirectory() : project.WorkingDirectory;

        switch (step.StepType.Code)
        {
            case "GitClone":
                var git = JsonSerializer.Deserialize<GitCloneSettings>(step.SettingsJson) ?? new GitCloneSettings();
                if (string.IsNullOrWhiteSpace(git.RepositoryUrl)) throw new InvalidOperationException("Git repository URL belirtilmemiş.");
                var url = git.RepositoryUrl;
                if (!string.IsNullOrWhiteSpace(git.Username))
                {
                    var uri = new Uri(url);
                    url = $"{uri.Scheme}://{Uri.EscapeDataString(git.Username)}:{Uri.EscapeDataString(git.Password)}@{uri.Host}{uri.PathAndQuery}";
                }
                Directory.CreateDirectory(workDir);
                var branchArgs = string.IsNullOrWhiteSpace(git.Branch) ? "" : $" --branch \"{git.Branch}\"";
                await RunProcessAsync("git", $"clone{branchArgs} \"{url}\" .", workDir, log, ct);
                break;
            case "NpmInstall":
                await RunProcessAsync("npm", "install", workDir, log, ct);
                break;
            case "DotnetPublish":
                var outputDir = Get("output", Path.Combine(workDir, "publish"));
                await RunProcessAsync("dotnet", $"publish -c Release -o \"{outputDir}\"", workDir, log, ct);
                break;
            case "FileCopy":
                File.Copy(ResolvePath(workDir, Get("source")), ResolvePath(workDir, Get("destination")), overwrite: true);
                break;
            case "FileDelete":
                var target = ResolvePath(workDir, Get("path"));
                if (File.Exists(target)) File.Delete(target);
                break;
            case "FileCopyAll":
                CopyDirectory(ResolvePath(workDir, Get("source")), ResolvePath(workDir, Get("destination")));
                break;
            case "FileDeleteAll":
                var dir = ResolvePath(workDir, Get("path"));
                if (Directory.Exists(dir)) Directory.Delete(dir, recursive: true);
                break;
            case "IisStop": iis.StopSite(project.IisSiteName); break;
            case "IisStart": iis.StartSite(project.IisSiteName); break;
            case "AppPoolStop": iis.StopAppPool(project.ApplicationPoolName); break;
            case "AppPoolStart": iis.StartAppPool(project.ApplicationPoolName); break;
            case "IisChangeDirectory":
                var newPath = Get("path", workDir);
                iis.ChangeSitePhysicalPath(project.IisSiteName, newPath);
                break;
            case "HealthCheck":
                var healthUrl = Get("url", project.Url ?? "");
                if (string.IsNullOrWhiteSpace(healthUrl)) throw new InvalidOperationException("Health check URL belirtilmemiş.");
                using (var http = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
                {
                    var response = await http.GetAsync(healthUrl, ct);
                    if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Health check başarısız: {(int)response.StatusCode}");
                }
                break;
            case "PowerShell":
                var script = Get("script");
                if (string.IsNullOrWhiteSpace(script)) throw new InvalidOperationException("PowerShell script belirtilmemiş.");
                await RunProcessAsync("powershell", $"-NoProfile -NonInteractive -Command \"{script.Replace("\"", "\\\"")}\"", workDir, log, ct);
                break;
            default:
                throw new NotSupportedException($"Desteklenmeyen adım türü: {step.StepType.Code}");
        }
    }

    private static string ResolvePath(string workDir, string path) => Path.IsPathRooted(path) ? path : Path.Combine(workDir, path);

    private static void CopyDirectory(string source, string destination)
    {
        Directory.CreateDirectory(destination);
        foreach (var file in Directory.GetFiles(source)) File.Copy(file, Path.Combine(destination, Path.GetFileName(file)), overwrite: true);
        foreach (var subDir in Directory.GetDirectories(source)) CopyDirectory(subDir, Path.Combine(destination, Path.GetFileName(subDir)));
    }

    private static async Task RunProcessAsync(string fileName, string arguments, string workingDirectory, Action<string> log, CancellationToken ct)
    {
        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                WorkingDirectory = workingDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };
        process.OutputDataReceived += (_, e) => { if (e.Data is not null) log($"   {e.Data}"); };
        process.ErrorDataReceived += (_, e) => { if (e.Data is not null) log($"   {e.Data}"); };
        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        try
        {
            await process.WaitForExitAsync(ct);
        }
        catch (OperationCanceledException)
        {
            try { process.Kill(entireProcessTree: true); } catch { /* best effort */ }
            throw new TimeoutException($"'{fileName}' zaman aşımına uğradı.");
        }
        if (process.ExitCode != 0) throw new InvalidOperationException($"'{fileName}' çıkış kodu {process.ExitCode} ile sonlandı.");
    }
}
