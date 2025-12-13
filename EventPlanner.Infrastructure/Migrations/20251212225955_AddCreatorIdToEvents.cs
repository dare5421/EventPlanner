using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatorIdToEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Registrations",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_UserId_EventId",
                table: "Registrations",
                columns: new[] { "UserId", "EventId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations");

            migrationBuilder.DropIndex(
                name: "IX_Registrations_UserId_EventId",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Events");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations",
                columns: new[] { "UserId", "EventId" });
        }
    }
}
