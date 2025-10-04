using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace back.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRespiratoryDiseasesToEnglish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 1,
                column: "Disease",
                value: "Asthma");

            migrationBuilder.UpdateData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 2,
                column: "Disease",
                value: "Chronic Obstructive Pulmonary Disease (COPD)");

            migrationBuilder.UpdateData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 3,
                column: "Disease",
                value: "Chronic Bronchitis");

            migrationBuilder.UpdateData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 4,
                column: "Disease",
                value: "Pulmonary Emphysema");

            migrationBuilder.UpdateData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 5,
                column: "Disease",
                value: "Allergic Rhinitis");

            migrationBuilder.UpdateData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 6,
                column: "Disease",
                value: "Sinusitis");

            migrationBuilder.UpdateData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 7,
                column: "Disease",
                value: "Laryngitis");

            migrationBuilder.UpdateData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 8,
                column: "Disease",
                value: "Pharyngitis");

            migrationBuilder.UpdateData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 9,
                column: "Disease",
                value: "Acute Bronchitis");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 1,
                column: "Disease",
                value: "Asma");

            migrationBuilder.UpdateData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 2,
                column: "Disease",
                value: "Doença Pulmonar Obstrutiva Crônica (DPOC)");

            migrationBuilder.UpdateData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 3,
                column: "Disease",
                value: "Bronquite Crônica");

            migrationBuilder.UpdateData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 4,
                column: "Disease",
                value: "Enfisema Pulmonar");

            migrationBuilder.UpdateData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 5,
                column: "Disease",
                value: "Rinite Alérgica");

            migrationBuilder.UpdateData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 6,
                column: "Disease",
                value: "Sinusite");

            migrationBuilder.UpdateData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 7,
                column: "Disease",
                value: "Laringite");

            migrationBuilder.UpdateData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 8,
                column: "Disease",
                value: "Faringite");

            migrationBuilder.UpdateData(
                table: "RespiratoryDisease",
                keyColumn: "Id",
                keyValue: 9,
                column: "Disease",
                value: "Bronquite Aguda");
        }
    }
}
