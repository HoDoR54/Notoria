using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesBackend.Migrations
{
    /// <inheritdoc />
    public partial class entityrelationshipadjustment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DraftId",
                table: "Notes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DraftId",
                table: "Notes",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
