using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations.ForumDb
{
    public partial class RemovingContentfromTHeme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Themes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Themes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
