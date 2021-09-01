using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComsiteDesk.ERP.DB.Core.Migrations
{
    public partial class Viewsinsecurity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Forms",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateModified", "Description", "IsActive", "ModifiedBy", "ModuleId", "Name", "URI" },
                values: new object[,]
                {
                    { 3, 1L, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sedes", true, null, 2, "Sedes", "/headquarter" },
                    { 4, 1L, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Departamentos", true, null, 2, "Departamentos", "/department" },
                    { 5, 1L, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Equipos", true, null, 2, "Equipos", "/equipment" },
                    { 6, 1L, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Usuarios de Equipos", true, null, 2, "Usuarios de Equipos", "/equipmentUser" },
                    { 7, 1L, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Proyectos", true, null, 3, "Proyectos", "/list" },
                    { 8, 1L, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Estatus de Proyectos", true, null, 3, "Estatus de Proyectos", "/status" },
                    { 9, 1L, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Tickets", true, null, 4, "Tickets", "/tickets" },
                    { 10, 1L, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Tipos de tickets", true, null, 4, "Tipos de tickets", "/types" },
                    { 11, 1L, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Categorias de tickets", true, null, 4, "Categorias de tickets", "/categories" },
                    { 12, 1L, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Procesos de tickets", true, null, 4, "Procesos de tickets", "/processes" },
                    { 13, 1L, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Estatus de tickets", true, null, 4, "Estatus de tickets", "/status" },
                    { 14, 1L, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lista de tareas", true, null, 5, "Lista de tareas", "/list" },
                    { 15, 1L, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Estatus de tareas", true, null, 5, "Estatus de tareas", "/status" },
                    { 16, 1L, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Panel principal", true, null, 6, "Panel principal", "/dashboard" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Forms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Forms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Forms",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Forms",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Forms",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Forms",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Forms",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Forms",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Forms",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Forms",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Forms",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Forms",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Forms",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Forms",
                keyColumn: "Id",
                keyValue: 16);
        }
    }
}
