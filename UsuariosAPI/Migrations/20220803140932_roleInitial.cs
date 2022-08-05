using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosAPI.Migrations
{
    public partial class roleInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "b00df9d3-40e3-4f79-87f8-ffd47b0c22be");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 99998, "702b60f9-18bb-40e1-ba8b-e220c8113578", "initial", "INITIAL" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3991c734-537d-4bb2-9d5e-4384c244dc6e", "AQAAAAEAACcQAAAAENmdju+jRnQdcG75FjBpi382BtvqP+dVa84kt9Co1G6fpjkxBIMKHyaL+iBJH1TwlQ==", "ad9b390e-6349-4671-a492-3670b267ba0f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99998);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "68e001d8-35a5-4ae3-b9cb-ae069d62c368");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3a1ea9e3-1564-4ff8-bff8-6864b9db7ef2", "AQAAAAEAACcQAAAAEPh6QfA52dABeclW81HKuJxMSDCfIvXEs6wGiTftbVe06xyJBrQWUsS7phGydWQk4A==", "896364d7-e8bc-4f65-a0e1-6c8a04e7df68" });
        }
    }
}
