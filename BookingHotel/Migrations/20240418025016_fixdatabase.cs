using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingHotel.Migrations
{
    /// <inheritdoc />
    public partial class fixdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomServices_Hotels_HotelId",
                table: "RoomServices");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomServices_Services_ServiceId",
                table: "RoomServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomServices",
                table: "RoomServices");

            migrationBuilder.RenameTable(
                name: "RoomServices",
                newName: "HotelServices");

            migrationBuilder.RenameIndex(
                name: "IX_RoomServices_ServiceId",
                table: "HotelServices",
                newName: "IX_HotelServices_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomServices_HotelId",
                table: "HotelServices",
                newName: "IX_HotelServices_HotelId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelServices",
                table: "HotelServices",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelServices_Hotels_HotelId",
                table: "HotelServices",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelServices_Services_ServiceId",
                table: "HotelServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelServices_Hotels_HotelId",
                table: "HotelServices");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelServices_Services_ServiceId",
                table: "HotelServices");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelServices",
                table: "HotelServices");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reviews");

            migrationBuilder.RenameTable(
                name: "HotelServices",
                newName: "RoomServices");

            migrationBuilder.RenameIndex(
                name: "IX_HotelServices_ServiceId",
                table: "RoomServices",
                newName: "IX_RoomServices_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelServices_HotelId",
                table: "RoomServices",
                newName: "IX_RoomServices_HotelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomServices",
                table: "RoomServices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomServices_Hotels_HotelId",
                table: "RoomServices",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomServices_Services_ServiceId",
                table: "RoomServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }
    }
}
