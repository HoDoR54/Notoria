using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesBackend.Migrations
{
    /// <inheritdoc />
    public partial class reset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_UserId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Preferences_PreferenceId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PreferenceId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PreferenceId",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Preferences",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Notes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DraftId",
                table: "Notes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Preferences_UserId",
                table: "Preferences",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_UserId",
                table: "Notes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Preferences_Users_UserId",
                table: "Preferences",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_UserId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Preferences_Users_UserId",
                table: "Preferences");

            migrationBuilder.DropIndex(
                name: "IX_Preferences_UserId",
                table: "Preferences");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Preferences");

            migrationBuilder.DropColumn(
                name: "DraftId",
                table: "Notes");

            migrationBuilder.AddColumn<Guid>(
                name: "PreferenceId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Notes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PreferenceId",
                table: "Users",
                column: "PreferenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_UserId",
                table: "Notes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Preferences_PreferenceId",
                table: "Users",
                column: "PreferenceId",
                principalTable: "Preferences",
                principalColumn: "Id");
        }
    }
}
