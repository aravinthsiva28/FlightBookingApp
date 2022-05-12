using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AirlineService.Migrations
{
    public partial class initail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airlinetbl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Contact = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airlinetbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discounttbl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DiscountCode = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounttbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usertbl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usertbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flight_shedule_tbl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AirlineId = table.Column<int>(nullable: false),
                    FromPlace = table.Column<string>(nullable: false),
                    ToPlace = table.Column<string>(nullable: false),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    SheduledDay = table.Column<string>(nullable: true),
                    InstrumentUsed = table.Column<string>(nullable: true),
                    TotalBCSeats = table.Column<int>(nullable: false),
                    TotalNBCSeats = table.Column<int>(nullable: false),
                    BcTicketCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NBcTicketCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MealType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight_shedule_tbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flight_shedule_tbl_Airlinetbl_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airlinetbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookingtbl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PNR = table.Column<string>(nullable: true),
                    FlightId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    EmailId = table.Column<string>(nullable: false),
                    NoOfSeats = table.Column<int>(nullable: false),
                    JourneyType = table.Column<string>(nullable: true),
                    ReturnDate = table.Column<DateTime>(nullable: true),
                    BookingStatus = table.Column<string>(nullable: true),
                    ClassType = table.Column<string>(nullable: true),
                    TotalCost = table.Column<double>(nullable: false),
                    DiscountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookingtbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookingtbl_Discounttbl_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounttbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookingtbl_Flight_shedule_tbl_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flight_shedule_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookingtbl_Usertbl_UserId",
                        column: x => x.UserId,
                        principalTable: "Usertbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Passengertbl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookingId = table.Column<int>(nullable: false),
                    PassengerName = table.Column<string>(nullable: false),
                    Gender = table.Column<string>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    SeatNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengertbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passengertbl_Bookingtbl_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookingtbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookingtbl_DiscountId",
                table: "Bookingtbl",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookingtbl_FlightId",
                table: "Bookingtbl",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookingtbl_UserId",
                table: "Bookingtbl",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_shedule_tbl_AirlineId",
                table: "Flight_shedule_tbl",
                column: "AirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_Passengertbl_BookingId",
                table: "Passengertbl",
                column: "BookingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Passengertbl");

            migrationBuilder.DropTable(
                name: "Bookingtbl");

            migrationBuilder.DropTable(
                name: "Discounttbl");

            migrationBuilder.DropTable(
                name: "Flight_shedule_tbl");

            migrationBuilder.DropTable(
                name: "Usertbl");

            migrationBuilder.DropTable(
                name: "Airlinetbl");
        }
    }
}
