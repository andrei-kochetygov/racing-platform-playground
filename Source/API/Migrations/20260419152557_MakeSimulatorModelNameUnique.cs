using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.API.Migrations
{
    /// <inheritdoc />
    public partial class MakeSimulatorModelNameUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SimulatorModels_Name",
                table: "SimulatorModels",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SimulatorModels_Name",
                table: "SimulatorModels");
        }
    }
}
