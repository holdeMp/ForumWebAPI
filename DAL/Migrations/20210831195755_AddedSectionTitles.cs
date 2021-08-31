using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddedSectionTitles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.AddColumn<int>(
                name: "SectionTitleId",
                table: "Sections",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SectionTitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionTitles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sections_SectionTitleId",
                table: "Sections",
                column: "SectionTitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_SectionTitles_SectionTitleId",
                table: "Sections",
                column: "SectionTitleId",
                principalTable: "SectionTitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_SectionTitles_SectionTitleId",
                table: "Sections");

            migrationBuilder.DropTable(
                name: "SectionTitles");

            migrationBuilder.DropIndex(
                name: "IX_Sections_SectionTitleId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "SectionTitleId",
                table: "Sections");

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });
        }
    }
}
