using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingHotel.Migrations
{
    /// <inheritdoc />
    public partial class FixBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckInDate",
                table: "BookingRoom");

            migrationBuilder.DropColumn(
                name: "CheckOutDate",
                table: "BookingRoom");

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInDate",
                table: "Bookings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOutDate",
                table: "Bookings",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckInDate",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CheckOutDate",
                table: "Bookings");

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInDate",
                table: "BookingRoom",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOutDate",
                table: "BookingRoom",
                type: "datetime2",
                nullable: true);
        }
    }
}
