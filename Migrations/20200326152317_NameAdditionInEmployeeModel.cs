using Microsoft.EntityFrameworkCore.Migrations;
namespace EmployeeManagementCoreApp.Migrations
{
    public partial class NameAdditionInEmployeeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Employee",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Employee");
        }
    }
}
