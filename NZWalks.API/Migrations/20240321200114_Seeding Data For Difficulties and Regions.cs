using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataForDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0759ae7d-267b-4996-931f-2a91bd533e10"), "Medium" },
                    { new Guid("b89b23cd-9c4d-4d74-a17b-71238b536361"), "Easy" },
                    { new Guid("ba75d11d-cda0-44ee-b486-682cb13fa63b"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("0201fb76-55a0-4751-bc9a-492c571f4001"), "JPN", "Japon", "https://www.pexels.com/fr-fr/photo/temple-de-la-pagode-pres-du-lac-sous-un-ciel-nuageux-1829980/" },
                    { new Guid("09de50b4-aa25-446c-84d9-335b9ef172fb"), "CAN", "Canada", "https://www.pexels.com/fr-fr/photo/batiments-pres-de-plan-d-eau-la-nuit-1519088/" },
                    { new Guid("5f49cc7b-d60e-4238-9cae-735950b28dc6"), "AKL", "Auckland", "https://www.pexels.com/fr-fr/photo/ville-horizon-autoroute-ciel-bleu-5342978/" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("0759ae7d-267b-4996-931f-2a91bd533e10"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("b89b23cd-9c4d-4d74-a17b-71238b536361"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("ba75d11d-cda0-44ee-b486-682cb13fa63b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("0201fb76-55a0-4751-bc9a-492c571f4001"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("09de50b4-aa25-446c-84d9-335b9ef172fb"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("5f49cc7b-d60e-4238-9cae-735950b28dc6"));
        }
    }
}
