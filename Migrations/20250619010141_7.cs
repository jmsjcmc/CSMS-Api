using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSMapi.Migrations
{
    /// <inheritdoc />
    public partial class _7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 19, 9, 1, 41, 43, DateTimeKind.Unspecified).AddTicks(6229), "$2a$11$8NPwg7iH1m6OrGXwPgPmH.VwjQkf2MK/y/YicWvq6MtBzc14qfcsq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 19, 9, 1, 41, 216, DateTimeKind.Unspecified).AddTicks(9430), "$2a$11$ZTwsFWjB6jYY2ZD1lTCCPe9UXA3FBvJc1kXmke8N7rAq3QsPiMRTe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 19, 9, 1, 41, 391, DateTimeKind.Unspecified).AddTicks(1781), "$2a$11$ftnpMKb.2v3X.KUBeFJSD.UVj6IGo8qEcWwoz.eV4o69Aqmb..xIy" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
