using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlgoTecture.Migrations
{
    public partial class ChangeContractTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Contracts",
                newName: "DeclarationDateTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractDateTime",
                table: "Contracts",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractDateTime",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "DeclarationDateTime",
                table: "Contracts",
                newName: "CreatedDate");
        }
    }
}
