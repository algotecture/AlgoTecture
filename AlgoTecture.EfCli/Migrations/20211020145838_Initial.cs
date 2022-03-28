using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlgoTecture.Database.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TypeOfSpaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfSpaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Surname = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Patronymic = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UtilizationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilizationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spaces",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypeOfSpaceId = table.Column<int>(type: "INTEGER", nullable: false),
                    SpaceAddress = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Latitude = table.Column<double>(type: "REAL", nullable: false),
                    Longitude = table.Column<double>(type: "REAL", nullable: false),
                    SpaceProperty = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spaces_TypeOfSpaces_TypeOfSpaceId",
                        column: x => x.TypeOfSpaceId,
                        principalTable: "TypeOfSpaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAuthentications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<long>(type: "INTEGER", nullable: false),
                    HashedPassword = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAuthentications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAuthentications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OwnerUserId = table.Column<long>(type: "INTEGER", nullable: false),
                    TenantUserId = table.Column<long>(type: "INTEGER", nullable: true),
                    SpaceId = table.Column<long>(type: "INTEGER", nullable: false),
                    SubSpaceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ContractDateStart = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ContractDateStop = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Cost = table.Column<decimal>(type: "TEXT", nullable: false),
                    UtilizationTypeId = table.Column<int>(type: "INTEGER", nullable: true),
                    DeclarationDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ContractDateTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Spaces_SpaceId",
                        column: x => x.SpaceId,
                        principalTable: "Spaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Users_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Users_TenantUserId",
                        column: x => x.TenantUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_UtilizationTypes_UtilizationTypeId",
                        column: x => x.UtilizationTypeId,
                        principalTable: "UtilizationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "TypeOfSpaces",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Public buildings and structures" });

            migrationBuilder.InsertData(
                table: "TypeOfSpaces",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Residential buildings" });

            migrationBuilder.InsertData(
                table: "TypeOfSpaces",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Industrial buildings and structures" });

            migrationBuilder.InsertData(
                table: "TypeOfSpaces",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Buildings and structures intended for the needs of agriculture" });

            migrationBuilder.InsertData(
                table: "UtilizationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Residential" });

            migrationBuilder.InsertData(
                table: "UtilizationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Сommercial" });

            migrationBuilder.InsertData(
                table: "UtilizationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Production" });

            migrationBuilder.InsertData(
                table: "UtilizationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Warehouse" });

            migrationBuilder.InsertData(
                table: "UtilizationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5, "Public catering" });

            migrationBuilder.InsertData(
                table: "UtilizationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 6, "Utility" });

            migrationBuilder.InsertData(
                table: "UtilizationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 7, "Office space" });

            migrationBuilder.InsertData(
                table: "UtilizationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 8, "Education" });

            migrationBuilder.InsertData(
                table: "UtilizationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 9, "Sports" });

            migrationBuilder.InsertData(
                table: "UtilizationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 10, "Free target" });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_OwnerUserId",
                table: "Contracts",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_SpaceId",
                table: "Contracts",
                column: "SpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_TenantUserId",
                table: "Contracts",
                column: "TenantUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_UtilizationTypeId",
                table: "Contracts",
                column: "UtilizationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_Latitude",
                table: "Spaces",
                column: "Latitude");

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_Longitude",
                table: "Spaces",
                column: "Longitude");

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_TypeOfSpaceId",
                table: "Spaces",
                column: "TypeOfSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAuthentications_UserId",
                table: "UserAuthentications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "UserAuthentications");

            migrationBuilder.DropTable(
                name: "Spaces");

            migrationBuilder.DropTable(
                name: "UtilizationTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "TypeOfSpaces");
        }
    }
}
