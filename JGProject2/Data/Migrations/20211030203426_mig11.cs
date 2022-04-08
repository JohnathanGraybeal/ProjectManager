using Microsoft.EntityFrameworkCore.Migrations;

namespace JGProject2.Data.Migrations
{
    public partial class mig11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "AppRoles",
                type: "int",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRoles_People_PersonId",
                table: "AppRoles");

            migrationBuilder.DropIndex(
                name: "IX_AppRoles_PersonId",
                table: "AppRoles");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "AppRoles");
        }
    }
}
