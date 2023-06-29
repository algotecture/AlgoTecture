using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlgoTecture.Data.Persistence.Ef.Migrations
{
    public partial class _29_Add_Coworking_to_utilization_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Spaces",
                keyColumn: "Id",
                keyValue: 1L,
                column: "SpaceProperty",
                value: "{\"SpacePropertyId\":\"4c4f455c-bc98-47da-9f4b-9dcc25a17fe5\",\"Name\":\"Santa María\",\"Description\":\"best boat in the world\",\"Properties\":{\"additionalProp1\":\"string\",\"additionalProp2\":\"string\",\"additionalProp3\":\"string\"},\"SubSpaces\":null,\"Images\":[\"4c4f455c-bc98-47da-9f4b-9dcc25a17fe5.jpeg\"]}");

            migrationBuilder.UpdateData(
                table: "Spaces",
                keyColumn: "Id",
                keyValue: 2L,
                column: "SpaceProperty",
                value: "{\"SpacePropertyId\":\"7d2dc2f3-4f52-4244-8ade-73eba2772a51\",\"Name\":\"Niña\",\"Description\":\"best boat in the world\",\"Properties\":{\"additionalProp1\":\"string\",\"additionalProp2\":\"string\",\"additionalProp3\":\"string\"},\"SubSpaces\":null,\"Images\":[\"7d2dc2f3-4f52-4244-8ade-73eba2772a51.jpeg\"]}");

            migrationBuilder.UpdateData(
                table: "Spaces",
                keyColumn: "Id",
                keyValue: 3L,
                column: "SpaceProperty",
                value: "{\"SpacePropertyId\":\"a5f8e388-0c2f-491c-82ff-d4c92da97aaa\",\"Name\":\"Pinta\",\"Description\":\"best boat in the world\",\"Properties\":{\"additionalProp1\":\"string\",\"additionalProp2\":\"string\",\"additionalProp3\":\"string\"},\"SubSpaces\":null,\"Images\":[\"a5f8e388-0c2f-491c-82ff-d4c92da97aaa.jpeg\"]}");

            migrationBuilder.InsertData(
                table: "UtilizationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 13, "Coworking" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UtilizationTypes",
                keyColumn: "Id",
                keyValue: 13);

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
    }
}
