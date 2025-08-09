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
            migrationBuilder.AlterColumn<DateTime>(
                name: "Approvedon",
                table: "Repalletizations",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 9, 9, 51, 8, 757, DateTimeKind.Unspecified).AddTicks(4615), "$2a$11$fAsYphZKsu7CIITb7RPLLecNrcd/hpxWhR2ETPrtoH3cH4urb7nm." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 9, 9, 51, 8, 929, DateTimeKind.Unspecified).AddTicks(8846), "$2a$11$/5n83MzAJ8oct6Cp.tES5euRcjKJn/rlnwR.C9XtHM4gJ.mAisPe." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 9, 9, 51, 9, 103, DateTimeKind.Unspecified).AddTicks(4658), "$2a$11$2S74U/peKVuYMWwkt29tNuZo2680mwrRSEc4iKyxZFRROuMmaW1Ce" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Approvedon",
                table: "Repalletizations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

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
    }
}
