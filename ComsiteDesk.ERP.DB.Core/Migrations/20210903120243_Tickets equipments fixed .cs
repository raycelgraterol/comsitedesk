using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComsiteDesk.ERP.DB.Core.Migrations
{
    public partial class Ticketsequipmentsfixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "TicketsEquipments",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "TicketsEquipments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "TicketsEquipments",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TicketsEquipments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedBy",
                table: "TicketsEquipments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TicketsEquipments");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "TicketsEquipments");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "TicketsEquipments");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TicketsEquipments");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "TicketsEquipments");
        }
    }
}
