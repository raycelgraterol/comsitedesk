using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComsiteDesk.ERP.DB.Core.Migrations
{
    public partial class Clientintableswithoutorganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Headquarter_Organizations_OrganizationsId",
                table: "Headquarter");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Organizations_OrganizationId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_OrganizationId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Headquarter_OrganizationsId",
                table: "Headquarter");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "OrganizationsId",
                table: "Headquarter");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Tickets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Headquarter",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessName = table.Column<string>(maxLength: 255, nullable: true),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    IdNumer = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: true),
                    Address = table.Column<string>(maxLength: 255, nullable: true),
                    ClientTypesId = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Client_ClientTypes_ClientTypesId",
                        column: x => x.ClientTypesId,
                        principalTable: "ClientTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Client_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ClientId",
                table: "Tickets",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Headquarter_ClientId",
                table: "Headquarter",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_ClientTypesId",
                table: "Client",
                column: "ClientTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_OrganizationId",
                table: "Client",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Headquarter_Client_ClientId",
                table: "Headquarter",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Client_ClientId",
                table: "Tickets",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Headquarter_Client_ClientId",
                table: "Headquarter");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Client_ClientId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ClientId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Headquarter_ClientId",
                table: "Headquarter");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Headquarter");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationsId",
                table: "Headquarter",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_OrganizationId",
                table: "Tickets",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Headquarter_OrganizationsId",
                table: "Headquarter",
                column: "OrganizationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Headquarter_Organizations_OrganizationsId",
                table: "Headquarter",
                column: "OrganizationsId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Organizations_OrganizationId",
                table: "Tickets",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
