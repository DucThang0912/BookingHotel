using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingHotel.Migrations
{
    /// <inheritdoc />
    public partial class AddAdultsChildrenForCartItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Adults",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Children",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adults",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Children",
                table: "CartItems");
        }
    }
}
