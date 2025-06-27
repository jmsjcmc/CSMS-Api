using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSMapi.Migrations
{
    /// <inheritdoc />
    public partial class _4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 17, 8, 15, 56, 257, DateTimeKind.Unspecified).AddTicks(8601), "$2a$11$gkdiVD89c9xJjYEi36qkI.AsXs/l0IH7jQx8TI8Wc0LtR4WFMnQ/O" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 17, 8, 15, 56, 437, DateTimeKind.Unspecified).AddTicks(7373), "$2a$11$BcRfbievghszJFN1CIaJfOuOpPLxzMQLILc3Tm2TFmHeJ5q8Xoo6u" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 6, 17, 8, 15, 56, 615, DateTimeKind.Unspecified).AddTicks(5886), "$2a$11$WgwKWb5RY7f2Wh758xTiJuwpysbEMkF2Ggw1j6LjvfzHUTjH1hrtC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
