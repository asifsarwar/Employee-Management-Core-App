using Microsoft.EntityFrameworkCore.Migrations;
namespace EmployeeManagementCoreApp.Migrations
{
    public partial class DepartmentAdditionInEmployeeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Department",
                table: "Employee",
                nullable: true);
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "Employee");
        }
    }
}
