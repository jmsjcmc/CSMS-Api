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
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Contracts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 15, 16, 43, 13, 132, DateTimeKind.Unspecified).AddTicks(4990), "$2a$11$0VoOryZ/NmmmkjDKaes6M.tiYhpS3I8gSmBswwhCOkryFVypWG/g2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 15, 16, 43, 13, 298, DateTimeKind.Unspecified).AddTicks(7193), "$2a$11$KyrpStxp0TcuHEtDiECGkO18yYDHa9mc0IoXJAnVnJUHNalJ5gaTO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 15, 16, 43, 13, 462, DateTimeKind.Unspecified).AddTicks(6115), "$2a$11$MkU1qvh/7fFxCFX5EV9nfuFH46dn/GoTcZHN3fTnKKI9Z1Ztcog9G" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Contracts");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 15, 15, 47, 29, 222, DateTimeKind.Unspecified).AddTicks(2645), "$2a$11$7Wl072m8PyGCqdX6GUPBROu0O0hjka1F7y28yWf8Dhhzo.Exfx28." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 15, 15, 47, 29, 414, DateTimeKind.Unspecified).AddTicks(5837), "$2a$11$UXvCrgZH2e0XlZPoC6XtL.0eTKNUXBWQlQLvLMG9WnaS7VU/YARMy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 15, 15, 47, 29, 595, DateTimeKind.Unspecified).AddTicks(8722), "$2a$11$tzak.Yc5EAZlUkGrN7n7SOxxHDO1KaSGb4II0VyOxjdU65UDW.M/K" });
        }
    }
}
