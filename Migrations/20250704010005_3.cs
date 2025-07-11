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
            migrationBuilder.AddColumn<double>(
                name: "Duweight",
                table: "Receivingdetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duweight",
                table: "Receivingdetails");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 3, 15, 24, 10, 166, DateTimeKind.Unspecified).AddTicks(193), "$2a$11$qoP46X5nAPROzOSyqEAyJuesM5ZP.xYtkPcnrjbDMFU14KYsE/oXK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 3, 15, 24, 10, 334, DateTimeKind.Unspecified).AddTicks(4532), "$2a$11$RZVPulnNT77vU8FYmMyHpubXgw2OvKDnifYSVYDxu7Lw3PirtwlXa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 7, 3, 15, 24, 10, 505, DateTimeKind.Unspecified).AddTicks(8984), "$2a$11$Ug2JcGux0fCcFVInw7kqdOwfwJPjS3cpW.5pzSsgwXsV9t/IVNXA6" });
        }
    }
}
