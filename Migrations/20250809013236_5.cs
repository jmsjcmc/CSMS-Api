using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSMapi.Migrations
{
    /// <inheritdoc />
    public partial class _5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Approvedon",
                table: "Repalletizations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 9, 9, 32, 36, 85, DateTimeKind.Unspecified).AddTicks(8783), "$2a$11$wJKiq5MCbF53O.cRqrVfd.WRa2Oo9Qwy9nCgXn/NCzD/GP1q2LMQK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 9, 9, 32, 36, 256, DateTimeKind.Unspecified).AddTicks(7627), "$2a$11$Ok8VJqb.pSogMQyIpkjSjOxkyBWLTX0qApThkt2LxHOIp9hYE8zVe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 9, 9, 32, 36, 427, DateTimeKind.Unspecified).AddTicks(2377), "$2a$11$ucehgFOnbLfysvH3OjCBtedrK40ysqQVC96Qmvp55QlJAPydgteVO" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approvedon",
                table: "Repalletizations");

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
    }
}
