using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlgoTecture.Data.Persistence.Ef.Migrations
{
    public partial class _28_Removed_Contract_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.UpdateData(
                table: "Spaces",
                keyColumn: "Id",
                keyValue: 1L,
                column: "SpaceProperty",
                value: "{\"SpacePropertyId\":\"4c4f455c-bc98-47da-9f4b-9dcc25a17fe5\",\"Name\":\"Santa María\",\"Description\":\"best boat in the world\",\"Properties\":null,\"SubSpaces\":null,\"Images\":null}");

            migrationBuilder.UpdateData(
                table: "Spaces",
                keyColumn: "Id",
                keyValue: 2L,
                column: "SpaceProperty",
                value: "{\"SpacePropertyId\":\"7d2dc2f3-4f52-4244-8ade-73eba2772a51\",\"Name\":\"Niña\",\"Description\":\"best boat in the world\",\"Properties\":null,\"SubSpaces\":null,\"Images\":null}");

            migrationBuilder.UpdateData(
                table: "Spaces",
                keyColumn: "Id",
                keyValue: 3L,
                column: "SpaceProperty",
                value: "{\"SpacePropertyId\":\"a5f8e388-0c2f-491c-82ff-d4c92da97aaa\",\"Name\":\"Pinta\",\"Description\":\"best boat in the world\",\"Properties\":null,\"SubSpaces\":null,\"Images\":null}");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerUserId = table.Column<long>(type: "bigint", nullable: false),
                    PriceSpecificationId = table.Column<long>(type: "bigint", nullable: false),
                    ReservationId = table.Column<long>(type: "bigint", nullable: true),
                    SpaceId = table.Column<long>(type: "bigint", nullable: false),
                    TenantUserId = table.Column<long>(type: "bigint", nullable: true),
                    UtilizationTypeId = table.Column<int>(type: "integer", nullable: true),
                    ContractDateTimeUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ContractFromUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ContractToUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeclarationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PriceCurrency = table.Column<string>(type: "text", nullable: false),
                    SubSpaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalPrice = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_PriceSpecifications_PriceSpecificationId",
                        column: x => x.PriceSpecificationId,
                        principalTable: "PriceSpecifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id");
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contracts_UtilizationTypes_UtilizationTypeId",
                        column: x => x.UtilizationTypeId,
                        principalTable: "UtilizationTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Spaces",
                keyColumn: "Id",
                keyValue: 1L,
                column: "SpaceProperty",
                value: "{\"SpacePropertyId\":\"4c4f455c-bc98-47da-9f4b-9dcc25a17fe5\",\"Name\":\"Santa María\",\"Description\":\"best boat in the world\",\"Properties\":null,\"OwnerId\":0,\"ContractId\":0,\"SubSpaces\":null}");

            migrationBuilder.UpdateData(
                table: "Spaces",
                keyColumn: "Id",
                keyValue: 2L,
                column: "SpaceProperty",
                value: "{\"SpacePropertyId\":\"7d2dc2f3-4f52-4244-8ade-73eba2772a51\",\"Name\":\"Niña\",\"Description\":\"best boat in the world\",\"Properties\":null,\"OwnerId\":0,\"ContractId\":0,\"SubSpaces\":null}");

            migrationBuilder.UpdateData(
                table: "Spaces",
                keyColumn: "Id",
                keyValue: 3L,
                column: "SpaceProperty",
                value: "{\"SpacePropertyId\":\"a5f8e388-0c2f-491c-82ff-d4c92da97aaa\",\"Name\":\"Pinta\",\"Description\":\"best boat in the world\",\"Properties\":null,\"OwnerId\":0,\"ContractId\":0,\"SubSpaces\":null}");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_OwnerUserId",
                table: "Contracts",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_PriceSpecificationId",
                table: "Contracts",
                column: "PriceSpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ReservationId",
                table: "Contracts",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_SpaceId",
                table: "Contracts",
                column: "SpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_SubSpaceId",
                table: "Contracts",
                column: "SubSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_TenantUserId",
                table: "Contracts",
                column: "TenantUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_UtilizationTypeId",
                table: "Contracts",
                column: "UtilizationTypeId");
        }
    }
}
