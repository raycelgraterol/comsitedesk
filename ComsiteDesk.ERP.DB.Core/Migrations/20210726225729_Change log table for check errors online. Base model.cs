using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComsiteDesk.ERP.DB.Core.Migrations
{
    public partial class ChangelogtableforcheckerrorsonlineBasemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "ChangeLogs",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "ChangeLogs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "ChangeLogs",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ChangeLogs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedBy",
                table: "ChangeLogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ChangeLogs");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "ChangeLogs");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "ChangeLogs");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ChangeLogs");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "ChangeLogs");
        }
    }
}
