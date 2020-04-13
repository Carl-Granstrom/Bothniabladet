using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bothniabladet.Migrations
{
    public partial class migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageLicenseId = table.Column<int>(nullable: false),
                    ImageMetaDataId = table.Column<int>(nullable: false),
                    basePrice = table.Column<int>(nullable: false),
                    issue = table.Column<DateTime>(nullable: false),
                    sectionPublished = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                });

            migrationBuilder.CreateTable(
                name: "EditedImage",
                columns: table => new
                {
                    EditedImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createdAt = table.Column<DateTime>(nullable: false),
                    ImageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditedImage", x => x.EditedImageId);
                    table.ForeignKey(
                        name: "FK_EditedImage_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EditedImage_ImageId",
                table: "EditedImage",
                column: "ImageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EditedImage");

            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
