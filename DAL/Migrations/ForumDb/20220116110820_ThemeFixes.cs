using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations.ForumDb
{
    public partial class ThemeFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Themes_ThemeId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_SubSections_Sections_SectionID",
                table: "SubSections");

            migrationBuilder.DropForeignKey(
                name: "FK_Themes_Answers_MainAnswerId",
                table: "Themes");

            migrationBuilder.DropIndex(
                name: "IX_Themes_MainAnswerId",
                table: "Themes");

            migrationBuilder.DropColumn(
                name: "AnswersCount",
                table: "Themes");

            migrationBuilder.DropColumn(
                name: "MainAnswerId",
                table: "Themes");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "SectionID",
                table: "SubSections",
                newName: "SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_SubSections_SectionID",
                table: "SubSections",
                newName: "IX_SubSections_SectionId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Themes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SectionId",
                table: "SubSections",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ThemeId",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Themes_ThemeId",
                table: "Answers",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubSections_Sections_SectionId",
                table: "SubSections",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "SectionID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Themes_ThemeId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_SubSections_Sections_SectionId",
                table: "SubSections");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Themes");

            migrationBuilder.RenameColumn(
                name: "SectionId",
                table: "SubSections",
                newName: "SectionID");

            migrationBuilder.RenameIndex(
                name: "IX_SubSections_SectionId",
                table: "SubSections",
                newName: "IX_SubSections_SectionID");

            migrationBuilder.AddColumn<int>(
                name: "AnswersCount",
                table: "Themes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MainAnswerId",
                table: "Themes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SectionID",
                table: "SubSections",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ThemeId",
                table: "Answers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SectionId",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Themes_MainAnswerId",
                table: "Themes",
                column: "MainAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Themes_ThemeId",
                table: "Answers",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubSections_Sections_SectionID",
                table: "SubSections",
                column: "SectionID",
                principalTable: "Sections",
                principalColumn: "SectionID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Themes_Answers_MainAnswerId",
                table: "Themes",
                column: "MainAnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
