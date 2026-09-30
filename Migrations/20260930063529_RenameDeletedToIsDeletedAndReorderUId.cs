using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfn.DevOps.Migrations
{
    /// <inheritdoc />
    public partial class RenameDeletedToIsDeletedAndReorderUId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "SyUser",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "Projects",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "DeploymentSteps",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "DeploymentRuns",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "Boards",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "BoardColumns",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "BoardCards",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "BoardCardComments",
                newName: "IsDeleted");

            migrationBuilder.AlterColumn<Guid>(
                name: "UId",
                table: "SyUser",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'00000000-0000-0000-0000-000000000000'",
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValueSql: "'00000000-0000-0000-0000-000000000000'")
                .Annotation("Relational:ColumnOrder", 1000)
                .OldAnnotation("Relational:ColumnOrder", 1009);

            migrationBuilder.AlterColumn<string>(
                name: "ModUser",
                table: "SyUser",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1003)
                .OldAnnotation("Relational:ColumnOrder", 1002);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModDate",
                table: "SyUser",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1004)
                .OldAnnotation("Relational:ColumnOrder", 1003);

            migrationBuilder.AlterColumn<string>(
                name: "DelUser",
                table: "SyUser",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1005)
                .OldAnnotation("Relational:ColumnOrder", 1004);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DelDate",
                table: "SyUser",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1006)
                .OldAnnotation("Relational:ColumnOrder", 1005);

            migrationBuilder.AlterColumn<string>(
                name: "CreUser",
                table: "SyUser",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1001)
                .OldAnnotation("Relational:ColumnOrder", 1000);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreDate",
                table: "SyUser",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'1970-01-01 00:00:00'",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "'1970-01-01 00:00:00'")
                .Annotation("Relational:ColumnOrder", 1002)
                .OldAnnotation("Relational:ColumnOrder", 1001);

            migrationBuilder.AlterColumn<string>(
                name: "ClientIp",
                table: "SyUser",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1008)
                .OldAnnotation("Relational:ColumnOrder", 1007);

            migrationBuilder.AlterColumn<string>(
                name: "Client",
                table: "SyUser",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1007)
                .OldAnnotation("Relational:ColumnOrder", 1006);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "SyUser",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldDefaultValue: false)
                .Annotation("Relational:ColumnOrder", 1009)
                .OldAnnotation("Relational:ColumnOrder", 1008);

            migrationBuilder.AlterColumn<Guid>(
                name: "UId",
                table: "Projects",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'00000000-0000-0000-0000-000000000000'",
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValueSql: "'00000000-0000-0000-0000-000000000000'")
                .Annotation("Relational:ColumnOrder", 1000)
                .OldAnnotation("Relational:ColumnOrder", 1009);

            migrationBuilder.AlterColumn<string>(
                name: "ModUser",
                table: "Projects",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1003)
                .OldAnnotation("Relational:ColumnOrder", 1002);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModDate",
                table: "Projects",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1004)
                .OldAnnotation("Relational:ColumnOrder", 1003);

            migrationBuilder.AlterColumn<string>(
                name: "DelUser",
                table: "Projects",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1005)
                .OldAnnotation("Relational:ColumnOrder", 1004);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DelDate",
                table: "Projects",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1006)
                .OldAnnotation("Relational:ColumnOrder", 1005);

            migrationBuilder.AlterColumn<string>(
                name: "CreUser",
                table: "Projects",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1001)
                .OldAnnotation("Relational:ColumnOrder", 1000);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreDate",
                table: "Projects",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'1970-01-01 00:00:00'",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "'1970-01-01 00:00:00'")
                .Annotation("Relational:ColumnOrder", 1002)
                .OldAnnotation("Relational:ColumnOrder", 1001);

            migrationBuilder.AlterColumn<string>(
                name: "ClientIp",
                table: "Projects",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1008)
                .OldAnnotation("Relational:ColumnOrder", 1007);

            migrationBuilder.AlterColumn<string>(
                name: "Client",
                table: "Projects",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1007)
                .OldAnnotation("Relational:ColumnOrder", 1006);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Projects",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldDefaultValue: false)
                .Annotation("Relational:ColumnOrder", 1009)
                .OldAnnotation("Relational:ColumnOrder", 1008);

            migrationBuilder.AlterColumn<Guid>(
                name: "UId",
                table: "DeploymentSteps",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'00000000-0000-0000-0000-000000000000'",
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValueSql: "'00000000-0000-0000-0000-000000000000'")
                .Annotation("Relational:ColumnOrder", 1000)
                .OldAnnotation("Relational:ColumnOrder", 1009);

            migrationBuilder.AlterColumn<string>(
                name: "ModUser",
                table: "DeploymentSteps",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1003)
                .OldAnnotation("Relational:ColumnOrder", 1002);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModDate",
                table: "DeploymentSteps",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1004)
                .OldAnnotation("Relational:ColumnOrder", 1003);

            migrationBuilder.AlterColumn<string>(
                name: "DelUser",
                table: "DeploymentSteps",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1005)
                .OldAnnotation("Relational:ColumnOrder", 1004);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DelDate",
                table: "DeploymentSteps",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1006)
                .OldAnnotation("Relational:ColumnOrder", 1005);

            migrationBuilder.AlterColumn<string>(
                name: "CreUser",
                table: "DeploymentSteps",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1001)
                .OldAnnotation("Relational:ColumnOrder", 1000);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreDate",
                table: "DeploymentSteps",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'1970-01-01 00:00:00'",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "'1970-01-01 00:00:00'")
                .Annotation("Relational:ColumnOrder", 1002)
                .OldAnnotation("Relational:ColumnOrder", 1001);

            migrationBuilder.AlterColumn<string>(
                name: "ClientIp",
                table: "DeploymentSteps",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1008)
                .OldAnnotation("Relational:ColumnOrder", 1007);

            migrationBuilder.AlterColumn<string>(
                name: "Client",
                table: "DeploymentSteps",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1007)
                .OldAnnotation("Relational:ColumnOrder", 1006);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DeploymentSteps",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldDefaultValue: false)
                .Annotation("Relational:ColumnOrder", 1009)
                .OldAnnotation("Relational:ColumnOrder", 1008);

            migrationBuilder.AlterColumn<Guid>(
                name: "UId",
                table: "DeploymentRuns",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'00000000-0000-0000-0000-000000000000'",
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValueSql: "'00000000-0000-0000-0000-000000000000'")
                .Annotation("Relational:ColumnOrder", 1000)
                .OldAnnotation("Relational:ColumnOrder", 1009);

            migrationBuilder.AlterColumn<string>(
                name: "ModUser",
                table: "DeploymentRuns",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1003)
                .OldAnnotation("Relational:ColumnOrder", 1002);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModDate",
                table: "DeploymentRuns",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1004)
                .OldAnnotation("Relational:ColumnOrder", 1003);

            migrationBuilder.AlterColumn<string>(
                name: "DelUser",
                table: "DeploymentRuns",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1005)
                .OldAnnotation("Relational:ColumnOrder", 1004);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DelDate",
                table: "DeploymentRuns",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1006)
                .OldAnnotation("Relational:ColumnOrder", 1005);

            migrationBuilder.AlterColumn<string>(
                name: "CreUser",
                table: "DeploymentRuns",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1001)
                .OldAnnotation("Relational:ColumnOrder", 1000);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreDate",
                table: "DeploymentRuns",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'1970-01-01 00:00:00'",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "'1970-01-01 00:00:00'")
                .Annotation("Relational:ColumnOrder", 1002)
                .OldAnnotation("Relational:ColumnOrder", 1001);

            migrationBuilder.AlterColumn<string>(
                name: "ClientIp",
                table: "DeploymentRuns",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1008)
                .OldAnnotation("Relational:ColumnOrder", 1007);

            migrationBuilder.AlterColumn<string>(
                name: "Client",
                table: "DeploymentRuns",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1007)
                .OldAnnotation("Relational:ColumnOrder", 1006);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DeploymentRuns",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldDefaultValue: false)
                .Annotation("Relational:ColumnOrder", 1009)
                .OldAnnotation("Relational:ColumnOrder", 1008);

            migrationBuilder.AlterColumn<Guid>(
                name: "UId",
                table: "Boards",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'00000000-0000-0000-0000-000000000000'",
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValueSql: "'00000000-0000-0000-0000-000000000000'")
                .Annotation("Relational:ColumnOrder", 1000)
                .OldAnnotation("Relational:ColumnOrder", 1009);

            migrationBuilder.AlterColumn<string>(
                name: "ModUser",
                table: "Boards",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1003)
                .OldAnnotation("Relational:ColumnOrder", 1002);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModDate",
                table: "Boards",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1004)
                .OldAnnotation("Relational:ColumnOrder", 1003);

            migrationBuilder.AlterColumn<string>(
                name: "DelUser",
                table: "Boards",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1005)
                .OldAnnotation("Relational:ColumnOrder", 1004);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DelDate",
                table: "Boards",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1006)
                .OldAnnotation("Relational:ColumnOrder", 1005);

            migrationBuilder.AlterColumn<string>(
                name: "CreUser",
                table: "Boards",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1001)
                .OldAnnotation("Relational:ColumnOrder", 1000);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreDate",
                table: "Boards",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'1970-01-01 00:00:00'",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "'1970-01-01 00:00:00'")
                .Annotation("Relational:ColumnOrder", 1002)
                .OldAnnotation("Relational:ColumnOrder", 1001);

            migrationBuilder.AlterColumn<string>(
                name: "ClientIp",
                table: "Boards",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1008)
                .OldAnnotation("Relational:ColumnOrder", 1007);

            migrationBuilder.AlterColumn<string>(
                name: "Client",
                table: "Boards",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1007)
                .OldAnnotation("Relational:ColumnOrder", 1006);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Boards",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldDefaultValue: false)
                .Annotation("Relational:ColumnOrder", 1009)
                .OldAnnotation("Relational:ColumnOrder", 1008);

            migrationBuilder.AlterColumn<Guid>(
                name: "UId",
                table: "BoardColumns",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'00000000-0000-0000-0000-000000000000'",
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValueSql: "'00000000-0000-0000-0000-000000000000'")
                .Annotation("Relational:ColumnOrder", 1000)
                .OldAnnotation("Relational:ColumnOrder", 1009);

            migrationBuilder.AlterColumn<string>(
                name: "ModUser",
                table: "BoardColumns",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1003)
                .OldAnnotation("Relational:ColumnOrder", 1002);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModDate",
                table: "BoardColumns",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1004)
                .OldAnnotation("Relational:ColumnOrder", 1003);

            migrationBuilder.AlterColumn<string>(
                name: "DelUser",
                table: "BoardColumns",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1005)
                .OldAnnotation("Relational:ColumnOrder", 1004);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DelDate",
                table: "BoardColumns",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1006)
                .OldAnnotation("Relational:ColumnOrder", 1005);

            migrationBuilder.AlterColumn<string>(
                name: "CreUser",
                table: "BoardColumns",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1001)
                .OldAnnotation("Relational:ColumnOrder", 1000);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreDate",
                table: "BoardColumns",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'1970-01-01 00:00:00'",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "'1970-01-01 00:00:00'")
                .Annotation("Relational:ColumnOrder", 1002)
                .OldAnnotation("Relational:ColumnOrder", 1001);

            migrationBuilder.AlterColumn<string>(
                name: "ClientIp",
                table: "BoardColumns",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1008)
                .OldAnnotation("Relational:ColumnOrder", 1007);

            migrationBuilder.AlterColumn<string>(
                name: "Client",
                table: "BoardColumns",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1007)
                .OldAnnotation("Relational:ColumnOrder", 1006);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BoardColumns",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldDefaultValue: false)
                .Annotation("Relational:ColumnOrder", 1009)
                .OldAnnotation("Relational:ColumnOrder", 1008);

            migrationBuilder.AlterColumn<Guid>(
                name: "UId",
                table: "BoardCards",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'00000000-0000-0000-0000-000000000000'",
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValueSql: "'00000000-0000-0000-0000-000000000000'")
                .Annotation("Relational:ColumnOrder", 1000)
                .OldAnnotation("Relational:ColumnOrder", 1009);

            migrationBuilder.AlterColumn<string>(
                name: "ModUser",
                table: "BoardCards",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1003)
                .OldAnnotation("Relational:ColumnOrder", 1002);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModDate",
                table: "BoardCards",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1004)
                .OldAnnotation("Relational:ColumnOrder", 1003);

            migrationBuilder.AlterColumn<string>(
                name: "DelUser",
                table: "BoardCards",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1005)
                .OldAnnotation("Relational:ColumnOrder", 1004);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DelDate",
                table: "BoardCards",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1006)
                .OldAnnotation("Relational:ColumnOrder", 1005);

            migrationBuilder.AlterColumn<string>(
                name: "CreUser",
                table: "BoardCards",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1001)
                .OldAnnotation("Relational:ColumnOrder", 1000);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreDate",
                table: "BoardCards",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'1970-01-01 00:00:00'",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "'1970-01-01 00:00:00'")
                .Annotation("Relational:ColumnOrder", 1002)
                .OldAnnotation("Relational:ColumnOrder", 1001);

            migrationBuilder.AlterColumn<string>(
                name: "ClientIp",
                table: "BoardCards",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1008)
                .OldAnnotation("Relational:ColumnOrder", 1007);

            migrationBuilder.AlterColumn<string>(
                name: "Client",
                table: "BoardCards",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1007)
                .OldAnnotation("Relational:ColumnOrder", 1006);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BoardCards",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldDefaultValue: false)
                .Annotation("Relational:ColumnOrder", 1009)
                .OldAnnotation("Relational:ColumnOrder", 1008);

            migrationBuilder.AlterColumn<Guid>(
                name: "UId",
                table: "BoardCardComments",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'00000000-0000-0000-0000-000000000000'",
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValueSql: "'00000000-0000-0000-0000-000000000000'")
                .Annotation("Relational:ColumnOrder", 1000)
                .OldAnnotation("Relational:ColumnOrder", 1009);

            migrationBuilder.AlterColumn<string>(
                name: "ModUser",
                table: "BoardCardComments",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1003)
                .OldAnnotation("Relational:ColumnOrder", 1002);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModDate",
                table: "BoardCardComments",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1004)
                .OldAnnotation("Relational:ColumnOrder", 1003);

            migrationBuilder.AlterColumn<string>(
                name: "DelUser",
                table: "BoardCardComments",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1005)
                .OldAnnotation("Relational:ColumnOrder", 1004);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DelDate",
                table: "BoardCardComments",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1006)
                .OldAnnotation("Relational:ColumnOrder", 1005);

            migrationBuilder.AlterColumn<string>(
                name: "CreUser",
                table: "BoardCardComments",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1001)
                .OldAnnotation("Relational:ColumnOrder", 1000);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreDate",
                table: "BoardCardComments",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'1970-01-01 00:00:00'",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "'1970-01-01 00:00:00'")
                .Annotation("Relational:ColumnOrder", 1002)
                .OldAnnotation("Relational:ColumnOrder", 1001);

            migrationBuilder.AlterColumn<string>(
                name: "ClientIp",
                table: "BoardCardComments",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1008)
                .OldAnnotation("Relational:ColumnOrder", 1007);

            migrationBuilder.AlterColumn<string>(
                name: "Client",
                table: "BoardCardComments",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1007)
                .OldAnnotation("Relational:ColumnOrder", 1006);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BoardCardComments",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldDefaultValue: false)
                .Annotation("Relational:ColumnOrder", 1009)
                .OldAnnotation("Relational:ColumnOrder", 1008);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "SyUser",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Projects",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "DeploymentSteps",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "DeploymentRuns",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Boards",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "BoardColumns",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "BoardCards",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "BoardCardComments",
                newName: "Deleted");

            migrationBuilder.AlterColumn<Guid>(
                name: "UId",
                table: "SyUser",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'00000000-0000-0000-0000-000000000000'",
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValueSql: "'00000000-0000-0000-0000-000000000000'")
                .Annotation("Relational:ColumnOrder", 1009)
                .OldAnnotation("Relational:ColumnOrder", 1000);

            migrationBuilder.AlterColumn<string>(
                name: "ModUser",
                table: "SyUser",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1002)
                .OldAnnotation("Relational:ColumnOrder", 1003);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModDate",
                table: "SyUser",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1003)
                .OldAnnotation("Relational:ColumnOrder", 1004);

            migrationBuilder.AlterColumn<string>(
                name: "DelUser",
                table: "SyUser",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1004)
                .OldAnnotation("Relational:ColumnOrder", 1005);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DelDate",
                table: "SyUser",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1005)
                .OldAnnotation("Relational:ColumnOrder", 1006);

            migrationBuilder.AlterColumn<string>(
                name: "CreUser",
                table: "SyUser",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1000)
                .OldAnnotation("Relational:ColumnOrder", 1001);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreDate",
                table: "SyUser",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'1970-01-01 00:00:00'",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "'1970-01-01 00:00:00'")
                .Annotation("Relational:ColumnOrder", 1001)
                .OldAnnotation("Relational:ColumnOrder", 1002);

            migrationBuilder.AlterColumn<string>(
                name: "ClientIp",
                table: "SyUser",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1007)
                .OldAnnotation("Relational:ColumnOrder", 1008);

            migrationBuilder.AlterColumn<string>(
                name: "Client",
                table: "SyUser",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1006)
                .OldAnnotation("Relational:ColumnOrder", 1007);

            migrationBuilder.AlterColumn<bool>(
                name: "Deleted",
                table: "SyUser",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldDefaultValue: false)
                .Annotation("Relational:ColumnOrder", 1008)
                .OldAnnotation("Relational:ColumnOrder", 1009);

            migrationBuilder.AlterColumn<Guid>(
                name: "UId",
                table: "Projects",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'00000000-0000-0000-0000-000000000000'",
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValueSql: "'00000000-0000-0000-0000-000000000000'")
                .Annotation("Relational:ColumnOrder", 1009)
                .OldAnnotation("Relational:ColumnOrder", 1000);

            migrationBuilder.AlterColumn<string>(
                name: "ModUser",
                table: "Projects",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1002)
                .OldAnnotation("Relational:ColumnOrder", 1003);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModDate",
                table: "Projects",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1003)
                .OldAnnotation("Relational:ColumnOrder", 1004);

            migrationBuilder.AlterColumn<string>(
                name: "DelUser",
                table: "Projects",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1004)
                .OldAnnotation("Relational:ColumnOrder", 1005);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DelDate",
                table: "Projects",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1005)
                .OldAnnotation("Relational:ColumnOrder", 1006);

            migrationBuilder.AlterColumn<string>(
                name: "CreUser",
                table: "Projects",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1000)
                .OldAnnotation("Relational:ColumnOrder", 1001);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreDate",
                table: "Projects",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'1970-01-01 00:00:00'",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "'1970-01-01 00:00:00'")
                .Annotation("Relational:ColumnOrder", 1001)
                .OldAnnotation("Relational:ColumnOrder", 1002);

            migrationBuilder.AlterColumn<string>(
                name: "ClientIp",
                table: "Projects",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1007)
                .OldAnnotation("Relational:ColumnOrder", 1008);

            migrationBuilder.AlterColumn<string>(
                name: "Client",
                table: "Projects",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1006)
                .OldAnnotation("Relational:ColumnOrder", 1007);

            migrationBuilder.AlterColumn<bool>(
                name: "Deleted",
                table: "Projects",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldDefaultValue: false)
                .Annotation("Relational:ColumnOrder", 1008)
                .OldAnnotation("Relational:ColumnOrder", 1009);

            migrationBuilder.AlterColumn<Guid>(
                name: "UId",
                table: "DeploymentSteps",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'00000000-0000-0000-0000-000000000000'",
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValueSql: "'00000000-0000-0000-0000-000000000000'")
                .Annotation("Relational:ColumnOrder", 1009)
                .OldAnnotation("Relational:ColumnOrder", 1000);

            migrationBuilder.AlterColumn<string>(
                name: "ModUser",
                table: "DeploymentSteps",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1002)
                .OldAnnotation("Relational:ColumnOrder", 1003);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModDate",
                table: "DeploymentSteps",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1003)
                .OldAnnotation("Relational:ColumnOrder", 1004);

            migrationBuilder.AlterColumn<string>(
                name: "DelUser",
                table: "DeploymentSteps",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1004)
                .OldAnnotation("Relational:ColumnOrder", 1005);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DelDate",
                table: "DeploymentSteps",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1005)
                .OldAnnotation("Relational:ColumnOrder", 1006);

            migrationBuilder.AlterColumn<string>(
                name: "CreUser",
                table: "DeploymentSteps",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1000)
                .OldAnnotation("Relational:ColumnOrder", 1001);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreDate",
                table: "DeploymentSteps",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'1970-01-01 00:00:00'",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "'1970-01-01 00:00:00'")
                .Annotation("Relational:ColumnOrder", 1001)
                .OldAnnotation("Relational:ColumnOrder", 1002);

            migrationBuilder.AlterColumn<string>(
                name: "ClientIp",
                table: "DeploymentSteps",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1007)
                .OldAnnotation("Relational:ColumnOrder", 1008);

            migrationBuilder.AlterColumn<string>(
                name: "Client",
                table: "DeploymentSteps",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1006)
                .OldAnnotation("Relational:ColumnOrder", 1007);

            migrationBuilder.AlterColumn<bool>(
                name: "Deleted",
                table: "DeploymentSteps",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldDefaultValue: false)
                .Annotation("Relational:ColumnOrder", 1008)
                .OldAnnotation("Relational:ColumnOrder", 1009);

            migrationBuilder.AlterColumn<Guid>(
                name: "UId",
                table: "DeploymentRuns",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'00000000-0000-0000-0000-000000000000'",
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValueSql: "'00000000-0000-0000-0000-000000000000'")
                .Annotation("Relational:ColumnOrder", 1009)
                .OldAnnotation("Relational:ColumnOrder", 1000);

            migrationBuilder.AlterColumn<string>(
                name: "ModUser",
                table: "DeploymentRuns",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1002)
                .OldAnnotation("Relational:ColumnOrder", 1003);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModDate",
                table: "DeploymentRuns",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1003)
                .OldAnnotation("Relational:ColumnOrder", 1004);

            migrationBuilder.AlterColumn<string>(
                name: "DelUser",
                table: "DeploymentRuns",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1004)
                .OldAnnotation("Relational:ColumnOrder", 1005);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DelDate",
                table: "DeploymentRuns",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1005)
                .OldAnnotation("Relational:ColumnOrder", 1006);

            migrationBuilder.AlterColumn<string>(
                name: "CreUser",
                table: "DeploymentRuns",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1000)
                .OldAnnotation("Relational:ColumnOrder", 1001);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreDate",
                table: "DeploymentRuns",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'1970-01-01 00:00:00'",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "'1970-01-01 00:00:00'")
                .Annotation("Relational:ColumnOrder", 1001)
                .OldAnnotation("Relational:ColumnOrder", 1002);

            migrationBuilder.AlterColumn<string>(
                name: "ClientIp",
                table: "DeploymentRuns",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1007)
                .OldAnnotation("Relational:ColumnOrder", 1008);

            migrationBuilder.AlterColumn<string>(
                name: "Client",
                table: "DeploymentRuns",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1006)
                .OldAnnotation("Relational:ColumnOrder", 1007);

            migrationBuilder.AlterColumn<bool>(
                name: "Deleted",
                table: "DeploymentRuns",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldDefaultValue: false)
                .Annotation("Relational:ColumnOrder", 1008)
                .OldAnnotation("Relational:ColumnOrder", 1009);

            migrationBuilder.AlterColumn<Guid>(
                name: "UId",
                table: "Boards",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'00000000-0000-0000-0000-000000000000'",
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValueSql: "'00000000-0000-0000-0000-000000000000'")
                .Annotation("Relational:ColumnOrder", 1009)
                .OldAnnotation("Relational:ColumnOrder", 1000);

            migrationBuilder.AlterColumn<string>(
                name: "ModUser",
                table: "Boards",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1002)
                .OldAnnotation("Relational:ColumnOrder", 1003);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModDate",
                table: "Boards",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1003)
                .OldAnnotation("Relational:ColumnOrder", 1004);

            migrationBuilder.AlterColumn<string>(
                name: "DelUser",
                table: "Boards",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1004)
                .OldAnnotation("Relational:ColumnOrder", 1005);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DelDate",
                table: "Boards",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1005)
                .OldAnnotation("Relational:ColumnOrder", 1006);

            migrationBuilder.AlterColumn<string>(
                name: "CreUser",
                table: "Boards",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1000)
                .OldAnnotation("Relational:ColumnOrder", 1001);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreDate",
                table: "Boards",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'1970-01-01 00:00:00'",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "'1970-01-01 00:00:00'")
                .Annotation("Relational:ColumnOrder", 1001)
                .OldAnnotation("Relational:ColumnOrder", 1002);

            migrationBuilder.AlterColumn<string>(
                name: "ClientIp",
                table: "Boards",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1007)
                .OldAnnotation("Relational:ColumnOrder", 1008);

            migrationBuilder.AlterColumn<string>(
                name: "Client",
                table: "Boards",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1006)
                .OldAnnotation("Relational:ColumnOrder", 1007);

            migrationBuilder.AlterColumn<bool>(
                name: "Deleted",
                table: "Boards",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldDefaultValue: false)
                .Annotation("Relational:ColumnOrder", 1008)
                .OldAnnotation("Relational:ColumnOrder", 1009);

            migrationBuilder.AlterColumn<Guid>(
                name: "UId",
                table: "BoardColumns",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'00000000-0000-0000-0000-000000000000'",
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValueSql: "'00000000-0000-0000-0000-000000000000'")
                .Annotation("Relational:ColumnOrder", 1009)
                .OldAnnotation("Relational:ColumnOrder", 1000);

            migrationBuilder.AlterColumn<string>(
                name: "ModUser",
                table: "BoardColumns",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1002)
                .OldAnnotation("Relational:ColumnOrder", 1003);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModDate",
                table: "BoardColumns",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1003)
                .OldAnnotation("Relational:ColumnOrder", 1004);

            migrationBuilder.AlterColumn<string>(
                name: "DelUser",
                table: "BoardColumns",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1004)
                .OldAnnotation("Relational:ColumnOrder", 1005);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DelDate",
                table: "BoardColumns",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1005)
                .OldAnnotation("Relational:ColumnOrder", 1006);

            migrationBuilder.AlterColumn<string>(
                name: "CreUser",
                table: "BoardColumns",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1000)
                .OldAnnotation("Relational:ColumnOrder", 1001);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreDate",
                table: "BoardColumns",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'1970-01-01 00:00:00'",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "'1970-01-01 00:00:00'")
                .Annotation("Relational:ColumnOrder", 1001)
                .OldAnnotation("Relational:ColumnOrder", 1002);

            migrationBuilder.AlterColumn<string>(
                name: "ClientIp",
                table: "BoardColumns",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1007)
                .OldAnnotation("Relational:ColumnOrder", 1008);

            migrationBuilder.AlterColumn<string>(
                name: "Client",
                table: "BoardColumns",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1006)
                .OldAnnotation("Relational:ColumnOrder", 1007);

            migrationBuilder.AlterColumn<bool>(
                name: "Deleted",
                table: "BoardColumns",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldDefaultValue: false)
                .Annotation("Relational:ColumnOrder", 1008)
                .OldAnnotation("Relational:ColumnOrder", 1009);

            migrationBuilder.AlterColumn<Guid>(
                name: "UId",
                table: "BoardCards",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'00000000-0000-0000-0000-000000000000'",
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValueSql: "'00000000-0000-0000-0000-000000000000'")
                .Annotation("Relational:ColumnOrder", 1009)
                .OldAnnotation("Relational:ColumnOrder", 1000);

            migrationBuilder.AlterColumn<string>(
                name: "ModUser",
                table: "BoardCards",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1002)
                .OldAnnotation("Relational:ColumnOrder", 1003);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModDate",
                table: "BoardCards",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1003)
                .OldAnnotation("Relational:ColumnOrder", 1004);

            migrationBuilder.AlterColumn<string>(
                name: "DelUser",
                table: "BoardCards",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1004)
                .OldAnnotation("Relational:ColumnOrder", 1005);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DelDate",
                table: "BoardCards",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1005)
                .OldAnnotation("Relational:ColumnOrder", 1006);

            migrationBuilder.AlterColumn<string>(
                name: "CreUser",
                table: "BoardCards",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1000)
                .OldAnnotation("Relational:ColumnOrder", 1001);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreDate",
                table: "BoardCards",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'1970-01-01 00:00:00'",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "'1970-01-01 00:00:00'")
                .Annotation("Relational:ColumnOrder", 1001)
                .OldAnnotation("Relational:ColumnOrder", 1002);

            migrationBuilder.AlterColumn<string>(
                name: "ClientIp",
                table: "BoardCards",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1007)
                .OldAnnotation("Relational:ColumnOrder", 1008);

            migrationBuilder.AlterColumn<string>(
                name: "Client",
                table: "BoardCards",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1006)
                .OldAnnotation("Relational:ColumnOrder", 1007);

            migrationBuilder.AlterColumn<bool>(
                name: "Deleted",
                table: "BoardCards",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldDefaultValue: false)
                .Annotation("Relational:ColumnOrder", 1008)
                .OldAnnotation("Relational:ColumnOrder", 1009);

            migrationBuilder.AlterColumn<Guid>(
                name: "UId",
                table: "BoardCardComments",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'00000000-0000-0000-0000-000000000000'",
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValueSql: "'00000000-0000-0000-0000-000000000000'")
                .Annotation("Relational:ColumnOrder", 1009)
                .OldAnnotation("Relational:ColumnOrder", 1000);

            migrationBuilder.AlterColumn<string>(
                name: "ModUser",
                table: "BoardCardComments",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1002)
                .OldAnnotation("Relational:ColumnOrder", 1003);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModDate",
                table: "BoardCardComments",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1003)
                .OldAnnotation("Relational:ColumnOrder", 1004);

            migrationBuilder.AlterColumn<string>(
                name: "DelUser",
                table: "BoardCardComments",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1004)
                .OldAnnotation("Relational:ColumnOrder", 1005);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DelDate",
                table: "BoardCardComments",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1005)
                .OldAnnotation("Relational:ColumnOrder", 1006);

            migrationBuilder.AlterColumn<string>(
                name: "CreUser",
                table: "BoardCardComments",
                type: "TEXT",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 450,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1000)
                .OldAnnotation("Relational:ColumnOrder", 1001);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreDate",
                table: "BoardCardComments",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'1970-01-01 00:00:00'",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "'1970-01-01 00:00:00'")
                .Annotation("Relational:ColumnOrder", 1001)
                .OldAnnotation("Relational:ColumnOrder", 1002);

            migrationBuilder.AlterColumn<string>(
                name: "ClientIp",
                table: "BoardCardComments",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1007)
                .OldAnnotation("Relational:ColumnOrder", 1008);

            migrationBuilder.AlterColumn<string>(
                name: "Client",
                table: "BoardCardComments",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1006)
                .OldAnnotation("Relational:ColumnOrder", 1007);

            migrationBuilder.AlterColumn<bool>(
                name: "Deleted",
                table: "BoardCardComments",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldDefaultValue: false)
                .Annotation("Relational:ColumnOrder", 1008)
                .OldAnnotation("Relational:ColumnOrder", 1009);
        }
    }
}
