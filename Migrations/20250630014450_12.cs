using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSMapi.Migrations
{
    /// <inheritdoc />
    public partial class _12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Side",
                table: "Palletpositions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Column",
                table: "Palletpositions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 30, 9, 44, 49, 428, DateTimeKind.Unspecified).AddTicks(4695), "$2a$11$VNcNUhZEKuEir.VI2dgyIuv23hlivOMf.DhEVFCQP3MFnJ8WZbtC6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 30, 9, 44, 49, 791, DateTimeKind.Unspecified).AddTicks(173), "$2a$11$zz.VKEiTD4RbjnA3VZEr9.oePVKLSXHNzseFn7bHS2oZpqu9iX09W" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 30, 9, 44, 49, 994, DateTimeKind.Unspecified).AddTicks(5140), "$2a$11$MJeqht7newU2LJqu5xx5/ew4.DuK.7KPnpYxGZoFpFXUdeTfrJ.re" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Side",
                table: "Palletpositions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Column",
                table: "Palletpositions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 30, 9, 32, 37, 308, DateTimeKind.Unspecified).AddTicks(5068), "$2a$11$yN9t5QQcKA0SrTxAfeS8XOrSq5FZ1xcRsVYvEx4s02jJCS3s05rgG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 30, 9, 32, 37, 482, DateTimeKind.Unspecified).AddTicks(4793), "$2a$11$QcKJNagu5WDVO4cBjPafOuVSj26r3y2BMqxmHR3DzzkTR1JgbvOCC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 30, 9, 32, 37, 658, DateTimeKind.Unspecified).AddTicks(9668), "$2a$11$L2waGscoNU4dW22.XllZ/uT8jcf4tpWR2Tt5N3.quoZ5/E3aEJt2i" });
        }
    }
}
