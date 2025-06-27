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
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 16, 14, 19, 28, 380, DateTimeKind.Unspecified).AddTicks(2612), "$2a$11$sYmfwwgnMl0pDopdnhD/Huxso8U36AcP.58RUNACLsDVJ.zTxyO6G" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 16, 14, 19, 28, 548, DateTimeKind.Unspecified).AddTicks(926), "$2a$11$FQbC3AaYurgFBCUCqcRoSeGeuJmJ1bvFCm.y5yVIjp0OFgQpuA99." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 16, 14, 19, 28, 715, DateTimeKind.Unspecified).AddTicks(350), "$2a$11$gPRk.pFiJi3FIQwe7C0P0e6tQq7ZOo0bDD6JoVBdIGNOVaQsg0sta" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 16, 9, 18, 50, 319, DateTimeKind.Unspecified).AddTicks(1498), "$2a$11$p3ZHKynOJ4exuV5zk8yu3eWCsq7ms0RPQ0wXESwo/drpTmZbTMCue" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 16, 9, 18, 50, 504, DateTimeKind.Unspecified).AddTicks(420), "$2a$11$g5fqjHZ7edXcFVSNTV6x8eEDCRua/Q1mUbyU80Gifei5YsPNpOK3O" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 16, 9, 18, 50, 684, DateTimeKind.Unspecified).AddTicks(3991), "$2a$11$5NM/7itrfzPscz2s/dNORePrUcoezgDl2joBzzTZMmav1RBhAGBOm" });
        }
    }
}
