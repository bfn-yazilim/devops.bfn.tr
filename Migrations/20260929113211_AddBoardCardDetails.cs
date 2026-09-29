using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfn.DevOps.Migrations
{
    /// <inheritdoc />
    public partial class AddBoardCardDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedUserId",
                table: "BoardCards",
                type: "TEXT",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "BoardCards",
                type: "TEXT",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueAtUtc",
                table: "BoardCards",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "BoardCards",
                type: "TEXT",
                nullable: false,
                defaultValue: "Unspecified");

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "BoardCards",
                type: "TEXT",
                maxLength: 300,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BoardCardComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BoardCardId = table.Column<int>(type: "INTEGER", nullable: false),
                    AuthorUserId = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    Body = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardCardComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardCardComments_AspNetUsers_AuthorUserId",
                        column: x => x.AuthorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_BoardCardComments_BoardCards_BoardCardId",
                        column: x => x.BoardCardId,
                        principalTable: "BoardCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardCards_AssignedUserId",
                table: "BoardCards",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardCards_CreatedByUserId",
                table: "BoardCards",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardCardComments_AuthorUserId",
                table: "BoardCardComments",
                column: "AuthorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardCardComments_BoardCardId_CreatedAtUtc",
                table: "BoardCardComments",
                columns: new[] { "BoardCardId", "CreatedAtUtc" });

            migrationBuilder.AddForeignKey(
                name: "FK_BoardCards_AspNetUsers_AssignedUserId",
                table: "BoardCards",
                column: "AssignedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardCards_AspNetUsers_CreatedByUserId",
                table: "BoardCards",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardCards_AspNetUsers_AssignedUserId",
                table: "BoardCards");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardCards_AspNetUsers_CreatedByUserId",
                table: "BoardCards");

            migrationBuilder.DropTable(
                name: "BoardCardComments");

            migrationBuilder.DropIndex(
                name: "IX_BoardCards_AssignedUserId",
                table: "BoardCards");

            migrationBuilder.DropIndex(
                name: "IX_BoardCards_CreatedByUserId",
                table: "BoardCards");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "BoardCards");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "BoardCards");

            migrationBuilder.DropColumn(
                name: "DueAtUtc",
                table: "BoardCards");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "BoardCards");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "BoardCards");
        }
    }
}
