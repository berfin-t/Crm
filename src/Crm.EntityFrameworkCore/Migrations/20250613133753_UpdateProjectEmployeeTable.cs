using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crm.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProjectEmployeeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectEmployees_AppEmployees_EmployeeId",
                table: "ProjectEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectEmployees_AppProjects_ProjectId",
                table: "ProjectEmployees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectEmployees",
                table: "ProjectEmployees");

            migrationBuilder.RenameTable(
                name: "ProjectEmployees",
                newName: "AppProjectEmployees");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectEmployees_ProjectId_EmployeeId",
                table: "AppProjectEmployees",
                newName: "IX_AppProjectEmployees_ProjectId_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectEmployees_EmployeeId",
                table: "AppProjectEmployees",
                newName: "IX_AppProjectEmployees_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppProjectEmployees",
                table: "AppProjectEmployees",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProjectEmployees_AppEmployees_EmployeeId",
                table: "AppProjectEmployees",
                column: "EmployeeId",
                principalTable: "AppEmployees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProjectEmployees_AppProjects_ProjectId",
                table: "AppProjectEmployees",
                column: "ProjectId",
                principalTable: "AppProjects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProjectEmployees_AppEmployees_EmployeeId",
                table: "AppProjectEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProjectEmployees_AppProjects_ProjectId",
                table: "AppProjectEmployees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppProjectEmployees",
                table: "AppProjectEmployees");

            migrationBuilder.RenameTable(
                name: "AppProjectEmployees",
                newName: "ProjectEmployees");

            migrationBuilder.RenameIndex(
                name: "IX_AppProjectEmployees_ProjectId_EmployeeId",
                table: "ProjectEmployees",
                newName: "IX_ProjectEmployees_ProjectId_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_AppProjectEmployees_EmployeeId",
                table: "ProjectEmployees",
                newName: "IX_ProjectEmployees_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectEmployees",
                table: "ProjectEmployees",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectEmployees_AppEmployees_EmployeeId",
                table: "ProjectEmployees",
                column: "EmployeeId",
                principalTable: "AppEmployees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectEmployees_AppProjects_ProjectId",
                table: "ProjectEmployees",
                column: "ProjectId",
                principalTable: "AppProjects",
                principalColumn: "Id");
        }
    }
}
