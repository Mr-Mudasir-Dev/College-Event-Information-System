using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CEIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class registrationTblUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AttendedAt",
                table: "Registrations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAttended",
                table: "Registrations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttendedAt",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "IsAttended",
                table: "Registrations");
        }
    }
}
