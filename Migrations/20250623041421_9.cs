using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSMapi.Migrations
{
    /// <inheritdoc />
    public partial class _9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Dispatchings");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 23, 12, 14, 20, 450, DateTimeKind.Unspecified).AddTicks(9001), "$2a$11$OuuK7fyLZ8km6MFkvfvRru/SNbGwk/D/1BuUdXiK6zo8T6tiTfpBW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 23, 12, 14, 20, 635, DateTimeKind.Unspecified).AddTicks(3499), "$2a$11$eksmAipy0ygcrAYTIU5Cy.hNI9.HBgddqjnyuHOHtlri1u7sMs3Vm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 23, 12, 14, 20, 808, DateTimeKind.Unspecified).AddTicks(41), "$2a$11$/UgJFWqfdB11l1VJnVG2yejwnnOArnbdjso92nLtQsCalnj1m0pSq" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Dispatchings",
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
    }
}
