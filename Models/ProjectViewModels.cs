using System.ComponentModel.DataAnnotations;

namespace Bfn.DevOps.Models;

public sealed class ProjectEditViewModel
{
    public int Id { get; set; }
    public Guid UId { get; set; }
    [Required, MaxLength(100), Display(Name = "Proje adı")] public string Name { get; set; } = "";
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
    [Display(Name = "Adım türü")] public DeploymentStepType Type { get; set; }
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
}

public sealed class BoardCardCommentViewModel
{
    public int BoardCardId { get; set; }
    [Required, MaxLength(4000)] public string Body { get; set; } = "";
}
