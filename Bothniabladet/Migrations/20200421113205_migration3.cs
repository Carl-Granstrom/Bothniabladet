using Microsoft.EntityFrameworkCore.Migrations;

namespace Bothniabladet.Migrations
{
    public partial class migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sectionPublished",
                table: "Images",
                newName: "SectionPublished");

            migrationBuilder.CreateTable(
                name: "Keyword",
                columns: table => new
                {
                    KeywordId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(nullable: true),
                    ImageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keyword", x => x.KeywordId);
                    table.ForeignKey(
                        name: "FK_Keyword_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Keyword_ImageId",
                table: "Keyword",
                column: "ImageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Keyword");

            migrationBuilder.RenameColumn(
                name: "SectionPublished",
                table: "Images",
                newName: "sectionPublished");
        }
    }
}
