using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSMapi.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Receivingdetails");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 7, 11, 18, 10, 2, DateTimeKind.Unspecified).AddTicks(9844), "$2a$11$l9kk.7USQQWdbMczxSyPMOR.9H.MdLbeavKl6PdRAxTzFoMjZ6pAG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 7, 11, 18, 10, 185, DateTimeKind.Unspecified).AddTicks(1446), "$2a$11$z41ww/Xm9wEaBrOh.YWK/udY740SFXJ46xPwrnGqD3LLVxAJ2cG.u" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 7, 11, 18, 10, 377, DateTimeKind.Unspecified).AddTicks(7701), "$2a$11$rbP3ruOwQ3Dl4XvYZYHM6OCEXD/PKa.JsXdoehrtKXQo.FZA0q5Xm" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Receivingdetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 6, 16, 39, 31, 705, DateTimeKind.Unspecified).AddTicks(4517), "$2a$11$IO86g6JZr1qc4I/0OypDtOWbxgTT9a6xXCasjiquhtuqvvcmAOBJi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 6, 16, 39, 31, 883, DateTimeKind.Unspecified).AddTicks(7235), "$2a$11$nYCIgjidAyMGTooyu658QOcMcEQmYcLon3KpLZG48jomJWBxVhSBm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 6, 16, 39, 32, 66, DateTimeKind.Unspecified).AddTicks(5767), "$2a$11$86xDJ8tQavWoMvWVrBRAa./.66nqQPp07EyFj08sNh54DYDMGnD5W" });
        }
    }
}
