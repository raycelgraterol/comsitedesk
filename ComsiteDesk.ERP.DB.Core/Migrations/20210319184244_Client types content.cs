using Microsoft.EntityFrameworkCore.Migrations;

namespace ComsiteDesk.ERP.DB.Core.Migrations
{
    public partial class Clienttypescontent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ClientTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 1, "N", "Natural" });

            migrationBuilder.InsertData(
                table: "ClientTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 2, "J", "Juridicada" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClientTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ClientTypes",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
