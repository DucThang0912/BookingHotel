using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingHotel.Migrations
{
    /// <inheritdoc />
    public partial class AddAdultsAndChildren : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "RoomTypes",
                newName: "Children");

            migrationBuilder.AddColumn<int>(
                name: "Adults",
                table: "RoomTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Adults",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Children",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adults",
                table: "RoomTypes");

            migrationBuilder.DropColumn(
                name: "Adults",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Children",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "Children",
                table: "RoomTypes",
                newName: "Capacity");
        }
    }
}
