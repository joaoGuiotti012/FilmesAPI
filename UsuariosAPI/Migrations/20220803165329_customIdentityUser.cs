using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosAPI.Migrations
{
    public partial class customIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "AspNetUsers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99998,
                column: "ConcurrencyStamp",
                value: "01e36f79-baf9-40b9-8974-804f0b79bb4d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "51d0d306-44b3-4e96-a5e9-2f395622c81c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a2f28353-fd55-4a92-87a4-31ae513088ed", "AQAAAAEAACcQAAAAED6aGkVf459MU7H5OBrXaBv96XHi3FnAlY3piPSHQVMVKvWmVZV3j93waxLE4jh+JA==", "c04a8bb5-0b88-4ac6-b4ea-388344b0b03a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99998,
                column: "ConcurrencyStamp",
                value: "702b60f9-18bb-40e1-ba8b-e220c8113578");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "b00df9d3-40e3-4f79-87f8-ffd47b0c22be");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3991c734-537d-4bb2-9d5e-4384c244dc6e", "AQAAAAEAACcQAAAAENmdju+jRnQdcG75FjBpi382BtvqP+dVa84kt9Co1G6fpjkxBIMKHyaL+iBJH1TwlQ==", "ad9b390e-6349-4671-a492-3670b267ba0f" });
        }
    }
}
