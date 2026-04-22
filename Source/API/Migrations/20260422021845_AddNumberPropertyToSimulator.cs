using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.API.Migrations
{
    /// <inheritdoc />
    public partial class AddNumberPropertyToSimulator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Number",
                table: "Simulators",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Simulators");
        }
    }
}
