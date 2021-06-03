using Microsoft.EntityFrameworkCore.Migrations;

namespace ComsiteDesk.ERP.DB.Core.Migrations
{
    public partial class newschemafororganizationleves : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationTypesId",
                table: "Organizations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParentOrganizationId",
                table: "Organizations",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrganizationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "OrganizationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Organizacion Matriz" },
                    { 2, "Organizacion Principal" },
                    { 3, "Organizacion Secundaria" }
                });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "587d8c0e-bb9e-451f-8ff4-d58dc5cd2a23");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "80c65947-3d0a-4b0f-9c15-da2e4a979898");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "5c4c1cf0-6c83-4df5-adc1-602313c11d39");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "ConcurrencyStamp",
                value: "9dca6bb6-6a00-4ba1-a93a-2d60365c49f2");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_OrganizationTypesId",
                table: "Organizations",
                column: "OrganizationTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_ParentOrganizationId",
                table: "Organizations",
                column: "ParentOrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_OrganizationTypes_OrganizationTypesId",
                table: "Organizations",
                column: "OrganizationTypesId",
                principalTable: "OrganizationTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Organizations_ParentOrganizationId",
                table: "Organizations",
                column: "ParentOrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_OrganizationTypes_OrganizationTypesId",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Organizations_ParentOrganizationId",
                table: "Organizations");

            migrationBuilder.DropTable(
                name: "OrganizationTypes");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_OrganizationTypesId",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_ParentOrganizationId",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "OrganizationTypesId",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "ParentOrganizationId",
                table: "Organizations");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "87c211b6-fe67-4659-b491-18a75dceaf8c");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "d23d33eb-208f-41fd-94d3-f2e31217880b");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "30d3712f-8cf0-421f-98bb-29fbf3946116");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "ConcurrencyStamp",
                value: "3216255f-53d6-4332-8901-f663c9365faf");
        }
    }
}
