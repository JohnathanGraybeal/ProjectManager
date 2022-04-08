using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JGProject2.Data.Migrations
{
    public partial class mig06 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Projects_ProjectId",
                table: "People");


           

            migrationBuilder.DropIndex(
                name: "IX_ProjectRoles_EditProjectVMId",
                table: "ProjectRoles");

            migrationBuilder.DropIndex(
                name: "IX_ProjectRoles_ProjectDetailsVMId",
                table: "ProjectRoles");

            migrationBuilder.DropIndex(
                name: "IX_People_ProjectId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "EditProjectVMId",
                table: "ProjectRoles");

            migrationBuilder.DropColumn(
                name: "ProjectDetailsVMId",
                table: "ProjectRoles");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "People");

            migrationBuilder.AlterColumn<decimal>(
                name: "HourlyRate",
                table: "ProjectRoles",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

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

            migrationBuilder.DropTable(
                name: "PersonProject");

            migrationBuilder.DropIndex(
                name: "IX_AppRoles_PersonId",
                table: "AppRoles");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "AppRoles");

            migrationBuilder.AlterColumn<decimal>(
                name: "HourlyRate",
                table: "ProjectRoles",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");


            

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "People",
                type: "int",
                nullable: true);

            

            migrationBuilder.CreateIndex(
                name: "IX_People_ProjectId",
                table: "People",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Projects_ProjectId",
                table: "People",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            
        }
    }
}
