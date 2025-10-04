using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace back.Migrations
{
    /// <inheritdoc />
    public partial class CreateRespiratoryDiseasesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RespiratoryDisease",
                columns: new[] { "Id", "Disease" },
                values: new object[,]
                {
                    { 1, "Asma" },
                    { 2, "Doença Pulmonar Obstrutiva Crônica (DPOC)" },
                    { 3, "Bronquite Crônica" },
                    { 4, "Enfisema Pulmonar" },
                    { 5, "Rinite Alérgica" },
                    { 6, "Sinusite" },
                    { 7, "Laringite" },
                    { 8, "Faringite" },
                    { 9, "Bronquite Aguda" },
                    { 10, "Pneumonia" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
