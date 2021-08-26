using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComsiteDesk.ERP.DB.Core.Migrations
{
    public partial class Organizationlevelanduserequipment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_Organizations_OrganizationId",
                table: "Equipment");

            migrationBuilder.DropIndex(
                name: "IX_Equipment_OrganizationId",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Equipment");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Equipment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Headquarter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 100, nullable: true),
                    Address = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Headquarter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 100, nullable: true),
                    HeadquarterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_Headquarter_HeadquarterId",
                        column: x => x.HeadquarterId,
                        principalTable: "Headquarter",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_DepartmentId",
                table: "Equipment",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_HeadquarterId",
                table: "Department",
                column: "HeadquarterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_Department_DepartmentId",
                table: "Equipment",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_Department_DepartmentId",
                table: "Equipment");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Headquarter");

            migrationBuilder.DropIndex(
                name: "IX_Equipment_DepartmentId",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Equipment");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Equipment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_OrganizationId",
                table: "Equipment",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_Organizations_OrganizationId",
                table: "Equipment",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
