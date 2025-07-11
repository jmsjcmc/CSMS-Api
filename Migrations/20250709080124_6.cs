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
            migrationBuilder.DropColumn(
                name: "Productiondate",
                table: "Dispatchings");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "Dispatchings");

            migrationBuilder.AddColumn<DateTime>(
                name: "Productiondate",
                table: "Dispatchingdetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 9, 16, 1, 23, 654, DateTimeKind.Unspecified).AddTicks(5843), "$2a$11$jvvS2fALjNxwzlcjCWI1lezuMKG/nfv.NSKzEYOFBicXxU3.AVo12" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 9, 16, 1, 23, 895, DateTimeKind.Unspecified).AddTicks(8416), "$2a$11$ZALJWiG4MLpjgjdXv9YAq.g1AqHKnBGpj2DXMwB2FQ3qe1NsUTplq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 9, 16, 1, 24, 108, DateTimeKind.Unspecified).AddTicks(5112), "$2a$11$RlnJnAo7rhr9RUpPFe02TeTkV1mWuNuJFcP60GUmqF5abotuLbMJm" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Productiondate",
                table: "Dispatchingdetails");

            migrationBuilder.AddColumn<DateTime>(
                name: "Productiondate",
                table: "Dispatchings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Temperature",
                table: "Dispatchings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 7, 14, 12, 56, 908, DateTimeKind.Unspecified).AddTicks(8030), "$2a$11$HmBDid/vyjCDMzdyoGe1KeLEPAb4Ftw0TY6BVE2HoVJvrCNpCUbq2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 7, 14, 12, 57, 97, DateTimeKind.Unspecified).AddTicks(4036), "$2a$11$MUPzJbYh1rgk3.1LGI7txerVqoPdQ/uInPbtXoykDMGKgJonSzGiC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 7, 14, 12, 57, 278, DateTimeKind.Unspecified).AddTicks(2193), "$2a$11$Lu.evyefpX7Im9PoYtG6depKHudZA67Uredyy/ZFBbIWI.hBLGwE." });
        }
    }
}
