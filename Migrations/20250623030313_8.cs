using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSMapi.Migrations
{
    /// <inheritdoc />
    public partial class _8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Receivings");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 23, 11, 3, 12, 896, DateTimeKind.Unspecified).AddTicks(1455), "$2a$11$A7OGafDtzROk1MncjjIm4e0EfOA1yBx3W/iQDsMQ7kzbPt.EeqPWy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 23, 11, 3, 13, 78, DateTimeKind.Unspecified).AddTicks(2067), "$2a$11$TlyUG93dKVo1lLZgheKN4epi4nSRUCN3h.9aEM1H3SF11eGzAbUOi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 23, 11, 3, 13, 256, DateTimeKind.Unspecified).AddTicks(7434), "$2a$11$Ms5ZN.mOxXEdVRZlv4nSKu0OkxCfeT0xFQW8J3QuR0lHBjXWaPyki" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Receivings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
    }
}
