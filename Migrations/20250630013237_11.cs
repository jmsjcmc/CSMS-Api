using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSMapi.Migrations
{
    /// <inheritdoc />
    public partial class _11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Productiondate",
                table: "Receivings");

            migrationBuilder.AddColumn<DateTime>(
                name: "Productiondate",
                table: "Receivingdetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 30, 9, 32, 37, 308, DateTimeKind.Unspecified).AddTicks(5068), "$2a$11$yN9t5QQcKA0SrTxAfeS8XOrSq5FZ1xcRsVYvEx4s02jJCS3s05rgG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 30, 9, 32, 37, 482, DateTimeKind.Unspecified).AddTicks(4793), "$2a$11$QcKJNagu5WDVO4cBjPafOuVSj26r3y2BMqxmHR3DzzkTR1JgbvOCC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 30, 9, 32, 37, 658, DateTimeKind.Unspecified).AddTicks(9668), "$2a$11$L2waGscoNU4dW22.XllZ/uT8jcf4tpWR2Tt5N3.quoZ5/E3aEJt2i" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Productiondate",
                table: "Receivingdetails");

            migrationBuilder.AddColumn<DateTime>(
                name: "Productiondate",
                table: "Receivings",
                type: "datetime2",
                nullable: true);

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
    }
}
