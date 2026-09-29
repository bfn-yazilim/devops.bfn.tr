using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfn.DevOps.Migrations
{
    /// <inheritdoc />
    public partial class ArchiveBoardCards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "BoardCards",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "BoardCards");
        }
    }
}
