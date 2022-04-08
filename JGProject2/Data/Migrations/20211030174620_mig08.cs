using Microsoft.EntityFrameworkCore.Migrations;

namespace JGProject2.Data.Migrations
{
    public partial class mig08 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppRoleId",
                table: "People",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_AppRoleId",
                table: "People",
                column: "AppRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_AppRoles_AppRoleId",
                table: "People",
                column: "AppRoleId",
                principalTable: "AppRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_AppRoles_AppRoleId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_AppRoleId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "AppRoleId",
                table: "People");
        }
    }
}
