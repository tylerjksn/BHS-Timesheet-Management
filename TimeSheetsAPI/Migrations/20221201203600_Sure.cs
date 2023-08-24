using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeSheets.API.Migrations
{
    public partial class Sure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Template_Questions_SheetTemplates_SheetTemplateId",
                table: "Template_Questions");

            migrationBuilder.AlterColumn<Guid>(
                name: "SheetTemplateId",
                table: "Template_Questions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Template_Questions_SheetTemplates_SheetTemplateId",
                table: "Template_Questions",
                column: "SheetTemplateId",
                principalTable: "SheetTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Template_Questions_SheetTemplates_SheetTemplateId",
                table: "Template_Questions");

            migrationBuilder.AlterColumn<Guid>(
                name: "SheetTemplateId",
                table: "Template_Questions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Template_Questions_SheetTemplates_SheetTemplateId",
                table: "Template_Questions",
                column: "SheetTemplateId",
                principalTable: "SheetTemplates",
                principalColumn: "Id");
        }
    }
}
