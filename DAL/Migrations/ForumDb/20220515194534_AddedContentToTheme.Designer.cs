﻿// <auto-generated />

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations.ForumDb
{
    [DbContext(typeof(ForumDbContext))]
    [Migration("20220515194534_AddedContentToTheme")]
    partial class AddedContentToTheme
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Data.Entities.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Dislikes")
                        .HasColumnType("int");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.Property<int?>("ReferenceAnswerId")
                        .HasColumnType("int");

                    b.Property<int>("ThemeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ThemeId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Data.Entities.Section", b =>
                {
                    b.Property<int>("SectionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SectionTitleId")
                        .HasColumnType("int");

                    b.HasKey("SectionID");

                    b.HasIndex("SectionTitleId");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("Data.Entities.SectionTitle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SectionTitles");
                });

            modelBuilder.Entity("Data.Entities.SubSection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SectionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.ToTable("SubSections");
                });

            modelBuilder.Entity("Data.Entities.Theme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubSectionId")
                        .HasColumnType("int");

                    b.Property<int>("ViewCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubSectionId");

                    b.ToTable("Themes");
                });

            modelBuilder.Entity("Data.Entities.Answer", b =>
                {
                    b.HasOne("Data.Entities.Theme", null)
                        .WithMany("Answers")
                        .HasForeignKey("ThemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Entities.Section", b =>
                {
                    b.HasOne("Data.Entities.SectionTitle", "SectionTitle")
                        .WithMany("Sections")
                        .HasForeignKey("SectionTitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SectionTitle");
                });

            modelBuilder.Entity("Data.Entities.SubSection", b =>
                {
                    b.HasOne("Data.Entities.Section", null)
                        .WithMany("SubSections")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Entities.Theme", b =>
                {
                    b.HasOne("Data.Entities.SubSection", null)
                        .WithMany("Themes")
                        .HasForeignKey("SubSectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Entities.Section", b =>
                {
                    b.Navigation("SubSections");
                });

            modelBuilder.Entity("Data.Entities.SectionTitle", b =>
                {
                    b.Navigation("Sections");
                });

            modelBuilder.Entity("Data.Entities.SubSection", b =>
                {
                    b.Navigation("Themes");
                });

            modelBuilder.Entity("Data.Entities.Theme", b =>
                {
                    b.Navigation("Answers");
                });
#pragma warning restore 612, 618
        }
    }
}
