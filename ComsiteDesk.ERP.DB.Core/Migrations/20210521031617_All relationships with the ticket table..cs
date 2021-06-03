using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComsiteDesk.ERP.DB.Core.Migrations
{
    public partial class Allrelationshipswiththetickettable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Step",
                table: "TicketProcesses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Equipment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 255, nullable: true),
                    TicketDate = table.Column<DateTime>(nullable: false),
                    HoursWorked = table.Column<int>(nullable: false),
                    ReportedFailure = table.Column<string>(maxLength: 1000, nullable: true),
                    TechnicalFailure = table.Column<string>(maxLength: 1000, nullable: true),
                    SolutionDone = table.Column<string>(maxLength: 1000, nullable: true),
                    Notes = table.Column<string>(maxLength: 1000, nullable: true),
                    StartTime = table.Column<DateTime>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: true),
                    TicketStatusId = table.Column<int>(nullable: false),
                    TicketCategoryId = table.Column<int>(nullable: false),
                    TicketTypeId = table.Column<int>(nullable: false),
                    TicketProcessId = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_TicketCategories_TicketCategoryId",
                        column: x => x.TicketCategoryId,
                        principalTable: "TicketCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_TicketProcesses_TicketProcessId",
                        column: x => x.TicketProcessId,
                        principalTable: "TicketProcesses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_TicketStatus_TicketStatusId",
                        column: x => x.TicketStatusId,
                        principalTable: "TicketStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_TicketTypes_TicketTypeId",
                        column: x => x.TicketTypeId,
                        principalTable: "TicketTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TicketsEquipments",
                columns: table => new
                {
                    TicketsId = table.Column<int>(nullable: false),
                    EquipmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketsEquipments", x => new { x.TicketsId, x.EquipmentId });
                    table.ForeignKey(
                        name: "FK_TicketsEquipments_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketsEquipments_Tickets_TicketsId",
                        column: x => x.TicketsId,
                        principalTable: "Tickets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TicketsUsers",
                columns: table => new
                {
                    TicketsId = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketsUsers", x => new { x.TicketsId, x.UserId });
                    table.ForeignKey(
                        name: "FK_TicketsUsers_Tickets_TicketsId",
                        column: x => x.TicketsId,
                        principalTable: "Tickets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketsUsers_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_OrganizationId",
                table: "Equipment",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_OrganizationId",
                table: "Tickets",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketCategoryId",
                table: "Tickets",
                column: "TicketCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketProcessId",
                table: "Tickets",
                column: "TicketProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketStatusId",
                table: "Tickets",
                column: "TicketStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketTypeId",
                table: "Tickets",
                column: "TicketTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketsEquipments_EquipmentId",
                table: "TicketsEquipments",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketsUsers_UserId",
                table: "TicketsUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_Organizations_OrganizationId",
                table: "Equipment",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_Organizations_OrganizationId",
                table: "Equipment");

            migrationBuilder.DropTable(
                name: "TicketsEquipments");

            migrationBuilder.DropTable(
                name: "TicketsUsers");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Equipment_OrganizationId",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "Step",
                table: "TicketProcesses");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Equipment");
        }
    }
}
