using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComsiteDesk.ERP.DB.Core.Migrations
{
    public partial class Addtheorganizationownwerofthesoftware : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "Address", "BusinessName", "CreatedBy", "DateCreated", "DateModified", "Email", "IsActive", "ModifiedBy", "OrganizationTypesId", "ParentOrganizationId", "PhoneNumber", "RIF" },
                values: new object[] { 1, "Av. La Colina Edf. Florencia Local 2 Los Chaguaramos Caracas – Venezuela", "ComSite, C.A.", 1, new DateTime(2021, 5, 4, 11, 28, 44, 50, DateTimeKind.Local).AddTicks(2821), null, "daniel@comsite.com.ve", true, null, 1, null, "02126616922", "J-308373478" });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "6d1a354c-cb8a-40ab-8375-178bdc0fe8f8");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "d7e2ed4b-1bf8-44f7-938e-ce2744cf1e85");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "f8498b8b-cf8e-4358-86a4-b9a09d82f327");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "ConcurrencyStamp",
                value: "b94714d8-b07c-4a53-b2ff-5ef3e65118bf");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 1);

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
        }
    }
}
