using Microsoft.EntityFrameworkCore.Migrations;

namespace AlgoTec.Migrations
{
    public partial class UpdateContractTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpacePropertyId",
                table: "Contracts",
                newName: "SubSpaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubSpaceId",
                table: "Contracts",
                newName: "SpacePropertyId");
        }
    }
}
