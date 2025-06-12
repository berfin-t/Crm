using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crm.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectIdInTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTasks_AppCustomers_CustomerId",
                table: "AppTasks");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "AppTasks",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_AppTasks_CustomerId",
                table: "AppTasks",
                newName: "IX_AppTasks_ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTasks_AppProjects_ProjectId",
                table: "AppTasks",
                column: "ProjectId",
                principalTable: "AppProjects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTasks_AppProjects_ProjectId",
                table: "AppTasks");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "AppTasks",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_AppTasks_ProjectId",
                table: "AppTasks",
                newName: "IX_AppTasks_CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTasks_AppCustomers_CustomerId",
                table: "AppTasks",
                column: "CustomerId",
                principalTable: "AppCustomers",
                principalColumn: "Id");
        }
    }
}
