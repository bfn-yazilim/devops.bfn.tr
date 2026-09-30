using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfn.DevOps.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    UId = table.Column<string>(type: "TEXT", nullable: false, defaultValueSql: "'00000000-0000-0000-0000-000000000000'"),
                    CreUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    CreDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "'1970-01-01 00:00:00'"),
                    ModUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    ModDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DelUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    DelDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Client = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ClientIp = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApplicationPoolName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Environment = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "Dev"),
                    IisSiteName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    RepositoryUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Url = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    WorkingDirectory = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    ZeroDowntimeEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    UId = table.Column<string>(type: "TEXT", nullable: false, defaultValueSql: "'00000000-0000-0000-0000-000000000000'"),
                    CreUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    CreDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "'1970-01-01 00:00:00'"),
                    ModUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    ModDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DelUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    DelDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Client = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ClientIp = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StepTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    UId = table.Column<string>(type: "TEXT", nullable: false, defaultValueSql: "'00000000-0000-0000-0000-000000000000'"),
                    CreUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    CreDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "'1970-01-01 00:00:00'"),
                    ModUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    ModDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DelUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    DelDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Client = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ClientIp = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SyRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SyUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsFounder = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    MustChangePassword = table.Column<bool>(type: "INTEGER", nullable: false),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    Theme = table.Column<string>(type: "TEXT", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    UId = table.Column<string>(type: "TEXT", nullable: false, defaultValueSql: "'00000000-0000-0000-0000-000000000000'"),
                    CreUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    CreDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "'1970-01-01 00:00:00'"),
                    ModUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    ModDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DelUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    DelDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Client = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ClientIp = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoardColumns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BoardId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    SortOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    UId = table.Column<string>(type: "TEXT", nullable: false, defaultValueSql: "'00000000-0000-0000-0000-000000000000'"),
                    CreUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    CreDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "'1970-01-01 00:00:00'"),
                    ModUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    ModDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DelUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    DelDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Client = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ClientIp = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardColumns_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeploymentRuns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FinishedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Log = table.Column<string>(type: "TEXT", nullable: false),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    RequestedByUserId = table.Column<string>(type: "TEXT", maxLength: 450, nullable: false),
                    StartedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    UId = table.Column<string>(type: "TEXT", nullable: false, defaultValueSql: "'00000000-0000-0000-0000-000000000000'"),
                    CreUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    CreDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "'1970-01-01 00:00:00'"),
                    ModUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    ModDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DelUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    DelDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Client = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ClientIp = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeploymentRuns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeploymentRuns_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeploymentSteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContinueOnError = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    SettingsJson = table.Column<string>(type: "TEXT", nullable: false),
                    SortOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    StepTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeoutSeconds = table.Column<int>(type: "INTEGER", nullable: false),
                    UId = table.Column<string>(type: "TEXT", nullable: false, defaultValueSql: "'00000000-0000-0000-0000-000000000000'"),
                    CreUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    CreDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "'1970-01-01 00:00:00'"),
                    ModUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    ModDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DelUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    DelDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Client = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ClientIp = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeploymentSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeploymentSteps_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeploymentSteps_StepTypes_StepTypeId",
                        column: x => x.StepTypeId,
                        principalTable: "StepTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SyUserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyUserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_SyUserLogin_SyUser_UserId",
                        column: x => x.UserId,
                        principalTable: "SyUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SyUserRole",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyUserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_SyUserRole_SyRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "SyRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SyUserRole_SyUser_UserId",
                        column: x => x.UserId,
                        principalTable: "SyUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SyUserToken",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyUserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_SyUserToken_SyUser_UserId",
                        column: x => x.UserId,
                        principalTable: "SyUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoardCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AssignedAgentId = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    AssignedUserId = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    AttachmentLabel = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    AttachmentUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    BoardColumnId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: true),
                    DueAtUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Priority = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "Unspecified"),
                    SortOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    Tags = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    UId = table.Column<string>(type: "TEXT", nullable: false, defaultValueSql: "'00000000-0000-0000-0000-000000000000'"),
                    CreUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    CreDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "'1970-01-01 00:00:00'"),
                    ModUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    ModDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DelUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    DelDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Client = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ClientIp = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardCards_BoardColumns_BoardColumnId",
                        column: x => x.BoardColumnId,
                        principalTable: "BoardColumns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardCards_SyUser_AssignedUserId",
                        column: x => x.AssignedUserId,
                        principalTable: "SyUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_BoardCards_SyUser_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "SyUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "DeploymentRunSteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeploymentRunId = table.Column<int>(type: "INTEGER", nullable: false),
                    DeploymentStepId = table.Column<int>(type: "INTEGER", nullable: false),
                    FinishedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    StartedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    UId = table.Column<string>(type: "TEXT", nullable: false, defaultValueSql: "'00000000-0000-0000-0000-000000000000'"),
                    CreUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    CreDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "'1970-01-01 00:00:00'"),
                    ModUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    ModDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DelUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    DelDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Client = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ClientIp = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeploymentRunSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeploymentRunSteps_DeploymentRuns_DeploymentRunId",
                        column: x => x.DeploymentRunId,
                        principalTable: "DeploymentRuns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeploymentRunSteps_DeploymentSteps_DeploymentStepId",
                        column: x => x.DeploymentStepId,
                        principalTable: "DeploymentSteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoardCardComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthorId = table.Column<string>(type: "TEXT", nullable: true),
                    AuthorUserId = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    BoardCardId = table.Column<int>(type: "INTEGER", nullable: false),
                    Body = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: false),
                    UId = table.Column<string>(type: "TEXT", nullable: false, defaultValueSql: "'00000000-0000-0000-0000-000000000000'"),
                    CreUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    CreDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "'1970-01-01 00:00:00'"),
                    ModUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    ModDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DelUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    DelDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Client = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ClientIp = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardCardComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardCardComments_BoardCards_BoardCardId",
                        column: x => x.BoardCardId,
                        principalTable: "BoardCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardCardComments_SyUser_AuthorUserId",
                        column: x => x.AuthorUserId,
                        principalTable: "SyUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "BoardCardSubtasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BoardCardId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDone = table.Column<bool>(type: "INTEGER", nullable: false),
                    SortOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    UId = table.Column<string>(type: "TEXT", nullable: false, defaultValueSql: "'00000000-0000-0000-0000-000000000000'"),
                    CreUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    CreDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "'1970-01-01 00:00:00'"),
                    ModUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    ModDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DelUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    DelDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Client = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ClientIp = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardCardSubtasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardCardSubtasks_BoardCards_BoardCardId",
                        column: x => x.BoardCardId,
                        principalTable: "BoardCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardCardComments_AuthorUserId",
                table: "BoardCardComments",
                column: "AuthorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardCardComments_BoardCardId_CreDate",
                table: "BoardCardComments",
                columns: new[] { "BoardCardId", "CreDate" });

            migrationBuilder.CreateIndex(
                name: "IX_BoardCards_AssignedUserId",
                table: "BoardCards",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardCards_BoardColumnId_SortOrder",
                table: "BoardCards",
                columns: new[] { "BoardColumnId", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_BoardCards_CreatedByUserId",
                table: "BoardCards",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardCardSubtasks_BoardCardId_SortOrder",
                table: "BoardCardSubtasks",
                columns: new[] { "BoardCardId", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_BoardColumns_BoardId_SortOrder",
                table: "BoardColumns",
                columns: new[] { "BoardId", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_DeploymentRuns_ProjectId",
                table: "DeploymentRuns",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DeploymentRunSteps_DeploymentRunId_DeploymentStepId",
                table: "DeploymentRunSteps",
                columns: new[] { "DeploymentRunId", "DeploymentStepId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeploymentRunSteps_DeploymentStepId",
                table: "DeploymentRunSteps",
                column: "DeploymentStepId");

            migrationBuilder.CreateIndex(
                name: "IX_DeploymentSteps_ProjectId_SortOrder",
                table: "DeploymentSteps",
                columns: new[] { "ProjectId", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_DeploymentSteps_StepTypeId",
                table: "DeploymentSteps",
                column: "StepTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Name_Environment",
                table: "Projects",
                columns: new[] { "Name", "Environment" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StepTypes_Code",
                table: "StepTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "SyRole",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "SyUser",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "SyUser",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SyUserLogin_UserId",
                table: "SyUserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SyUserRole_RoleId",
                table: "SyUserRole",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardCardComments");

            migrationBuilder.DropTable(
                name: "BoardCardSubtasks");

            migrationBuilder.DropTable(
                name: "DeploymentRunSteps");

            migrationBuilder.DropTable(
                name: "SyUserLogin");

            migrationBuilder.DropTable(
                name: "SyUserRole");

            migrationBuilder.DropTable(
                name: "SyUserToken");

            migrationBuilder.DropTable(
                name: "BoardCards");

            migrationBuilder.DropTable(
                name: "DeploymentRuns");

            migrationBuilder.DropTable(
                name: "DeploymentSteps");

            migrationBuilder.DropTable(
                name: "SyRole");

            migrationBuilder.DropTable(
                name: "BoardColumns");

            migrationBuilder.DropTable(
                name: "SyUser");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "StepTypes");

            migrationBuilder.DropTable(
                name: "Boards");
        }
    }
}
