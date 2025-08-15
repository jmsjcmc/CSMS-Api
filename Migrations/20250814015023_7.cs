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
            migrationBuilder.AddColumn<DateTime>(
                name: "Updatedon",
                table: "Receivingdetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Updaterid",
                table: "Receivingdetails",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Receivingdetails_Updaterid",
                table: "Receivingdetails",
                column: "Updaterid");

            migrationBuilder.AddForeignKey(
                name: "FK_Receivingdetails_Users_Updaterid",
                table: "Receivingdetails",
                column: "Updaterid",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receivingdetails_Users_Updaterid",
                table: "Receivingdetails");

            migrationBuilder.DropIndex(
                name: "IX_Receivingdetails_Updaterid",
                table: "Receivingdetails");

            migrationBuilder.DropColumn(
                name: "Updatedon",
                table: "Receivingdetails");

            migrationBuilder.DropColumn(
                name: "Updaterid",
                table: "Receivingdetails");

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
    }
}
