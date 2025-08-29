using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Algotecture.Data.Persistence.Ef.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TelegramUserInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TelegramUserId = table.Column<long>(type: "bigint", nullable: true),
                    TelegramChatId = table.Column<long>(type: "bigint", nullable: true),
                    TelegramUserName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    TelegramUserFullName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramUserInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UtilizationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilizationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Phone = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Email = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    TelegramUserInfoId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDateTimeUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_TelegramUserInfos_TelegramUserInfoId",
                        column: x => x.TelegramUserInfoId,
                        principalTable: "TelegramUserInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Spaces",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UtilizationTypeId = table.Column<int>(type: "integer", nullable: false),
                    SpaceAddress = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    SpaceProperty = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spaces_UtilizationTypes_UtilizationTypeId",
                        column: x => x.UtilizationTypeId,
                        principalTable: "UtilizationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAuthentications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    HashedPassword = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
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
                name: "PriceSpecifications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SpaceId = table.Column<long>(type: "bigint", nullable: false),
                    SubSpaceId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PricePerTime = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PriceCurrency = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UnitOfTime = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ValidThrough = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceSpecifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceSpecifications_Spaces_SpaceId",
                        column: x => x.SpaceId,
                        principalTable: "Spaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    TenantUserId = table.Column<long>(type: "bigint", nullable: false),
                    SpaceId = table.Column<long>(type: "bigint", nullable: false),
                    SubSpaceId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TotalPrice = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PriceSpecificationId = table.Column<long>(type: "bigint", nullable: false),
                    ReservationDateTimeUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReservationFromUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReservationToUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReservationStatus = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_PriceSpecifications_PriceSpecificationId",
                        column: x => x.PriceSpecificationId,
                        principalTable: "PriceSpecifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Spaces_SpaceId",
                        column: x => x.SpaceId,
                        principalTable: "Spaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_TenantUserId",
                        column: x => x.TenantUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerUserId = table.Column<long>(type: "bigint", nullable: false),
                    TenantUserId = table.Column<long>(type: "bigint", nullable: true),
                    SpaceId = table.Column<long>(type: "bigint", nullable: false),
                    SubSpaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    PriceCurrency = table.Column<string>(type: "text", nullable: false),
                    TotalPrice = table.Column<string>(type: "text", nullable: false),
                    PriceSpecificationId = table.Column<long>(type: "bigint", nullable: false),
                    UtilizationTypeId = table.Column<int>(type: "integer", nullable: true),
                    ReservationId = table.Column<long>(type: "bigint", nullable: true),
                    DeclarationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ContractFromUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ContractToUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ContractDateTimeUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDateTimeUtc", "Email", "Phone", "TelegramUserInfoId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 2, 20, 21, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { 2L, new DateTime(2023, 3, 13, 21, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { 3L, new DateTime(2023, 3, 13, 21, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { 4L, new DateTime(2023, 3, 15, 21, 0, 0, 0, DateTimeKind.Utc), null, null, null }
                });

            migrationBuilder.InsertData(
                table: "UtilizationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Residential" },
                    { 2, "Сommercial" },
                    { 3, "Production" },
                    { 4, "Warehouse" },
                    { 5, "Public catering" },
                    { 6, "Utility" },
                    { 7, "Office space" },
                    { 8, "Education" },
                    { 9, "Sports" },
                    { 10, "Free target" },
                    { 11, "Parking" },
                    { 12, "Boat" }
                });

            migrationBuilder.InsertData(
                table: "Spaces",
                columns: new[] { "Id", "Latitude", "Longitude", "SpaceAddress", "SpaceProperty", "UtilizationTypeId" },
                values: new object[,]
                {
                    { 1L, 47.361812591552734, 8.5370702743530273, "Mythenquai 7, 8002 Zürich", "{\"SpacePropertyId\":\"4c4f455c-bc98-47da-9f4b-9dcc25a17fe5\",\"Name\":\"Santa María\",\"Description\":\"best boat in the world\",\"Properties\":null,\"OwnerId\":0,\"ContractId\":0,\"SubSpaces\":null}", 12 },
                    { 2L, 47.361648559570312, 8.5366735458374023, "Mythenquai 9, 8002 Zürich", "{\"SpacePropertyId\":\"7d2dc2f3-4f52-4244-8ade-73eba2772a51\",\"Name\":\"Niña\",\"Description\":\"best boat in the world\",\"Properties\":null,\"OwnerId\":0,\"ContractId\":0,\"SubSpaces\":null}", 12 },
                    { 3L, 47.361316680908203, 8.5362958908081055, "Mythenquai 25, 8002 Zürich", "{\"SpacePropertyId\":\"a5f8e388-0c2f-491c-82ff-d4c92da97aaa\",\"Name\":\"Pinta\",\"Description\":\"best boat in the world\",\"Properties\":null,\"OwnerId\":0,\"ContractId\":0,\"SubSpaces\":null}", 12 }
                });

            migrationBuilder.InsertData(
                table: "PriceSpecifications",
                columns: new[] { "Id", "PriceCurrency", "PricePerTime", "SpaceId", "SubSpaceId", "UnitOfTime", "ValidFrom", "ValidThrough" },
                values: new object[,]
                {
                    { 1L, "Usd", "50", 1L, null, "Hour", null, null },
                    { 2L, "Usd", "45", 2L, null, "Hour", null, null },
                    { 3L, "Usd", "60", 3L, null, "Hour", null, null }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "Description", "PriceSpecificationId", "ReservationDateTimeUtc", "ReservationFromUtc", "ReservationStatus", "ReservationToUtc", "SpaceId", "SubSpaceId", "TenantUserId", "TotalPrice" },
                values: new object[,]
                {
                    { 1L, null, 1L, new DateTime(2023, 3, 16, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 3, 17, 12, 0, 0, 0, DateTimeKind.Utc), "Confirmed", new DateTime(2023, 3, 17, 14, 0, 0, 0, DateTimeKind.Utc), 1L, null, 2L, "100" },
                    { 2L, null, 1L, new DateTime(2023, 3, 17, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 3, 18, 12, 0, 0, 0, DateTimeKind.Utc), "Confirmed", new DateTime(2023, 3, 18, 15, 0, 0, 0, DateTimeKind.Utc), 1L, null, 3L, "100" }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_PriceSpecifications_SpaceId",
                table: "PriceSpecifications",
                column: "SpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PriceSpecificationId",
                table: "Reservations",
                column: "PriceSpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SpaceId",
                table: "Reservations",
                column: "SpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TenantUserId",
                table: "Reservations",
                column: "TenantUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_Latitude",
                table: "Spaces",
                column: "Latitude");

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_Longitude",
                table: "Spaces",
                column: "Longitude");

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_UtilizationTypeId",
                table: "Spaces",
                column: "UtilizationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAuthentications_UserId",
                table: "UserAuthentications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TelegramUserInfoId",
                table: "Users",
                column: "TelegramUserInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "UserAuthentications");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "PriceSpecifications");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Spaces");

            migrationBuilder.DropTable(
                name: "TelegramUserInfos");

            migrationBuilder.DropTable(
                name: "UtilizationTypes");
        }
    }
}
