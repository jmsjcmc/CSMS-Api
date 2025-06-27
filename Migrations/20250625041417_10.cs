using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSMapi.Migrations
{
    /// <inheritdoc />
    public partial class _10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dispatched",
                table: "Receivings");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 25, 12, 14, 16, 952, DateTimeKind.Unspecified).AddTicks(6336), "$2a$11$Fyy6Sv3KvpZq6ys3D7U8IuyEWXj.vlEK9pIue4lMuzIgbasg/lAqS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 25, 12, 14, 17, 118, DateTimeKind.Unspecified).AddTicks(7910), "$2a$11$dKRkeCvbrsg/eiPoClpS4eyfcKoxLpCGRwKCN/AZozmEfzUSfgvh2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 25, 12, 14, 17, 286, DateTimeKind.Unspecified).AddTicks(5235), "$2a$11$68bDmTofQfGXIq/QH7Wt.OXAYQLIvDSbyx8g7OaAM6p89tZc2W0ai" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Dispatched",
                table: "Receivings",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
    }
}
