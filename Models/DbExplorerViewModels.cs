namespace Bfn.DevOps.Models;

public sealed class DbExplorerViewModel
{
    public List<DbTableInfo> Tables { get; set; } = [];
    public string? SelectedTable { get; set; }
    public List<string> Columns { get; set; } = [];
    public List<object?[]> Rows { get; set; } = [];
    public long TotalRowCount { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 50;
    public int PageCount => TotalRowCount == 0 ? 1 : (int)Math.Ceiling(TotalRowCount / (double)PageSize);
}

public sealed record DbTableInfo(string Name, long RowCount);
