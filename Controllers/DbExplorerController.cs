using System.Data.Common;
using Bfn.DevOps.Data;
using Bfn.DevOps.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bfn.DevOps.Controllers;

[Authorize(Roles = DatabaseSeeder.AdministratorRole)]
public sealed class DbExplorerController(ApplicationDbContext db) : Controller
{
    private const int PageSize = 50;

    public async Task<IActionResult> Index(string? table, int page = 1)
    {
        var connection = db.Database.GetDbConnection();
        if (connection.State != System.Data.ConnectionState.Open) await connection.OpenAsync();

        var tables = await GetTablesAsync(connection);
        var selected = table is not null ? tables.FirstOrDefault(x => string.Equals(x.Name, table, StringComparison.OrdinalIgnoreCase))?.Name : null;
        selected ??= tables.FirstOrDefault()?.Name;

        var model = new DbExplorerViewModel { Tables = tables, SelectedTable = selected, Page = Math.Max(1, page), PageSize = PageSize };
        if (selected is not null)
        {
            model.TotalRowCount = tables.First(x => x.Name == selected).RowCount;
            var offset = (model.Page - 1) * PageSize;
            using var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM \"{selected}\" LIMIT {PageSize} OFFSET {offset}";
            using var reader = await command.ExecuteReaderAsync();
            for (var i = 0; i < reader.FieldCount; i++) model.Columns.Add(reader.GetName(i));
            while (await reader.ReadAsync())
            {
                var row = new object?[reader.FieldCount];
                reader.GetValues(row!);
                for (var i = 0; i < row.Length; i++) if (row[i] is DBNull) row[i] = null;
                model.Rows.Add(row);
            }
        }
        return View(model);
    }

    private static async Task<List<DbTableInfo>> GetTablesAsync(DbConnection connection)
    {
        var names = new List<string>();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%' ORDER BY name";
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync()) names.Add(reader.GetString(0));
        }
        var tables = new List<DbTableInfo>();
        foreach (var name in names)
        {
            using var command = connection.CreateCommand();
            command.CommandText = $"SELECT COUNT(*) FROM \"{name}\"";
            var count = Convert.ToInt64(await command.ExecuteScalarAsync() ?? 0L);
            tables.Add(new DbTableInfo(name, count));
        }
        return tables;
    }
}
