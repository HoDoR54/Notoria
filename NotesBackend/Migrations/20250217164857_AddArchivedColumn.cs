﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddArchivedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Notes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Notes");
        }
    }
}
