using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudDataApplication.Migrations
{
    public partial class AddForeignKeyWithRolesData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Register",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Register_RoleId",
                table: "Register",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Register_Roles_RoleId",
                table: "Register",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Register_Roles_RoleId",
                table: "Register");

            migrationBuilder.DropIndex(
                name: "IX_Register_RoleId",
                table: "Register");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Register");
        }
    }
}
