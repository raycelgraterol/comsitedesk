using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComsiteDesk.ERP.DB.Core.Migrations
{
    public partial class AddTasksstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TaskStatus",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateModified", "IsActive", "ModifiedBy", "Name" },
                values: new object[] { 1, 1L, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, null, "Asignada" });

            migrationBuilder.InsertData(
                table: "TaskStatus",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateModified", "IsActive", "ModifiedBy", "Name" },
                values: new object[] { 2, 1L, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, null, "Pendiente" });

            migrationBuilder.InsertData(
                table: "TaskStatus",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateModified", "IsActive", "ModifiedBy", "Name" },
                values: new object[] { 3, 1L, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, null, "Libre" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskStatus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TaskStatus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TaskStatus",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
