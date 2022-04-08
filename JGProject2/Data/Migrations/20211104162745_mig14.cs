using Microsoft.EntityFrameworkCore.Migrations;

namespace JGProject2.Data.Migrations
{
    public partial class mig14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRoles_AppRoles_AppRoleId",
                table: "ProjectRoles");

            migrationBuilder.RenameColumn(
                name: "AppRoleId",
                table: "ProjectRoles",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectRoles_AppRoleId",
                table: "ProjectRoles",
                newName: "IX_ProjectRoles_RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRoles_AppRoles_RoleId",
                table: "ProjectRoles",
                column: "RoleId",
                principalTable: "AppRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRoles_AppRoles_RoleId",
                table: "ProjectRoles");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "ProjectRoles",
                newName: "AppRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectRoles_RoleId",
                table: "ProjectRoles",
                newName: "IX_ProjectRoles_AppRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRoles_AppRoles_AppRoleId",
                table: "ProjectRoles",
                column: "AppRoleId",
                principalTable: "AppRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
