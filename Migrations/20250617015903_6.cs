using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSMapi.Migrations
{
    /// <inheritdoc />
    public partial class _6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Weightmoved",
                table: "Repalletizationdetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 17, 9, 59, 2, 501, DateTimeKind.Unspecified).AddTicks(5010), "$2a$11$0fOSz3aXuQMwLmuEbjFfsegyHA6ybgjO2d9hr6rwtKaD/V5cYbwP6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 17, 9, 59, 2, 673, DateTimeKind.Unspecified).AddTicks(7809), "$2a$11$EqOOmKYg1CNWcaJMPlQmLOXe.DjHhC9N5kv5Xh2/frlFe0RKunnSi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 17, 9, 59, 2, 845, DateTimeKind.Unspecified).AddTicks(6902), "$2a$11$0OJJYW1l0Tc7E16eoh7Mg.2TXDosO.s.NMX4reBSWThywkY.z5H46" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
