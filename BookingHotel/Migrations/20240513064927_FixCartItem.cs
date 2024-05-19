using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingHotel.Migrations
{
    /// <inheritdoc />
    public partial class FixCartItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adults",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "CartItems",
                newName: "TotalGuests");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "CartItems",
                newName: "PricePerNight");

            migrationBuilder.RenameColumn(
                name: "Children",
                table: "CartItems",
                newName: "RoomTypeId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "TotalGuests",
                table: "CartItems",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "RoomTypeId",
                table: "CartItems",
                newName: "Children");

            migrationBuilder.RenameColumn(
                name: "PricePerNight",
                table: "CartItems",
                newName: "Price");

            migrationBuilder.AddColumn<int>(
                name: "Adults",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
