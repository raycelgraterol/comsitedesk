using Microsoft.EntityFrameworkCore.Migrations;

namespace ComsiteDesk.ERP.DB.Core.Migrations
{
    public partial class Roleslast : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 5L, "78c9b629-f045-47cf-954c-17e5907dac94", "Role", "Tecnico Soporte I", "TECNICO SOPORTE I" },
                    { 6L, "92f79ac3-ad69-4425-bca5-d91d330d74fc", "Role", "Tecnico Soporte II", "TECNICO SOPORTE II" },
                    { 7L, "77c80bfc-a386-4633-8a04-bf0e539dc2c7", "Role", "Tecnico Soporte III", "TECNICO SOPORTE III" },
                    { 8L, "ab6bea7a-64a7-4d88-a85d-8eb490d57cc9", "Role", "Gerente Soporte", "GERENTE SOPORTE" },
                    { 9L, "a80892a0-fb99-4928-b6b6-86aae2fe2ffc", "Role", "Asistente Administrativo", "ASISTENTE ADMINISTRATIVO" },
                    { 10L, "d96eaba3-0ea0-4521-bb98-1bdad3a31024", "Role", "Vendedor", "VENDEDOR" },
                    { 11L, "fd734eb1-d558-4d89-a238-547f83e5df0a", "Role", "Finanzas", "FINANZAS" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 11L);
        }
    }
}
