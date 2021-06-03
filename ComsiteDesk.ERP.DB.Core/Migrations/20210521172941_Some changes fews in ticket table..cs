using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComsiteDesk.ERP.DB.Core.Migrations
{
    public partial class Somechangesfewsintickettable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Tickets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Tickets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Tickets",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Tickets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Tickets");
        }
    }
}
