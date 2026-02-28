using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crm.Migrations
{
    /// <inheritdoc />
    public partial class AddedSLAEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SLAResolutionDeadline",
                table: "AppSupportTickets",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SLAResponseDeadline",
                table: "AppSupportTickets",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SLAResolutionDeadline",
                table: "AppSupportTickets");

            migrationBuilder.DropColumn(
                name: "SLAResponseDeadline",
                table: "AppSupportTickets");
        }
    }
}
