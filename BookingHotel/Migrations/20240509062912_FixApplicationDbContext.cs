using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingHotel.Migrations
{
    /// <inheritdoc />
    public partial class FixApplicationDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomTypeImage_RoomTypes_RoomTypeId",
                table: "RoomTypeImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomTypeImage",
                table: "RoomTypeImage");

            migrationBuilder.RenameTable(
                name: "RoomTypeImage",
                newName: "RoomTypeImages");

            migrationBuilder.RenameIndex(
                name: "IX_RoomTypeImage_RoomTypeId",
                table: "RoomTypeImages",
                newName: "IX_RoomTypeImages_RoomTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomTypeImages",
                table: "RoomTypeImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTypeImages_RoomTypes_RoomTypeId",
                table: "RoomTypeImages",
                column: "RoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomTypeImages_RoomTypes_RoomTypeId",
                table: "RoomTypeImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomTypeImages",
                table: "RoomTypeImages");

            migrationBuilder.RenameTable(
                name: "RoomTypeImages",
                newName: "RoomTypeImage");

            migrationBuilder.RenameIndex(
                name: "IX_RoomTypeImages_RoomTypeId",
                table: "RoomTypeImage",
                newName: "IX_RoomTypeImage_RoomTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomTypeImage",
                table: "RoomTypeImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTypeImage_RoomTypes_RoomTypeId",
                table: "RoomTypeImage",
                column: "RoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
