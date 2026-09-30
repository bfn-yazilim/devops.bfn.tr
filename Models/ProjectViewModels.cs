using System.ComponentModel.DataAnnotations;

namespace Bfn.DevOps.Models;

public static class ProjectEnvironmentDisplay
{
    public static string Label(ProjectEnvironment env) => env switch
    {
        ProjectEnvironment.Dev => "Dev",
        ProjectEnvironment.Test => "Test",
        ProjectEnvironment.PreTest => "Pre-Test",
        ProjectEnvironment.PreProd => "Pre-Prod",
        ProjectEnvironment.Prod => "Prod",
        _ => env.ToString()
    };

    public static string BadgeClass(ProjectEnvironment env) => env switch
    {
        ProjectEnvironment.Dev => "badge-ghost",
        ProjectEnvironment.Test => "badge-info",
        ProjectEnvironment.PreTest => "badge-secondary",
        ProjectEnvironment.PreProd => "badge-warning",
        ProjectEnvironment.Prod => "badge-error",
        _ => "badge-ghost"
    };
}

public sealed class ProjectListItemViewModel
{
    public DevOpsProject Project { get; set; } = null!;
    public DeploymentRun? LatestRun { get; set; }
}

public sealed class StepsPageViewModel
{
    public DevOpsProject Project { get; set; } = null!;
    public bool IsRunning { get; set; }
    public Dictionary<int, string> StepStatuses { get; set; } = [];
}

public sealed class DeploymentDetailViewModel
{
    public DevOpsProject Project { get; set; } = null!;
    public List<DeploymentStep> Steps { get; set; } = [];
    public DeploymentRun? LatestRun { get; set; }
    public Dictionary<int, string> StepStatuses { get; set; } = [];
    public bool IsRunning => LatestRun?.Status == "Running";
}

public sealed class ProjectEditViewModel
{
    public int Id { get; set; }
    public Guid UId { get; set; }
    [Required, MaxLength(100), Display(Name = "Proje adı")] public string Name { get; set; } = "";
    [Display(Name = "Ortam")] public ProjectEnvironment Environment { get; set; } = ProjectEnvironment.Dev;
    [MaxLength(200), Display(Name = "URL / host adı")] public string? Url { get; set; }
    [Required, MaxLength(100), Display(Name = "IIS site adı")] public string IisSiteName { get; set; } = "";
    [Required, MaxLength(100), Display(Name = "Application Pool adı")] public string ApplicationPoolName { get; set; } = "";
    [Url, MaxLength(500), Display(Name = "Git repository URL")] public string? RepositoryUrl { get; set; }
    [MaxLength(500), Display(Name = "Çalışma dizini")] public string? WorkingDirectory { get; set; }
    [Display(Name = "Kesintisiz güncelleme kullan")] public bool ZeroDowntimeEnabled { get; set; } = true;
}

public sealed class DeploymentStepEditViewModel
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public Guid ProjectUId { get; set; }
    [Required, MaxLength(120), Display(Name = "Adım adı")] public string Name { get; set; } = "";
    [Display(Name = "Adım türü")] public int StepTypeId { get; set; }
    public string StepTypeCode { get; set; } = "";
    public List<StepType> StepTypes { get; set; } = [];
    [Range(5, 7200), Display(Name = "Zaman aşımı (sn)")] public int TimeoutSeconds { get; set; } = 600;
    [Display(Name = "Aktif")] public bool IsEnabled { get; set; } = true;
    [Display(Name = "Hatada devam et")] public bool ContinueOnError { get; set; }
    [Display(Name = "Ayarlar (JSON)")] public string SettingsJson { get; set; } = "{}";
    [MaxLength(500), Display(Name = "Git repository URL")] public string? GitRepositoryUrl { get; set; }
    [MaxLength(200), Display(Name = "Kullanıcı adı")] public string? GitUsername { get; set; }
    [MaxLength(500), Display(Name = "Şifre / Personal Access Token")] public string? GitPassword { get; set; }
    [MaxLength(200), Display(Name = "Branch")] public string? GitBranch { get; set; }
}

public sealed class GitCloneSettings
{
    public string RepositoryUrl { get; set; } = "";
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string Branch { get; set; } = "";
}

public sealed class BoardCardUpdateViewModel
{
    public int Id { get; set; }
    public int BoardColumnId { get; set; }
    [Required, MaxLength(200), Display(Name = "Başlık")] public string Title { get; set; } = "";
    [MaxLength(4000), Display(Name = "Açıklama")] public string? Description { get; set; }
    [Display(Name = "Atanan")] public string? AssignedUserId { get; set; }
    [Display(Name = "Öncelik")] public CardPriority Priority { get; set; }
    [Display(Name = "Son tarih")] public DateTime? DueAtUtc { get; set; }
    [MaxLength(300), Display(Name = "Etiketler")] public string? Tags { get; set; }
    [MaxLength(500), Display(Name = "Bağlantı / Dosya URL")] public string? AttachmentUrl { get; set; }
    [MaxLength(200), Display(Name = "Bağlantı etiketi")] public string? AttachmentLabel { get; set; }
}

public sealed class BoardCardCommentViewModel
{
    public int BoardCardId { get; set; }
    [Required, MaxLength(4000)] public string Body { get; set; } = "";
}
