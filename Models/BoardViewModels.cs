namespace Bfn.DevOps.Models;

public sealed class BoardCardDetailViewModel
{
    public BoardCard Card { get; set; } = null!;
    public List<BoardColumn> Columns { get; set; } = [];
    public List<ApplicationUser> Users { get; set; } = [];
}

public static class CardPriorityDisplay
{
    public static string Label(CardPriority priority) => priority switch
    {
        CardPriority.Low => "Düşük",
        CardPriority.Medium => "Orta",
        CardPriority.High => "Yüksek",
        CardPriority.Urgent => "Acil",
        _ => "Belirtilmemiş"
    };

    public static string BadgeClass(CardPriority priority) => priority switch
    {
        CardPriority.Low => "badge-info",
        CardPriority.Medium => "badge-warning",
        CardPriority.High => "badge-error",
        CardPriority.Urgent => "badge-error badge-outline",
        _ => "badge-ghost"
    };
}
