using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Algotecture.Data.Persistence.Ef.Migrations
{
    public partial class AddReservationUniqueIdentifier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReservationUniqueIdentifier",
                table: "Reservations",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationUniqueIdentifier",
                table: "Reservations");
        }
    }
}
