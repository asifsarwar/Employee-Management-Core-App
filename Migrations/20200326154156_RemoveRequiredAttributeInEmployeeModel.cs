using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
namespace EmployeeManagementCoreApp.Migrations
{
    public partial class RemoveRequiredAttributeInEmployeeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TEmployees");
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employee",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Employee",
                nullable: false,
                defaultValue: "");
            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "EmployeeID", "Address", "BirthDate", "City", "Country", "Department", "Email", "Extension", "FirstName", "HireDate", "HomePhone", "LastName", "Name", "Notes", "Photo", "PhotoPath", "PostalCode", "Region", "ReportsTo", "Title", "TitleOfCourtesy" },
                values: new object[] { 1, null, null, null, null, 2, "mary@gmail.com", null, "Mary", null, null, "Magdalene", "Mary Magdalene", null, null, null, null, null, null, null, null });
            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "EmployeeID", "Address", "BirthDate", "City", "Country", "Department", "Email", "Extension", "FirstName", "HireDate", "HomePhone", "LastName", "Name", "Notes", "Photo", "PhotoPath", "PostalCode", "Region", "ReportsTo", "Title", "TitleOfCourtesy" },
                values: new object[] { 2, null, null, null, null, 0, "jhon@gmail.com", null, "Jhon", null, null, "Hopkins", "Jhon Hopkins", null, null, null, null, null, null, null, null });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "EmployeeID",
                keyValue: 1);
            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "EmployeeID",
                keyValue: 2);
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Employee");
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employee",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
            migrationBuilder.CreateTable(
                name: "TEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Department = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PhotoPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEmployees", x => x.Id);
                });
            migrationBuilder.InsertData(
                table: "TEmployees",
                columns: new[] { "Id", "Department", "Email", "Name", "PhotoPath" },
                values: new object[] { 1, 2, "mary@gmail.com", "Mary", null });
            migrationBuilder.InsertData(
                table: "TEmployees",
                columns: new[] { "Id", "Department", "Email", "Name", "PhotoPath" },
                values: new object[] { 2, 0, "jhon@gmail.com", "Jhon", null });
        }
    }
}
