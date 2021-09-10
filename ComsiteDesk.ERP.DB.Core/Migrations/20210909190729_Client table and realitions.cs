using Microsoft.EntityFrameworkCore.Migrations;

namespace ComsiteDesk.ERP.DB.Core.Migrations
{
    public partial class Clienttableandrealitions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_ClientTypes_ClientTypesId",
                table: "Client");

            migrationBuilder.DropForeignKey(
                name: "FK_Client_Organizations_OrganizationId",
                table: "Client");

            migrationBuilder.DropForeignKey(
                name: "FK_Headquarter_Client_ClientId",
                table: "Headquarter");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Client_ClientId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Client",
                table: "Client");

            migrationBuilder.RenameTable(
                name: "Client",
                newName: "Clients");

            migrationBuilder.RenameIndex(
                name: "IX_Client_OrganizationId",
                table: "Clients",
                newName: "IX_Clients_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Client_ClientTypesId",
                table: "Clients",
                newName: "IX_Clients_ClientTypesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_ClientTypes_ClientTypesId",
                table: "Clients",
                column: "ClientTypesId",
                principalTable: "ClientTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Organizations_OrganizationId",
                table: "Clients",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Headquarter_Clients_ClientId",
                table: "Headquarter",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Clients_ClientId",
                table: "Tickets",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_ClientTypes_ClientTypesId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Organizations_OrganizationId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Headquarter_Clients_ClientId",
                table: "Headquarter");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Clients_ClientId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "Client");

            migrationBuilder.RenameIndex(
                name: "IX_Clients_OrganizationId",
                table: "Client",
                newName: "IX_Client_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Clients_ClientTypesId",
                table: "Client",
                newName: "IX_Client_ClientTypesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Client",
                table: "Client",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_ClientTypes_ClientTypesId",
                table: "Client",
                column: "ClientTypesId",
                principalTable: "ClientTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Organizations_OrganizationId",
                table: "Client",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Headquarter_Client_ClientId",
                table: "Headquarter",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Client_ClientId",
                table: "Tickets",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
