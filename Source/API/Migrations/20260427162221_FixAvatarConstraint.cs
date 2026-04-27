using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.API.Migrations
{
    /// <inheritdoc />
    public partial class FixAvatarConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_MediaFiles_AvatarId",
                table: "UserProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "AvatarId",
                table: "UserProfiles",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_MediaFiles_AvatarId",
                table: "UserProfiles",
                column: "AvatarId",
                principalTable: "MediaFiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_MediaFiles_AvatarId",
                table: "UserProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "AvatarId",
                table: "UserProfiles",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_MediaFiles_AvatarId",
                table: "UserProfiles",
                column: "AvatarId",
                principalTable: "MediaFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
