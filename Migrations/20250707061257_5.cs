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
            migrationBuilder.DropTable(
                name: "Taggings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Startlease",
                table: "Contracts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Endlease",
                table: "Contracts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 7, 14, 12, 56, 908, DateTimeKind.Unspecified).AddTicks(8030), "$2a$11$HmBDid/vyjCDMzdyoGe1KeLEPAb4Ftw0TY6BVE2HoVJvrCNpCUbq2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 7, 14, 12, 57, 97, DateTimeKind.Unspecified).AddTicks(4036), "$2a$11$MUPzJbYh1rgk3.1LGI7txerVqoPdQ/uInPbtXoykDMGKgJonSzGiC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 7, 14, 12, 57, 278, DateTimeKind.Unspecified).AddTicks(2193), "$2a$11$Lu.evyefpX7Im9PoYtG6depKHudZA67Uredyy/ZFBbIWI.hBLGwE." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Startlease",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Endlease",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "Taggings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PalletId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Occupied = table.Column<bool>(type: "bit", nullable: false),
                    Removed = table.Column<bool>(type: "bit", nullable: false),
                    Taggingnumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taggings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Taggings_Pallets_PalletId",
                        column: x => x.PalletId,
                        principalTable: "Pallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 7, 10, 56, 11, 239, DateTimeKind.Unspecified).AddTicks(8336), "$2a$11$7LeZFvnUVCcb1De4hRm2l.SxbYTOiOOt3mjwR5/PeUPATBAJctxla" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 7, 10, 56, 11, 415, DateTimeKind.Unspecified).AddTicks(4263), "$2a$11$gsAbJls5AF3sf8VbaYLiW.g6ISIhxenJOdVNh3KKlBS04oscne7r2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 7, 10, 56, 11, 595, DateTimeKind.Unspecified).AddTicks(2100), "$2a$11$OIKe1w5GvIuGRVqSOsGi3eDiypm5v9.vhhyhamg8vUxtSjYwMhoHi" });

            migrationBuilder.CreateIndex(
                name: "IX_Taggings_PalletId",
                table: "Taggings",
                column: "PalletId");
        }
    }
}
