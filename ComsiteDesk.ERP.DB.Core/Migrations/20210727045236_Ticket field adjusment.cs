using Microsoft.EntityFrameworkCore.Migrations;

namespace ComsiteDesk.ERP.DB.Core.Migrations
{
    public partial class Ticketfieldadjusment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketProcesses_TicketProcessId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "TicketProcessId",
                table: "Tickets",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketProcesses_TicketProcessId",
                table: "Tickets",
                column: "TicketProcessId",
                principalTable: "TicketProcesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketProcesses_TicketProcessId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "TicketProcessId",
                table: "Tickets",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketProcesses_TicketProcessId",
                table: "Tickets",
                column: "TicketProcessId",
                principalTable: "TicketProcesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
