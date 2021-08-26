using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComsiteDesk.ERP.DB.Core.Migrations
{
    public partial class Equipmentusers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EquipmentUserId",
                table: "Equipment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EquipmentUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentUser", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_EquipmentUserId",
                table: "Equipment",
                column: "EquipmentUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_EquipmentUser_EquipmentUserId",
                table: "Equipment",
                column: "EquipmentUserId",
                principalTable: "EquipmentUser",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_EquipmentUser_EquipmentUserId",
                table: "Equipment");

            migrationBuilder.DropTable(
                name: "EquipmentUser");

            migrationBuilder.DropIndex(
                name: "IX_Equipment_EquipmentUserId",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "EquipmentUserId",
                table: "Equipment");
        }
    }
}
