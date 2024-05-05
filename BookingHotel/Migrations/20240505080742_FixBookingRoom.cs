using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingHotel.Migrations
{
    /// <inheritdoc />
    public partial class FixBookingRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adults",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CheckInDate",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CheckOutDate",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Children",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "Adults",
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

            migrationBuilder.AddColumn<int>(
                name: "Children",
                table: "BookingRoom",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adults",
                table: "BookingRoom");

            migrationBuilder.DropColumn(
                name: "CheckInDate",
                table: "BookingRoom");

            migrationBuilder.DropColumn(
                name: "CheckOutDate",
                table: "BookingRoom");

            migrationBuilder.DropColumn(
                name: "Children",
                table: "BookingRoom");

            migrationBuilder.AddColumn<int>(
                name: "Adults",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<int>(
                name: "Children",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
