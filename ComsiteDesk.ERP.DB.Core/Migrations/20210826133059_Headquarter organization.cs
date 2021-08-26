using Microsoft.EntityFrameworkCore.Migrations;

namespace ComsiteDesk.ERP.DB.Core.Migrations
{
    public partial class Headquarterorganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationsId",
                table: "Headquarter",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Headquarter_OrganizationsId",
                table: "Headquarter",
                column: "OrganizationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Headquarter_Organizations_OrganizationsId",
                table: "Headquarter",
                column: "OrganizationsId",
                principalTable: "Organizations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Headquarter_Organizations_OrganizationsId",
                table: "Headquarter");

            migrationBuilder.DropIndex(
                name: "IX_Headquarter_OrganizationsId",
                table: "Headquarter");

            migrationBuilder.DropColumn(
                name: "OrganizationsId",
                table: "Headquarter");
        }
    }
}
