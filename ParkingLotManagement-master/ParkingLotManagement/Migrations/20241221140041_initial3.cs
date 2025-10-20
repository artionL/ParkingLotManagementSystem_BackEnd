using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingLotManagement.Migrations
{
    /// <inheritdoc />
    public partial class initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegularSpots",
                table: "ParkingSpots");

            migrationBuilder.DropColumn(
                name: "ReservedSpots",
                table: "ParkingSpots");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegularSpots",
                table: "ParkingSpots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReservedSpots",
                table: "ParkingSpots",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
