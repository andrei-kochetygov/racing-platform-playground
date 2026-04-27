using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.API.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceMediaFileStateFieldsWithStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_MediaFiles_AvatarId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_AvatarId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "MediaFiles");

            migrationBuilder.AlterColumn<string>(
                name: "AvatarId",
                table: "UserProfiles",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "MediaFiles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_AvatarId",
                table: "UserProfiles",
                column: "AvatarId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_MediaFiles_AvatarId",
                table: "UserProfiles",
                column: "AvatarId",
                principalTable: "MediaFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_MediaFiles_AvatarId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_AvatarId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "MediaFiles");

            migrationBuilder.AlterColumn<string>(
                name: "AvatarId",
                table: "UserProfiles",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "MediaFiles",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_AvatarId",
                table: "UserProfiles",
                column: "AvatarId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_MediaFiles_AvatarId",
                table: "UserProfiles",
                column: "AvatarId",
                principalTable: "MediaFiles",
                principalColumn: "Id");
        }
    }
}
