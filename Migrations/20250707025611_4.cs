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
            migrationBuilder.AlterColumn<int>(
                name: "Palletno",
                table: "Pallets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Taggingnumber",
                table: "Pallets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Taggings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Taggingnumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Occupied = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Removed = table.Column<bool>(type: "bit", nullable: false),
                    PalletId = table.Column<int>(type: "int", nullable: false)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Taggings");

            migrationBuilder.DropColumn(
                name: "Taggingnumber",
                table: "Pallets");

            migrationBuilder.AlterColumn<int>(
                name: "Palletno",
                table: "Pallets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 4, 9, 0, 4, 659, DateTimeKind.Unspecified).AddTicks(6437), "$2a$11$IF6PQgU3eV8yLJ.f5g4cU.vF8Ny0SgIQuCYqB.bwafVa2YqzBNenW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 4, 9, 0, 4, 846, DateTimeKind.Unspecified).AddTicks(8264), "$2a$11$ctZilp.s/CjJ7gXc/k9V2uHxZmj7Wg6emEvhCKLmViz7hnNU6mfdy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 4, 9, 0, 5, 29, DateTimeKind.Unspecified).AddTicks(6163), "$2a$11$WrK8Y9Ej9VDx8go82CADtutnOycf4AtzloH3EzAgCroq6Lq8KYB8q" });
        }
    }
}
