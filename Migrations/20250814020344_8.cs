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
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Receivingdetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Receivingdetails");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 14, 9, 50, 22, 801, DateTimeKind.Unspecified).AddTicks(3843), "$2a$11$YagPWD1FI1m5SCwHSSm1oOoSzxHRjvh1Hpi/th7P05bdU2Kfmz26K" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 14, 9, 50, 23, 9, DateTimeKind.Unspecified).AddTicks(8339), "$2a$11$nO4IjrRq8Gv.KhOFZRO3YO.zyjVJKyH05tvYh7XTEMuYWeIK.dLf." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 14, 9, 50, 23, 201, DateTimeKind.Unspecified).AddTicks(4152), "$2a$11$pQ6ZsOeEaMtSiNAXjk5CHumv2NdOi7xzGo3wAQ1zePhbZieFLUzXq" });
        }
    }
}
