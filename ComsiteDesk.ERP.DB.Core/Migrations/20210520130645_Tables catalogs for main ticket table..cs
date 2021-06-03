using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComsiteDesk.ERP.DB.Core.Migrations
{
    public partial class Tablescatalogsformaintickettable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Type = table.Column<string>(maxLength: 255, nullable: true),
                    Make = table.Column<string>(maxLength: 255, nullable: true),
                    Model = table.Column<string>(maxLength: 255, nullable: true),
                    Serial = table.Column<string>(maxLength: 100, nullable: false),
                    features = table.Column<string>(maxLength: 500, nullable: true),
                    Notes = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketProcesses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketProcesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTypes", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "0b245219-a9b6-471c-84ac-60d7dc0c7bbd");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "56fd6ff4-2964-48ec-90d6-964dd677ed08");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "2289fbd7-586c-4c33-b03e-4aba7cb8dfb8");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "ConcurrencyStamp",
                value: "bc346c93-7929-43e0-b168-f25e755074f1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "TicketCategories");

            migrationBuilder.DropTable(
                name: "TicketProcesses");

            migrationBuilder.DropTable(
                name: "TicketStatus");

            migrationBuilder.DropTable(
                name: "TicketTypes");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 5, 4, 11, 28, 44, 50, DateTimeKind.Local).AddTicks(2821));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "6d1a354c-cb8a-40ab-8375-178bdc0fe8f8");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "d7e2ed4b-1bf8-44f7-938e-ce2744cf1e85");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "f8498b8b-cf8e-4358-86a4-b9a09d82f327");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "ConcurrencyStamp",
                value: "b94714d8-b07c-4a53-b2ff-5ef3e65118bf");
        }
    }
}
