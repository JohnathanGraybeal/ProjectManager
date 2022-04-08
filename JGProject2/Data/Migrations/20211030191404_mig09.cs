using Microsoft.EntityFrameworkCore.Migrations;

namespace JGProject2.Data.Migrations
{
    public partial class mig09 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRoles_People_PersonId",
                table: "AppRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_People_AppRoles_AppRoleId",
                table: "People");

            

            migrationBuilder.DropIndex(
                name: "IX_People_AppRoleId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_AppRoles_PersonId",
                table: "AppRoles");

            migrationBuilder.DropColumn(
                name: "AppRoleId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "AppRoles");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRoles_AppRoleId",
                table: "ProjectRoles",
                column: "AppRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRoles_AppRoles_AppRoleId",
                table: "ProjectRoles",
                column: "AppRoleId",
                principalTable: "AppRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRoles_AppRoles_AppRoleId",
                table: "ProjectRoles");

            migrationBuilder.DropIndex(
                name: "IX_ProjectRoles_AppRoleId",
                table: "ProjectRoles");

            migrationBuilder.AddColumn<int>(
                name: "AppRoleId",
                table: "People",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "AppRoles",
                type: "int",
                nullable: true);

           

            migrationBuilder.CreateIndex(
                name: "IX_People_AppRoleId",
                table: "People",
                column: "AppRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRoles_PersonId",
                table: "AppRoles",
                column: "PersonId");

          

            migrationBuilder.AddForeignKey(
                name: "FK_AppRoles_People_PersonId",
                table: "AppRoles",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_People_AppRoles_AppRoleId",
                table: "People",
                column: "AppRoleId",
                principalTable: "AppRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
