using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeSheets.API.Migrations
{
    public partial class LowercaseC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CountofDays",
                table: "SheetTemplates",
                newName: "countOfDays");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "countOfDays",
                table: "SheetTemplates",
                newName: "CountofDays");
        }
    }
}
