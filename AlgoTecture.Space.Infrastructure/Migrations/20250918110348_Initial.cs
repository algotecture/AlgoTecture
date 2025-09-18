using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AlgoTecture.Space.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "SpaceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spaces",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    SpaceTypeId = table.Column<int>(type: "integer", nullable: false),
                    SpaceAddress = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Location = table.Column<Point>(type: "geography(Point)", nullable: true),
                    Area = table.Column<decimal>(type: "numeric", nullable: true),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    SpaceProperties = table.Column<string>(type: "jsonb", nullable: true),
                    DataSource = table.Column<string>(type: "text", nullable: true),
                    ExternalId = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spaces_SpaceTypes_SpaceTypeId",
                        column: x => x.SpaceTypeId,
                        principalTable: "SpaceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Spaces_Spaces_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Spaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpaceImages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SpaceId = table.Column<long>(type: "bigint", nullable: false),
                    Url = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Path = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    ContentType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpaceImages_Spaces_SpaceId",
                        column: x => x.SpaceId,
                        principalTable: "Spaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SpaceTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Parking" },
                    { 2, "Coworking" },
                    { 3, "Boat" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpaceImages_CreatedAt",
                table: "SpaceImages",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceImages_SpaceId",
                table: "SpaceImages",
                column: "SpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_Location",
                table: "Spaces",
                column: "Location")
                .Annotation("Npgsql:IndexMethod", "GIST");

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_ParentId",
                table: "Spaces",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_SpaceProperties",
                table: "Spaces",
                column: "SpaceProperties")
                .Annotation("Npgsql:IndexMethod", "gin");

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_SpaceTypeId",
                table: "Spaces",
                column: "SpaceTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpaceImages");

            migrationBuilder.DropTable(
                name: "Spaces");

            migrationBuilder.DropTable(
                name: "SpaceTypes");
        }
    }
}
