using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSMapi.Migrations
{
    /// <inheritdoc />
    public partial class _4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Weightmoved",
                table: "Repalletizations",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 8, 15, 44, 30, 602, DateTimeKind.Unspecified).AddTicks(5916), "$2a$11$mvZ.ZntVAX/5MeabMXFrgeCI.ueZr3Rj4Wf.uxABOeGRwafg5.nKu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 8, 15, 44, 30, 772, DateTimeKind.Unspecified).AddTicks(2085), "$2a$11$qO7OAIX4n6kmRYmYMqlcMeagTdBbXklUXjvW72kDQ0G0dIEP67FlO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 8, 15, 44, 30, 941, DateTimeKind.Unspecified).AddTicks(22), "$2a$11$RcUlTjTgQ0V8Dubl09fXruIePdBfo/22fkDUTS5U/zNaKUoLKYRf6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weightmoved",
                table: "Repalletizations");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 7, 15, 42, 55, 853, DateTimeKind.Unspecified).AddTicks(8760), "$2a$11$WJGMIGjdRnNTKgtHG46oKueXZnQl1fj2.HX3aj9hwruvkkb7JW.qi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 7, 15, 42, 56, 88, DateTimeKind.Unspecified).AddTicks(1121), "$2a$11$gw4fs6WIy228MXHGeSFatu6V54uv7u3sdJBIWZ3ebt6jX2LIvutgW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 7, 15, 42, 56, 289, DateTimeKind.Unspecified).AddTicks(1117), "$2a$11$mIbzRIPst0NY3J7z55l39OqT3dDzSXe4pdZ3i/3B2yKVNA7kuHtPe" });
        }
    }
}
