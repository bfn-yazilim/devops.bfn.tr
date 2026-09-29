using System.ComponentModel.DataAnnotations;

namespace Bfn.DevOps.Models;

public sealed class DevOpsProject
{
    public int Id { get; set; }
    [Required, MaxLength(100)] public string Name { get; set; } = "";
    [MaxLength(200)] public string? Url { get; set; }
    [MaxLength(100)] public string IisSiteName { get; set; } = "";
    [MaxLength(100)] public string ApplicationPoolName { get; set; } = "";
    [MaxLength(500)] public string? RepositoryUrl { get; set; }
    [MaxLength(500)] public string? WorkingDirectory { get; set; }
    public bool ZeroDowntimeEnabled { get; set; } = true;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;
    public List<DeploymentStep> DeploymentSteps { get; set; } = [];
}

public enum DeploymentStepType
{
    GitClone, NpmInstall, DotnetPublish, FileCopy, FileDelete, FileCopyAll,
    FileDeleteAll, IisStop, IisStart, IisChangeDirectory, AppPoolStart, AppPoolStop,
    HealthCheck, PowerShell
}

public sealed class DeploymentStep
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public DevOpsProject Project { get; set; } = null!;
    [Required, MaxLength(120)] public string Name { get; set; } = "";
    public DeploymentStepType Type { get; set; }
    public int SortOrder { get; set; }
    public bool IsEnabled { get; set; } = true;
    public bool ContinueOnError { get; set; }
    public int TimeoutSeconds { get; set; } = 600;
    public string SettingsJson { get; set; } = "{}";
}

public sealed class Board
{
    public int Id { get; set; }
    [Required, MaxLength(100)] public string Name { get; set; } = "Genel";
    public int SortOrder { get; set; }
    public List<BoardColumn> Columns { get; set; } = [];
}

public sealed class BoardColumn
{
    public int Id { get; set; }
    public int BoardId { get; set; }
    public Board Board { get; set; } = null!;
    [Required, MaxLength(80)] public string Name { get; set; } = "";
    public int SortOrder { get; set; }
    public List<BoardCard> Cards { get; set; } = [];
}

public enum CardPriority { Unspecified, Low, Medium, High, Urgent }

public sealed class BoardCard
{
    public int Id { get; set; }
    public int BoardColumnId { get; set; }
    public BoardColumn Column { get; set; } = null!;
    [Required, MaxLength(200)] public string Title { get; set; } = "";
    [MaxLength(4000)] public string? Description { get; set; }
    public int SortOrder { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;
    [MaxLength(450)] public string? AssignedAgentId { get; set; }
    [MaxLength(450)] public string? AssignedUserId { get; set; }
    public ApplicationUser? AssignedUser { get; set; }
    [MaxLength(450)] public string? CreatedByUserId { get; set; }
    public ApplicationUser? CreatedByUser { get; set; }
    public CardPriority Priority { get; set; } = CardPriority.Unspecified;
    public DateTime? DueAtUtc { get; set; }
    [MaxLength(300)] public string? Tags { get; set; }
    public bool IsArchived { get; set; }
    public List<BoardCardComment> Comments { get; set; } = [];
}

public sealed class BoardCardComment
{
    public int Id { get; set; }
    public int BoardCardId { get; set; }
    public BoardCard Card { get; set; } = null!;
    [MaxLength(450)] public string? AuthorUserId { get; set; }
    public ApplicationUser? Author { get; set; }
    [Required, MaxLength(4000)] public string Body { get; set; } = "";
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}

public sealed class DeploymentRun
{
    public long Id { get; set; }
    public int ProjectId { get; set; }
    public DevOpsProject Project { get; set; } = null!;
    [MaxLength(40)] public string Status { get; set; } = "Queued";
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? StartedAtUtc { get; set; }
    public DateTime? FinishedAtUtc { get; set; }
    [MaxLength(450)] public string RequestedByUserId { get; set; } = "";
    public string Log { get; set; } = "";
}
