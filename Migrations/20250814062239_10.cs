using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSMapi.Migrations
{
    /// <inheritdoc />
    public partial class _10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Csmovements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Receivingdetailid = table.Column<int>(type: "int", nullable: false),
                    Frompositionid = table.Column<int>(type: "int", nullable: false),
                    Topositionid = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Csmovements", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 14, 14, 22, 38, 633, DateTimeKind.Unspecified).AddTicks(5606), "$2a$11$o04FcC/jXQsuVXAi54ZczutFJODqwjqaF5Q4Y1EoOFJJ2d5N9.Yqu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 14, 14, 22, 38, 824, DateTimeKind.Unspecified).AddTicks(4716), "$2a$11$Jlgn0vjXGA24QkyLF4W5JeGe9uzk2wrtqbxuE5Moj/AL/Aa/ngETK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 14, 14, 22, 39, 26, DateTimeKind.Unspecified).AddTicks(1973), "$2a$11$flgYeVzzbEyQ68isUZkhru5WP8ushf0jd4XijFleY.zKDmX47Mkl6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Csmovements");

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
    }
}
