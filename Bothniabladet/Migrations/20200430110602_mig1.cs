using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace Bothniabladet.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(nullable: true),
                    PriceReduction = table.Column<float>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Enums",
                columns: table => new
                {
                    SectionEnumId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enums", x => x.SectionEnumId);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageTitle = table.Column<string>(maxLength: 40, nullable: true),
                    ImageData = table.Column<byte[]>(nullable: true),
                    BasePrice = table.Column<int>(nullable: false),
                    Issue = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Thumbnail = table.Column<byte[]>(nullable: true),
                    ImageLicense_LicenceType = table.Column<int>(nullable: true),
                    ImageLicense_UsesLeft = table.Column<int>(nullable: true),
                    ImageMetaData_Height = table.Column<int>(nullable: true),
                    ImageMetaData_Width = table.Column<int>(nullable: true),
                    ImageMetaData_FileSize = table.Column<long>(nullable: true),
                    ImageMetaData_FileFormat = table.Column<string>(nullable: true),
                    ImageMetaData_DateTaken = table.Column<DateTime>(nullable: true),
                    ImageMetaData_Location = table.Column<Point>(type: "geometry", nullable: true),
                    Section = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                });

            migrationBuilder.CreateTable(
                name: "Keywords",
                columns: table => new
                {
                    KeywordId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keywords", x => x.KeywordId);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DollarAmount = table.Column<float>(nullable: false),
                    Paid = table.Column<bool>(nullable: false),
                    DueAt = table.Column<DateTime>(nullable: false),
                    ClientId = table.Column<int>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EditedImage",
                columns: table => new
                {
                    EditedImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageTitle = table.Column<string>(nullable: true),
                    ImageData = table.Column<byte[]>(nullable: true),
                    Thumbnail = table.Column<byte[]>(nullable: true),
                    ImageId = table.Column<int>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "ImageKeywords",
                columns: table => new
                {
                    ImageId = table.Column<int>(nullable: false),
                    KeywordId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageKeywords", x => new { x.ImageId, x.KeywordId });
                    table.ForeignKey(
                        name: "FK_ImageKeywords_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImageKeywords_Keywords_KeywordId",
                        column: x => x.KeywordId,
                        principalTable: "Keywords",
                        principalColumn: "KeywordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Enums",
                columns: new[] { "SectionEnumId", "Name" },
                values: new object[,]
                {
                    { 1, "CULTURE" },
                    { 2, "ECONOMY" },
                    { 3, "NEWS" },
                    { 4, "INTERNATIONAL" },
                    { 5, "SPORTS" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EditedImage_ImageId",
                table: "EditedImage",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageKeywords_KeywordId",
                table: "ImageKeywords",
                column: "KeywordId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ClientId",
                table: "Invoices",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EditedImage");

            migrationBuilder.DropTable(
                name: "Enums");

            migrationBuilder.DropTable(
                name: "ImageKeywords");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Keywords");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
