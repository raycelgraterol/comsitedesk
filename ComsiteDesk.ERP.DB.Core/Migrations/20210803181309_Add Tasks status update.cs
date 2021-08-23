using Microsoft.EntityFrameworkCore.Migrations;

namespace ComsiteDesk.ERP.DB.Core.Migrations
{
    public partial class AddTasksstatusupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TaskStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Por Hacer");

            migrationBuilder.UpdateData(
                table: "TaskStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "En proceso");

            migrationBuilder.UpdateData(
                table: "TaskStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Terminada");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TaskStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Asignada");

            migrationBuilder.UpdateData(
                table: "TaskStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Pendiente");

            migrationBuilder.UpdateData(
                table: "TaskStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Libre");
        }
    }
}
