using Microsoft.EntityFrameworkCore.Migrations;

namespace JGProject2.Data.Migrations
{
    public partial class mig03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRoles_ProjectRoles_ProjectRoleId",
                table: "AppRoles");

            migrationBuilder.DropIndex(
                name: "IX_AppRoles_ProjectRoleId",
                table: "AppRoles");

            migrationBuilder.DropColumn(
                name: "ProjectRoleId",
                table: "AppRoles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectRoleId",
                table: "AppRoles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppRoles_ProjectRoleId",
                table: "AppRoles",
                column: "ProjectRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppRoles_ProjectRoles_ProjectRoleId",
                table: "AppRoles",
                column: "ProjectRoleId",
                principalTable: "ProjectRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
