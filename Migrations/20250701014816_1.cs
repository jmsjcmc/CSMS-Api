using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CSMapi.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Removed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coldstorages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Csnumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coldstorages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Companyname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Companyaddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Companyemail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Companynumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Removed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Documentno = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rolename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Removed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Businessunit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Businessunitlocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Esignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Createdon = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updatedon = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Removed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Palletpositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Csid = table.Column<int>(type: "int", nullable: false),
                    Wing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Floor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Column = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Side = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hidden = table.Column<bool>(type: "bit", nullable: false),
                    Removed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Palletpositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Palletpositions_Coldstorages_Csid",
                        column: x => x.Csid,
                        principalTable: "Coldstorages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Categoryid = table.Column<int>(type: "int", nullable: false),
                    Customerid = table.Column<int>(type: "int", nullable: false),
                    Productcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Productname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Variant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sku = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Productpackaging = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deliveryunit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Uom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Removed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_Categoryid",
                        column: x => x.Categoryid,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Customers_Customerid",
                        column: x => x.Customerid,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Agreementdate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lessorcompany = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lessorrepresentative = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lessorrepresentativeposition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lessorcompanylocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lesseecompany = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lesseerepresentative = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lesseerepresentativeposition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lesseecompanylocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Startlease = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endlease = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notarylocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lessoridtype = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lessoriddetail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lesseeidtype = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lesseeiddetail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sealdate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Createdon = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Creatorid = table.Column<int>(type: "int", nullable: false),
                    Updatedon = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Removed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Users_Creatorid",
                        column: x => x.Creatorid,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pallets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pallettype = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Palletno = table.Column<int>(type: "int", nullable: false),
                    Creatorid = table.Column<int>(type: "int", nullable: false),
                    Createdon = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updatedon = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Occupied = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Removed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pallets_Users_Creatorid",
                        column: x => x.Creatorid,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Repalletizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Frompalletid = table.Column<int>(type: "int", nullable: false),
                    Topalletid = table.Column<int>(type: "int", nullable: false),
                    Createdon = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Creatorid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repalletizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repalletizations_Users_Creatorid",
                        column: x => x.Creatorid,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dispatchings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Productid = table.Column<int>(type: "int", nullable: false),
                    Documentid = table.Column<int>(type: "int", nullable: false),
                    Dispatchdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dispatchtimestart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dispatchtimeend = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nmiscertificate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dispatchplateno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sealno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Overallweight = table.Column<double>(type: "float", nullable: false),
                    Temperature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Productiondate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Requestorid = table.Column<int>(type: "int", nullable: false),
                    Approverid = table.Column<int>(type: "int", nullable: true),
                    Createdon = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Approvedon = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Declinedon = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updatedon = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Dispatched = table.Column<bool>(type: "bit", nullable: false),
                    Declined = table.Column<bool>(type: "bit", nullable: false),
                    Pending = table.Column<bool>(type: "bit", nullable: false),
                    Removed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dispatchings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dispatchings_Documents_Documentid",
                        column: x => x.Documentid,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dispatchings_Products_Productid",
                        column: x => x.Productid,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dispatchings_Users_Approverid",
                        column: x => x.Approverid,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Dispatchings_Users_Requestorid",
                        column: x => x.Requestorid,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receivings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Documentid = table.Column<int>(type: "int", nullable: false),
                    Productid = table.Column<int>(type: "int", nullable: false),
                    Expirationdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cvnumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Platenumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Arrivaldate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Unloadingstart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unloadingend = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Overallweight = table.Column<double>(type: "float", nullable: false),
                    Receivingform = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Requestorid = table.Column<int>(type: "int", nullable: false),
                    Approverid = table.Column<int>(type: "int", nullable: true),
                    Createdon = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updatedon = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Datereceived = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Datedeclined = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Pending = table.Column<bool>(type: "bit", nullable: false),
                    Received = table.Column<bool>(type: "bit", nullable: false),
                    Declined = table.Column<bool>(type: "bit", nullable: false),
                    Removed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receivings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receivings_Documents_Documentid",
                        column: x => x.Documentid,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receivings_Products_Productid",
                        column: x => x.Productid,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receivings_Users_Approverid",
                        column: x => x.Approverid,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Receivings_Users_Requestorid",
                        column: x => x.Requestorid,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Leasedpremises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contractid = table.Column<int>(type: "int", nullable: false),
                    Facility = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specific = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leasedpremises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leasedpremises_Contracts_Contractid",
                        column: x => x.Contractid,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receivingdetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Receivingid = table.Column<int>(type: "int", nullable: false),
                    Palletid = table.Column<int>(type: "int", nullable: false),
                    Positionid = table.Column<int>(type: "int", nullable: false),
                    Productiondate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantityinapallet = table.Column<int>(type: "int", nullable: false),
                    Totalweight = table.Column<double>(type: "float", nullable: false),
                    Received = table.Column<bool>(type: "bit", nullable: false),
                    Partialdispatched = table.Column<bool>(type: "bit", nullable: false),
                    Fulldispatched = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receivingdetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receivingdetails_Palletpositions_Positionid",
                        column: x => x.Positionid,
                        principalTable: "Palletpositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receivingdetails_Pallets_Palletid",
                        column: x => x.Palletid,
                        principalTable: "Pallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receivingdetails_Receivings_Receivingid",
                        column: x => x.Receivingid,
                        principalTable: "Receivings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dispatchingdetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Receivingdetailid = table.Column<int>(type: "int", nullable: false),
                    Dispatchingid = table.Column<int>(type: "int", nullable: false),
                    Palletid = table.Column<int>(type: "int", nullable: false),
                    Positionid = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Totalweight = table.Column<double>(type: "float", nullable: false),
                    Partialdispatched = table.Column<bool>(type: "bit", nullable: false),
                    Fulldispatched = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dispatchingdetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dispatchingdetails_Dispatchings_Dispatchingid",
                        column: x => x.Dispatchingid,
                        principalTable: "Dispatchings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dispatchingdetails_Palletpositions_Positionid",
                        column: x => x.Positionid,
                        principalTable: "Palletpositions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Dispatchingdetails_Pallets_Palletid",
                        column: x => x.Palletid,
                        principalTable: "Pallets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Dispatchingdetails_Receivingdetails_Receivingdetailid",
                        column: x => x.Receivingdetailid,
                        principalTable: "Receivingdetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Repalletizationdetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Repalletizationid = table.Column<int>(type: "int", nullable: false),
                    Receivingdetailid = table.Column<int>(type: "int", nullable: false),
                    Quantitymoved = table.Column<int>(type: "int", nullable: false),
                    Weightmoved = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repalletizationdetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repalletizationdetails_Receivingdetails_Receivingdetailid",
                        column: x => x.Receivingdetailid,
                        principalTable: "Receivingdetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Repalletizationdetails_Repalletizations_Repalletizationid",
                        column: x => x.Repalletizationid,
                        principalTable: "Repalletizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Coldstorages",
                columns: new[] { "Id", "Active", "Csnumber" },
                values: new object[,]
                {
                    { 1, true, "1" },
                    { 2, true, "2" },
                    { 3, true, "3" },
                    { 4, true, "4" },
                    { 5, true, "5" },
                    { 6, true, "6" },
                    { 7, true, "7" },
                    { 8, true, "8" },
                    { 9, true, "9" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Removed", "Rolename" },
                values: new object[,]
                {
                    { 1, false, "Admin" },
                    { 2, false, "User" },
                    { 3, false, "Approver" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Businessunit", "Businessunitlocation", "Createdon", "Department", "Esignature", "Firstname", "Lastname", "Password", "Position", "Removed", "Role", "Updatedon", "Username" },
                values: new object[,]
                {
                    { 1, "ABFI Central Office", "Binugao, Toril, Davao City", new DateTime(2025, 7, 1, 9, 48, 15, 544, DateTimeKind.Unspecified).AddTicks(2693), "Cisdevo", null, "James Jecemeco", "Tabilog", "$2a$11$rTgN2hzO.eTYKilpvZyxtuHKLpRhxhoUkxpXYUc9POoPI1xhPWIlG", "Software Developer", false, "Admin, User, Approver", null, "211072" },
                    { 2, "SubZero Ice and Cold Storage Inc", "Binugao, Toril, Davao City", new DateTime(2025, 7, 1, 9, 48, 15, 714, DateTimeKind.Unspecified).AddTicks(1847), "Executive", null, "Shiela", "Hernando", "$2a$11$9rYgbQ94Ahpk92rTKFk3meZTpA5amF9CspSCotconJv5lKFUL5M8W", "Senior Operations Manager", false, "Approver", null, "211073" },
                    { 3, "ABFI Central Office", "Binugao, Toril, Davao City", new DateTime(2025, 7, 1, 9, 48, 15, 882, DateTimeKind.Unspecified).AddTicks(7563), "Cisdevo", null, "Jerecho", "Asilum", "$2a$11$T.kiYx.zU/fY7HXE0aRkP.dSjNNhsk1T2mHtYgHN8fmIiL4fCkaTy", "Software Developer", false, "Admin, User, Approver", null, "211028" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_Creatorid",
                table: "Contracts",
                column: "Creatorid");

            migrationBuilder.CreateIndex(
                name: "IX_Dispatchingdetails_Dispatchingid",
                table: "Dispatchingdetails",
                column: "Dispatchingid");

            migrationBuilder.CreateIndex(
                name: "IX_Dispatchingdetails_Palletid",
                table: "Dispatchingdetails",
                column: "Palletid");

            migrationBuilder.CreateIndex(
                name: "IX_Dispatchingdetails_Positionid",
                table: "Dispatchingdetails",
                column: "Positionid");

            migrationBuilder.CreateIndex(
                name: "IX_Dispatchingdetails_Receivingdetailid",
                table: "Dispatchingdetails",
                column: "Receivingdetailid");

            migrationBuilder.CreateIndex(
                name: "IX_Dispatchings_Approverid",
                table: "Dispatchings",
                column: "Approverid");

            migrationBuilder.CreateIndex(
                name: "IX_Dispatchings_Documentid",
                table: "Dispatchings",
                column: "Documentid");

            migrationBuilder.CreateIndex(
                name: "IX_Dispatchings_Productid",
                table: "Dispatchings",
                column: "Productid");

            migrationBuilder.CreateIndex(
                name: "IX_Dispatchings_Requestorid",
                table: "Dispatchings",
                column: "Requestorid");

            migrationBuilder.CreateIndex(
                name: "IX_Leasedpremises_Contractid",
                table: "Leasedpremises",
                column: "Contractid");

            migrationBuilder.CreateIndex(
                name: "IX_Palletpositions_Csid",
                table: "Palletpositions",
                column: "Csid");

            migrationBuilder.CreateIndex(
                name: "IX_Pallets_Creatorid",
                table: "Pallets",
                column: "Creatorid");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Categoryid",
                table: "Products",
                column: "Categoryid");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Customerid",
                table: "Products",
                column: "Customerid");

            migrationBuilder.CreateIndex(
                name: "IX_Receivingdetails_Palletid",
                table: "Receivingdetails",
                column: "Palletid");

            migrationBuilder.CreateIndex(
                name: "IX_Receivingdetails_Positionid",
                table: "Receivingdetails",
                column: "Positionid");

            migrationBuilder.CreateIndex(
                name: "IX_Receivingdetails_Receivingid",
                table: "Receivingdetails",
                column: "Receivingid");

            migrationBuilder.CreateIndex(
                name: "IX_Receivings_Approverid",
                table: "Receivings",
                column: "Approverid");

            migrationBuilder.CreateIndex(
                name: "IX_Receivings_Documentid",
                table: "Receivings",
                column: "Documentid");

            migrationBuilder.CreateIndex(
                name: "IX_Receivings_Productid",
                table: "Receivings",
                column: "Productid");

            migrationBuilder.CreateIndex(
                name: "IX_Receivings_Requestorid",
                table: "Receivings",
                column: "Requestorid");

            migrationBuilder.CreateIndex(
                name: "IX_Repalletizationdetails_Receivingdetailid",
                table: "Repalletizationdetails",
                column: "Receivingdetailid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Repalletizationdetails_Repalletizationid",
                table: "Repalletizationdetails",
                column: "Repalletizationid");

            migrationBuilder.CreateIndex(
                name: "IX_Repalletizations_Creatorid",
                table: "Repalletizations",
                column: "Creatorid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dispatchingdetails");

            migrationBuilder.DropTable(
                name: "Leasedpremises");

            migrationBuilder.DropTable(
                name: "Repalletizationdetails");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Dispatchings");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Receivingdetails");

            migrationBuilder.DropTable(
                name: "Repalletizations");

            migrationBuilder.DropTable(
                name: "Palletpositions");

            migrationBuilder.DropTable(
                name: "Pallets");

            migrationBuilder.DropTable(
                name: "Receivings");

            migrationBuilder.DropTable(
                name: "Coldstorages");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
