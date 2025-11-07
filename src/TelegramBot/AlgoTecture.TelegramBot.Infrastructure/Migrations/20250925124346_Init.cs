using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlgoTecture.TelegramBot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TelegramAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TelegramUserId = table.Column<long>(type: "bigint", nullable: false),
                    TelegramChatId = table.Column<long>(type: "bigint", nullable: true),
                    TelegramUserName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    TelegramUserFullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LinkedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    LanguageCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastActivityAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsBlocked = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramAccounts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TelegramAccounts_LinkedUserId",
                table: "TelegramAccounts",
                column: "LinkedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramAccounts_TelegramUserId",
                table: "TelegramAccounts",
                column: "TelegramUserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TelegramAccounts");
        }
    }
}
