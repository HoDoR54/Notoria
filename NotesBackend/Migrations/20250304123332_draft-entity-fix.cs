using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesBackend.Migrations
{
    /// <inheritdoc />
    public partial class draftentityfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DraftId",
                table: "NoteTags",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NoteTags_DraftId",
                table: "NoteTags",
                column: "DraftId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteTags_Drafts_DraftId",
                table: "NoteTags",
                column: "DraftId",
                principalTable: "Drafts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteTags_Drafts_DraftId",
                table: "NoteTags");

            migrationBuilder.DropIndex(
                name: "IX_NoteTags_DraftId",
                table: "NoteTags");

            migrationBuilder.DropColumn(
                name: "DraftId",
                table: "NoteTags");
        }
    }
}
