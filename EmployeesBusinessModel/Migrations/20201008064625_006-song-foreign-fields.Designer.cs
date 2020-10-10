﻿// <auto-generated />
using System;
using EmployeesBusinessModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Chinook.BusinessModel.Migrations
{
    [DbContext(typeof(ChinookContext))]
    [Migration("20201008064625_006-song-foreign-fields")]
    partial class _006songforeignfields
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Chinook.BusinessModel.Models.Album", b =>
                {
                    b.Property<long>("AlbumId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AlbumId");

                    b.ToTable("Album");
                });

            modelBuilder.Entity("Chinook.BusinessModel.Models.Artist", b =>
                {
                    b.Property<long>("ArtistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AlbumId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArtistId");

                    b.HasIndex("AlbumId");

                    b.ToTable("Artist");
                });

            modelBuilder.Entity("Chinook.BusinessModel.Models.Client", b =>
                {
                    b.Property<long>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClientId");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("Chinook.BusinessModel.Models.Employee", b =>
                {
                    b.Property<long>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("DirectBossEmployeeId")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobPosition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.HasIndex("DirectBossEmployeeId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("Chinook.BusinessModel.Models.Genre", b =>
                {
                    b.Property<long>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GenreId");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("Chinook.BusinessModel.Models.Invoice", b =>
                {
                    b.Property<long>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("ClientId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("Total")
                        .HasColumnType("bigint");

                    b.HasKey("InvoiceId");

                    b.HasIndex("ClientId");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("Chinook.BusinessModel.Models.InvoiceDetail", b =>
                {
                    b.Property<long>("InvoiceDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<long?>("SongId")
                        .HasColumnType("bigint");

                    b.Property<long>("UnitPrice")
                        .HasColumnType("bigint");

                    b.HasKey("InvoiceDetailId");

                    b.HasIndex("SongId");

                    b.ToTable("InvoiceDetail");
                });

            modelBuilder.Entity("Chinook.BusinessModel.Models.Song", b =>
                {
                    b.Property<long>("SongId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AlbumId")
                        .HasColumnType("bigint");

                    b.Property<long>("GenreId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SongId");

                    b.HasIndex("AlbumId");

                    b.HasIndex("GenreId");

                    b.ToTable("Song");
                });

            modelBuilder.Entity("Chinook.BusinessModel.Models.Artist", b =>
                {
                    b.HasOne("Chinook.BusinessModel.Models.Album", "Album")
                        .WithMany("Artist")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Chinook.BusinessModel.Models.Employee", b =>
                {
                    b.HasOne("Chinook.BusinessModel.Models.Employee", "DirectBoss")
                        .WithMany()
                        .HasForeignKey("DirectBossEmployeeId");
                });

            modelBuilder.Entity("Chinook.BusinessModel.Models.Invoice", b =>
                {
                    b.HasOne("Chinook.BusinessModel.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId");
                });

            modelBuilder.Entity("Chinook.BusinessModel.Models.InvoiceDetail", b =>
                {
                    b.HasOne("Chinook.BusinessModel.Models.Song", "Song")
                        .WithMany()
                        .HasForeignKey("SongId");
                });

            modelBuilder.Entity("Chinook.BusinessModel.Models.Song", b =>
                {
                    b.HasOne("Chinook.BusinessModel.Models.Album", "Album")
                        .WithMany("Songs")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Chinook.BusinessModel.Models.Genre", "Genre")
                        .WithMany("Songs")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
