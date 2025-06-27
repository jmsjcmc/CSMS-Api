using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSMapi.Migrations
{
    /// <inheritdoc />
    public partial class _5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weightmoved",
                table: "Repalletizationdetails");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 17, 8, 26, 38, 500, DateTimeKind.Unspecified).AddTicks(3428), "$2a$11$k6QVa9w3gx/L1hXG6RXE7.rHa/bQWxCu.q1IwGoo.hQvdWZeABf6O" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 17, 8, 26, 38, 667, DateTimeKind.Unspecified).AddTicks(5606), "$2a$11$4QoGUTC4ijHMSDgOFEuKhOYz1.YUtHO9.T/pE/TmDqAUrHv0mGmcO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 17, 8, 26, 38, 836, DateTimeKind.Unspecified).AddTicks(4022), "$2a$11$0MxCVv201qcrOtI1EHOQ/eOM2XT6MjzNkiSSOgAuXOGe7FiAvBGDW" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Weightmoved",
                table: "Repalletizationdetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 17, 8, 15, 56, 257, DateTimeKind.Unspecified).AddTicks(8601), "$2a$11$gkdiVD89c9xJjYEi36qkI.AsXs/l0IH7jQx8TI8Wc0LtR4WFMnQ/O" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 17, 8, 15, 56, 437, DateTimeKind.Unspecified).AddTicks(7373), "$2a$11$BcRfbievghszJFN1CIaJfOuOpPLxzMQLILc3Tm2TFmHeJ5q8Xoo6u" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 17, 8, 15, 56, 615, DateTimeKind.Unspecified).AddTicks(5886), "$2a$11$WgwKWb5RY7f2Wh758xTiJuwpysbEMkF2Ggw1j6LjvfzHUTjH1hrtC" });
        }
    }
}
