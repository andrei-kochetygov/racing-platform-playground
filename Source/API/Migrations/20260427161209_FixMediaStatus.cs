using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.API.Migrations
{
    /// <inheritdoc />
    public partial class FixMediaStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE MediaFiles
                SET Status = 'pending'
                WHERE Status = ''
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
