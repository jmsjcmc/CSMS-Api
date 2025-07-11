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
            migrationBuilder.AddColumn<int>(
                name: "Duquantity",
                table: "Receivingdetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 3, 15, 24, 10, 166, DateTimeKind.Unspecified).AddTicks(193), "$2a$11$qoP46X5nAPROzOSyqEAyJuesM5ZP.xYtkPcnrjbDMFU14KYsE/oXK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 3, 15, 24, 10, 334, DateTimeKind.Unspecified).AddTicks(4532), "$2a$11$RZVPulnNT77vU8FYmMyHpubXgw2OvKDnifYSVYDxu7Lw3PirtwlXa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 3, 15, 24, 10, 505, DateTimeKind.Unspecified).AddTicks(8984), "$2a$11$Ug2JcGux0fCcFVInw7kqdOwfwJPjS3cpW.5pzSsgwXsV9t/IVNXA6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duquantity",
                table: "Receivingdetails");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 1, 9, 48, 15, 544, DateTimeKind.Unspecified).AddTicks(2693), "$2a$11$rTgN2hzO.eTYKilpvZyxtuHKLpRhxhoUkxpXYUc9POoPI1xhPWIlG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 1, 9, 48, 15, 714, DateTimeKind.Unspecified).AddTicks(1847), "$2a$11$9rYgbQ94Ahpk92rTKFk3meZTpA5amF9CspSCotconJv5lKFUL5M8W" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 1, 9, 48, 15, 882, DateTimeKind.Unspecified).AddTicks(7563), "$2a$11$T.kiYx.zU/fY7HXE0aRkP.dSjNNhsk1T2mHtYgHN8fmIiL4fCkaTy" });
        }
    }
}
