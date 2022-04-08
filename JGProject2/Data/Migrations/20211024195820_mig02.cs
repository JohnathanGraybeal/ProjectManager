using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JGProject2.Data.Migrations
{
    public partial class mig02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRoles_AppRoles_AppRoleId",
                table: "ProjectRoles");

            migrationBuilder.DropIndex(
                name: "IX_ProjectRoles_AppRoleId",
                table: "ProjectRoles");

            migrationBuilder.AlterColumn<decimal>(
                name: "HourlyRate",
                table: "ProjectRoles",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "EditProjectVMId",
                table: "ProjectRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectDetailsVMId",
                table: "ProjectRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "People",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectRoleId",
                table: "AppRoles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EditProjectVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditProjectVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectDetailsVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfRoles = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDetailsVM", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRoles_EditProjectVMId",
                table: "ProjectRoles",
                column: "EditProjectVMId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRoles_ProjectDetailsVMId",
                table: "ProjectRoles",
                column: "ProjectDetailsVMId");

            migrationBuilder.CreateIndex(
                name: "IX_People_ProjectId",
                table: "People",
                column: "ProjectId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_People_Projects_ProjectId",
                table: "People",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRoles_EditProjectVM_EditProjectVMId",
                table: "ProjectRoles",
                column: "EditProjectVMId",
                principalTable: "EditProjectVM",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRoles_ProjectDetailsVM_ProjectDetailsVMId",
                table: "ProjectRoles",
                column: "ProjectDetailsVMId",
                principalTable: "ProjectDetailsVM",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRoles_ProjectRoles_ProjectRoleId",
                table: "AppRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_People_Projects_ProjectId",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRoles_EditProjectVM_EditProjectVMId",
                table: "ProjectRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRoles_ProjectDetailsVM_ProjectDetailsVMId",
                table: "ProjectRoles");

            migrationBuilder.DropTable(
                name: "EditProjectVM");

            migrationBuilder.DropTable(
                name: "ProjectDetailsVM");

            migrationBuilder.DropIndex(
                name: "IX_ProjectRoles_EditProjectVMId",
                table: "ProjectRoles");

            migrationBuilder.DropIndex(
                name: "IX_ProjectRoles_ProjectDetailsVMId",
                table: "ProjectRoles");

            migrationBuilder.DropIndex(
                name: "IX_People_ProjectId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_AppRoles_ProjectRoleId",
                table: "AppRoles");

            migrationBuilder.DropColumn(
                name: "EditProjectVMId",
                table: "ProjectRoles");

            migrationBuilder.DropColumn(
                name: "ProjectDetailsVMId",
                table: "ProjectRoles");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "ProjectRoleId",
                table: "AppRoles");

            migrationBuilder.AlterColumn<decimal>(
                name: "HourlyRate",
                table: "ProjectRoles",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

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
    }
}
