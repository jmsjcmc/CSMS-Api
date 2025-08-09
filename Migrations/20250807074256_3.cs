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
            migrationBuilder.DropForeignKey(
                name: "FK_Repalletizations_Receivingdetails_Frompalletid",
                table: "Repalletizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Repalletizations_Receivingdetails_Topalletid",
                table: "Repalletizations");

            migrationBuilder.RenameColumn(
                name: "Topalletid",
                table: "Repalletizations",
                newName: "Toreceivingdetailtid");

            migrationBuilder.RenameColumn(
                name: "Frompalletid",
                table: "Repalletizations",
                newName: "Fromreceivingdetailid");

            migrationBuilder.RenameIndex(
                name: "IX_Repalletizations_Topalletid",
                table: "Repalletizations",
                newName: "IX_Repalletizations_Toreceivingdetailtid");

            migrationBuilder.RenameIndex(
                name: "IX_Repalletizations_Frompalletid",
                table: "Repalletizations",
                newName: "IX_Repalletizations_Fromreceivingdetailid");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 7, 15, 42, 55, 853, DateTimeKind.Unspecified).AddTicks(8760), "$2a$11$WJGMIGjdRnNTKgtHG46oKueXZnQl1fj2.HX3aj9hwruvkkb7JW.qi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 7, 15, 42, 56, 88, DateTimeKind.Unspecified).AddTicks(1121), "$2a$11$gw4fs6WIy228MXHGeSFatu6V54uv7u3sdJBIWZ3ebt6jX2LIvutgW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 7, 15, 42, 56, 289, DateTimeKind.Unspecified).AddTicks(1117), "$2a$11$mIbzRIPst0NY3J7z55l39OqT3dDzSXe4pdZ3i/3B2yKVNA7kuHtPe" });

            migrationBuilder.AddForeignKey(
                name: "FK_Repalletizations_Receivingdetails_Fromreceivingdetailid",
                table: "Repalletizations",
                column: "Fromreceivingdetailid",
                principalTable: "Receivingdetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Repalletizations_Receivingdetails_Toreceivingdetailtid",
                table: "Repalletizations",
                column: "Toreceivingdetailtid",
                principalTable: "Receivingdetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repalletizations_Receivingdetails_Fromreceivingdetailid",
                table: "Repalletizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Repalletizations_Receivingdetails_Toreceivingdetailtid",
                table: "Repalletizations");

            migrationBuilder.RenameColumn(
                name: "Toreceivingdetailtid",
                table: "Repalletizations",
                newName: "Topalletid");

            migrationBuilder.RenameColumn(
                name: "Fromreceivingdetailid",
                table: "Repalletizations",
                newName: "Frompalletid");

            migrationBuilder.RenameIndex(
                name: "IX_Repalletizations_Toreceivingdetailtid",
                table: "Repalletizations",
                newName: "IX_Repalletizations_Topalletid");

            migrationBuilder.RenameIndex(
                name: "IX_Repalletizations_Fromreceivingdetailid",
                table: "Repalletizations",
                newName: "IX_Repalletizations_Frompalletid");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 7, 11, 18, 10, 2, DateTimeKind.Unspecified).AddTicks(9844), "$2a$11$l9kk.7USQQWdbMczxSyPMOR.9H.MdLbeavKl6PdRAxTzFoMjZ6pAG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 7, 11, 18, 10, 185, DateTimeKind.Unspecified).AddTicks(1446), "$2a$11$z41ww/Xm9wEaBrOh.YWK/udY740SFXJ46xPwrnGqD3LLVxAJ2cG.u" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Createdon", "Password" },
                values: new object[] { new DateTime(2025, 8, 7, 11, 18, 10, 377, DateTimeKind.Unspecified).AddTicks(7701), "$2a$11$rbP3ruOwQ3Dl4XvYZYHM6OCEXD/PKa.JsXdoehrtKXQo.FZA0q5Xm" });

            migrationBuilder.AddForeignKey(
                name: "FK_Repalletizations_Receivingdetails_Frompalletid",
                table: "Repalletizations",
                column: "Frompalletid",
                principalTable: "Receivingdetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Repalletizations_Receivingdetails_Topalletid",
                table: "Repalletizations",
                column: "Topalletid",
                principalTable: "Receivingdetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
