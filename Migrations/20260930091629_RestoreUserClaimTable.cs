using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfn.DevOps.Migrations
{
    /// <inheritdoc />
    public partial class RestoreUserClaimTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SyUserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyUserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SyUserClaim_SyUser_UserId",
                        column: x => x.UserId,
                        principalTable: "SyUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SyUserClaim_UserId",
                table: "SyUserClaim",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SyUserClaim");
        }
    }
}
