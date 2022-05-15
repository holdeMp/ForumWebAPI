using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations.ForumDb
{
    public partial class Addedlikesanddislikestoanswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dislikes",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Answers");
        }
    }
}
