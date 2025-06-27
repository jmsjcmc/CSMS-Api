using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSMapi.Migrations
{
    /// <inheritdoc />
    public partial class _3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Weightmoved",
                table: "Repalletizationdetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 16, 14, 55, 28, 192, DateTimeKind.Unspecified).AddTicks(8498), "$2a$11$/Exx6lIjIekM58wNlZXVguo3.OI/HKAmSpAxexqGfw5/z/m4C.yKu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 16, 14, 55, 28, 376, DateTimeKind.Unspecified).AddTicks(1146), "$2a$11$5OA/mb2GL7iIHaUEuU96MeJERlo4QPo6brMDYjDw0q2zuqTqSYu/u" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 16, 14, 55, 28, 548, DateTimeKind.Unspecified).AddTicks(9605), "$2a$11$ed4QwLnlvvn8w2NjRxSMPuGfB/VSFHDKbHIW49spBKrpWV5e0OgFG" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weightmoved",
                table: "Repalletizationdetails");

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
    }
}
