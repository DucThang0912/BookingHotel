using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingHotel.Migrations
{
    /// <inheritdoc />
    public partial class fixBookingDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingsDetail_Rooms_RoomId",
                table: "BookingsDetail");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "BookingsDetail",
                newName: "RoomTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_BookingsDetail_RoomId",
                table: "BookingsDetail",
                newName: "IX_BookingsDetail_RoomTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingsDetail_RoomTypes_RoomTypeId",
                table: "BookingsDetail",
                column: "RoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingsDetail_RoomTypes_RoomTypeId",
                table: "BookingsDetail");

            migrationBuilder.RenameColumn(
                name: "RoomTypeId",
                table: "BookingsDetail",
                newName: "RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_BookingsDetail_RoomTypeId",
                table: "BookingsDetail",
                newName: "IX_BookingsDetail_RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingsDetail_Rooms_RoomId",
                table: "BookingsDetail",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
