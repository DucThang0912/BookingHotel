using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingHotel.Migrations
{
    /// <inheritdoc />
    public partial class BigUpdateBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingRoom_Bookings_BookingId",
                table: "BookingRoom");

            migrationBuilder.DropIndex(
                name: "IX_BookingRoom_BookingId",
                table: "BookingRoom");

            migrationBuilder.DropColumn(
                name: "Adults",
                table: "BookingRoom");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "BookingRoom");

            migrationBuilder.DropColumn(
                name: "CheckInDate",
                table: "BookingRoom");

            migrationBuilder.DropColumn(
                name: "CheckOutDate",
                table: "BookingRoom");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "BookingRoom");

            migrationBuilder.RenameColumn(
                name: "OrderName",
                table: "Bookings",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "Children",
                table: "BookingRoom",
                newName: "RoomBookingId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RoomBooking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    RoomTypeId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Adults = table.Column<int>(type: "int", nullable: false),
                    Children = table.Column<int>(type: "int", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomBooking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomBooking_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomBooking_RoomTypes_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingRoom_RoomBookingId",
                table: "BookingRoom",
                column: "RoomBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomBooking_BookingId",
                table: "RoomBooking",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomBooking_RoomTypeId",
                table: "RoomBooking",
                column: "RoomTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingRoom_RoomBooking_RoomBookingId",
                table: "BookingRoom",
                column: "RoomBookingId",
                principalTable: "RoomBooking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingRoom_RoomBooking_RoomBookingId",
                table: "BookingRoom");

            migrationBuilder.DropTable(
                name: "RoomBooking");

            migrationBuilder.DropIndex(
                name: "IX_BookingRoom_RoomBookingId",
                table: "BookingRoom");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Bookings",
                newName: "OrderName");

            migrationBuilder.RenameColumn(
                name: "RoomBookingId",
                table: "BookingRoom",
                newName: "Children");

            migrationBuilder.AddColumn<int>(
                name: "Adults",
                table: "BookingRoom",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "BookingRoom",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInDate",
                table: "BookingRoom",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOutDate",
                table: "BookingRoom",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "BookingRoom",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_BookingRoom_BookingId",
                table: "BookingRoom",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingRoom_Bookings_BookingId",
                table: "BookingRoom",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
