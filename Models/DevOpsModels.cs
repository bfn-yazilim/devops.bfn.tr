using System.ComponentModel.DataAnnotations;

namespace Bfn.DevOps.Models;

public interface IAuditable
{
    Guid UId { get; set; }
    string? CreUser { get; set; }
    DateTime CreDate { get; set; }
    string? ModUser { get; set; }
    DateTime? ModDate { get; set; }
    string? DelUser { get; set; }
    DateTime? DelDate { get; set; }
    string? Client { get; set; }
    string? ClientIp { get; set; }
    bool IsDeleted { get; set; }
}

public abstract class AuditableEntity : IAuditable
{
    public Guid UId { get; set; } = Guid.NewGuid();
    [MaxLength(450)] public string? CreUser { get; set; }
    public DateTime CreDate { get; set; } = DateTime.UtcNow;
    [MaxLength(450)] public string? ModUser { get; set; }
    public DateTime? ModDate { get; set; }
    [MaxLength(450)] public string? DelUser { get; set; }
    public DateTime? DelDate { get; set; }
    [MaxLength(50)] public string? Client { get; set; }
    [MaxLength(50)] public string? ClientIp { get; set; }
    public bool IsDeleted { get; set; }
}

public enum ProjectEnvironment { Dev, Test, PreTest, PreProd, Prod }

public sealed class DevOpsProject : AuditableEntity
{
    public int Id { get; set; }
    [Required, MaxLength(100)] public string Name { get; set; } = "";
    public ProjectEnvironment Environment { get; set; } = ProjectEnvironment.Dev;
    [MaxLength(200)] public string? Url { get; set; }
    [MaxLength(100)] public string IisSiteName { get; set; } = "";
    [MaxLength(100)] public string ApplicationPoolName { get; set; } = "";
    [MaxLength(500)] public string? RepositoryUrl { get; set; }
    [MaxLength(500)] public string? WorkingDirectory { get; set; }
    public bool ZeroDowntimeEnabled { get; set; } = true;
    public List<DeploymentStep> DeploymentSteps { get; set; } = [];
}

public sealed class StepType : AuditableEntity
{
    public int Id { get; set; }
    [Required, MaxLength(50)] public string Code { get; set; } = "";
    [Required, MaxLength(100)] public string Name { get; set; } = "";
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
}

public sealed class DeploymentStep : AuditableEntity
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public DevOpsProject Project { get; set; } = null!;
    [Required, MaxLength(120)] public string Name { get; set; } = "";
    public int StepTypeId { get; set; }
    public StepType StepType { get; set; } = null!;
    public int SortOrder { get; set; }
    public bool IsEnabled { get; set; } = true;
    public bool ContinueOnError { get; set; }
    public int TimeoutSeconds { get; set; } = 600;
    public string SettingsJson { get; set; } = "{}";
}

public sealed class Board : AuditableEntity
{
    public int Id { get; set; }
    [Required, MaxLength(100)] public string Name { get; set; } = "Genel";
    public int SortOrder { get; set; }
    public List<BoardColumn> Columns { get; set; } = [];
}

public sealed class BoardColumn : AuditableEntity
{
    public int Id { get; set; }
    public int BoardId { get; set; }
    public Board Board { get; set; } = null!;
    [Required, MaxLength(80)] public string Name { get; set; } = "";
    public int SortOrder { get; set; }
    public List<BoardCard> Cards { get; set; } = [];
}

public enum CardPriority { Unspecified, Low, Medium, High, Urgent }

public sealed class BoardCard : AuditableEntity
{
    public int Id { get; set; }
    public int BoardColumnId { get; set; }
    public BoardColumn Column { get; set; } = null!;
    [Required, MaxLength(200)] public string Title { get; set; } = "";
    [MaxLength(4000)] public string? Description { get; set; }
    public int SortOrder { get; set; }
    [MaxLength(450)] public string? AssignedAgentId { get; set; }
    [MaxLength(450)] public string? AssignedUserId { get; set; }
    public ApplicationUser? AssignedUser { get; set; }
    [MaxLength(450)] public string? CreatedByUserId { get; set; }
    public ApplicationUser? CreatedByUser { get; set; }
    public CardPriority Priority { get; set; } = CardPriority.Unspecified;
    public DateTime? DueAtUtc { get; set; }
    [MaxLength(300)] public string? Tags { get; set; }
    [MaxLength(500)] public string? AttachmentUrl { get; set; }
    [MaxLength(200)] public string? AttachmentLabel { get; set; }
    public List<BoardCardComment> Comments { get; set; } = [];
    public List<BoardCardSubtask> Subtasks { get; set; } = [];
}

public sealed class BoardCardSubtask : AuditableEntity
{
    public int Id { get; set; }
    public int BoardCardId { get; set; }
    public BoardCard Card { get; set; } = null!;
    [Required, MaxLength(200)] public string Title { get; set; } = "";
    public bool IsDone { get; set; }
    public int SortOrder { get; set; }
}

public sealed class BoardCardComment : AuditableEntity
{
    public int Id { get; set; }
    public int BoardCardId { get; set; }
    public BoardCard Card { get; set; } = null!;
    [MaxLength(450)] public string? AuthorUserId { get; set; }
    public ApplicationUser? Author { get; set; }
    [Required, MaxLength(4000)] public string Body { get; set; } = "";
}

public sealed class DeploymentRun : AuditableEntity
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public DevOpsProject Project { get; set; } = null!;
    [MaxLength(40)] public string Status { get; set; } = "Queued";
    public DateTime? StartedAtUtc { get; set; }
    public DateTime? FinishedAtUtc { get; set; }
    [MaxLength(450)] public string RequestedByUserId { get; set; } = "";
    public string Log { get; set; } = "";
    public List<DeploymentRunStep> StepRuns { get; set; } = [];
}

public sealed class DeploymentRunStep : AuditableEntity
{
    public int Id { get; set; }
    public int DeploymentRunId { get; set; }
    public DeploymentRun Run { get; set; } = null!;
    public int DeploymentStepId { get; set; }
    public DeploymentStep Step { get; set; } = null!;
    [MaxLength(40)] public string Status { get; set; } = "Pending";
    public DateTime? StartedAtUtc { get; set; }
    public DateTime? FinishedAtUtc { get; set; }
}
