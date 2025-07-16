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
            migrationBuilder.DropIndex(
                name: "IX_Repalletizationdetails_Receivingdetailid",
                table: "Repalletizationdetails");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Active", "Createdon", "Password" },
                values: new object[] { false, new DateTime(2025, 7, 15, 15, 47, 29, 222, DateTimeKind.Unspecified).AddTicks(2645), "$2a$11$7Wl072m8PyGCqdX6GUPBROu0O0hjka1F7y28yWf8Dhhzo.Exfx28." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Active", "Createdon", "Password" },
                values: new object[] { false, new DateTime(2025, 7, 15, 15, 47, 29, 414, DateTimeKind.Unspecified).AddTicks(5837), "$2a$11$UXvCrgZH2e0XlZPoC6XtL.0eTKNUXBWQlQLvLMG9WnaS7VU/YARMy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Active", "Createdon", "Password" },
                values: new object[] { false, new DateTime(2025, 7, 15, 15, 47, 29, 595, DateTimeKind.Unspecified).AddTicks(8722), "$2a$11$tzak.Yc5EAZlUkGrN7n7SOxxHDO1KaSGb4II0VyOxjdU65UDW.M/K" });

            migrationBuilder.CreateIndex(
                name: "IX_Repalletizationdetails_Receivingdetailid",
                table: "Repalletizationdetails",
                column: "Receivingdetailid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Repalletizationdetails_Receivingdetailid",
                table: "Repalletizationdetails");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Users");

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

            migrationBuilder.CreateIndex(
                name: "IX_Repalletizationdetails_Receivingdetailid",
                table: "Repalletizationdetails",
                column: "Receivingdetailid",
                unique: true);
        }
    }
}
