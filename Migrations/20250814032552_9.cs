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
            migrationBuilder.AddColumn<DateTime>(
                name: "Approvedon",
                table: "Receivingdetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 14, 11, 25, 52, 204, DateTimeKind.Unspecified).AddTicks(5916), "$2a$11$8SN2n6VInSkiyGqQWqSjq.7oj8CiAsdgpWSutQ7tfLIou7DVn5syS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 14, 11, 25, 52, 371, DateTimeKind.Unspecified).AddTicks(9285), "$2a$11$Z4VYtVc2ToTNxN1Sy/Q5B.yb7P1LbTmTO85KiygHbdD8ZSf/2ADiG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 14, 11, 25, 52, 539, DateTimeKind.Unspecified).AddTicks(6895), "$2a$11$Kw5DxZFBog1PTku5MteHm.t84mSVAEIruL0eNNykaEbd6hWbSEj4a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approvedon",
                table: "Receivingdetails");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 14, 10, 3, 43, 743, DateTimeKind.Unspecified).AddTicks(1214), "$2a$11$tEDAweP0IJDMP8/Vy4nFe.MPMLBh/m0XnGboLZU.OXFc8jN.1s082" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 14, 10, 3, 43, 911, DateTimeKind.Unspecified).AddTicks(3665), "$2a$11$oIP1uiWNw.zpoe/wHgSCz.3wYNmNyjqS8OZgxFfmpGCwke6mRoOZO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 14, 10, 3, 44, 79, DateTimeKind.Unspecified).AddTicks(4534), "$2a$11$/aRNDZ/Hxnsi81b.Cd.6cOa/CQHku9IIR57I9Pj/.uQ.Zi/R2qdjq" });
        }
    }
}
