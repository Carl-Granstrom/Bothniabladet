﻿// <auto-generated />
using System;
using Bothniabladet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;

namespace Bothniabladet.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200506093741_Bothnia")]
    partial class Bothnia
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bothniabladet.Data.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClientName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<float>("PriceReduction")
                        .HasColumnType("real");

                    b.HasKey("ClientId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Bothniabladet.Data.EditedImage", b =>
                {
                    b.Property<int>("EditedImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("ImageData")
                        .HasColumnType("varbinary(max)");

                    b.Property<int?>("ImageId")
                        .HasColumnType("int");

                    b.Property<string>("ImageTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Thumbnail")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("EditedImageId");

                    b.HasIndex("ImageId");

                    b.ToTable("EditedImage");
                });

            modelBuilder.Entity("Bothniabladet.Data.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BasePrice")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("ImageData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Issue")
                        .HasColumnType("datetime2");

                    b.Property<string>("Section")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Thumbnail")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("ImageId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Bothniabladet.Data.ImageKeyword", b =>
                {
                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.Property<int>("KeywordId")
                        .HasColumnType("int");

                    b.HasKey("ImageId", "KeywordId");

                    b.HasIndex("KeywordId");

                    b.ToTable("ImageKeywords");
                });

            modelBuilder.Entity("Bothniabladet.Data.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<float>("DollarAmount")
                        .HasColumnType("real");

                    b.Property<DateTime>("DueAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Paid")
                        .HasColumnType("bit");

                    b.HasKey("InvoiceId");

                    b.HasIndex("ClientId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("Bothniabladet.Data.Keyword", b =>
                {
                    b.Property<int>("KeywordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Word")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KeywordId");

                    b.ToTable("Keywords");
                });

            modelBuilder.Entity("Bothniabladet.Data.SectionEnum", b =>
                {
                    b.Property<int>("SectionEnumId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SectionEnumId");

                    b.ToTable("Enums");

                    b.HasData(
                        new
                        {
                            SectionEnumId = 1,
                            Name = "CULTURE"
                        },
                        new
                        {
                            SectionEnumId = 2,
                            Name = "ECONOMY"
                        },
                        new
                        {
                            SectionEnumId = 3,
                            Name = "NEWS"
                        },
                        new
                        {
                            SectionEnumId = 4,
                            Name = "INTERNATIONAL"
                        },
                        new
                        {
                            SectionEnumId = 5,
                            Name = "SPORTS"
                        });
                });

            modelBuilder.Entity("Bothniabladet.Data.EditedImage", b =>
                {
                    b.HasOne("Bothniabladet.Data.Image", "Image")
                        .WithMany("EditedImages")
                        .HasForeignKey("ImageId");
                });

            modelBuilder.Entity("Bothniabladet.Data.Image", b =>
                {
                    b.OwnsOne("Bothniabladet.Data.ImageLicense", "ImageLicense", b1 =>
                        {
                            b1.Property<int>("ImageId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<int>("LicenceType")
                                .HasColumnType("int");

                            b1.Property<int>("UsesLeft")
                                .HasColumnType("int");

                            b1.HasKey("ImageId");

                            b1.ToTable("Images");

                            b1.WithOwner()
                                .HasForeignKey("ImageId");
                        });

                    b.OwnsOne("Bothniabladet.Data.ImageMetaData", "ImageMetaData", b1 =>
                        {
                            b1.Property<int>("ImageId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTime>("DateTaken")
                                .HasColumnType("datetime2");

                            b1.Property<string>("FileFormat")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<long>("FileSize")
                                .HasColumnType("bigint");

                            b1.Property<int>("Height")
                                .HasColumnType("int");

                            b1.Property<Point>("Location")
                                .HasColumnType("geometry");

                            b1.Property<int>("Width")
                                .HasColumnType("int");

                            b1.HasKey("ImageId");

                            b1.ToTable("Images");

                            b1.WithOwner()
                                .HasForeignKey("ImageId");
                        });
                });

            modelBuilder.Entity("Bothniabladet.Data.ImageKeyword", b =>
                {
                    b.HasOne("Bothniabladet.Data.Image", "Image")
                        .WithMany("KeywordLink")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bothniabladet.Data.Keyword", "Keyword")
                        .WithMany("KeywordLink")
                        .HasForeignKey("KeywordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Bothniabladet.Data.Invoice", b =>
                {
                    b.HasOne("Bothniabladet.Data.Client", "Client")
                        .WithMany("Invoices")
                        .HasForeignKey("ClientId");
                });
#pragma warning restore 612, 618
        }
    }
}
