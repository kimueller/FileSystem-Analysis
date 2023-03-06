﻿// <auto-generated />
using System;
using LAP_RekursivDateisystem.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LAP_RekursivDateisystem.DAL.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230220140659_a")]
    partial class a
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LAP_RekursivDateisystem.DAL.Models.Directory", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("Name");

                    b.ToTable("Directories");
                });

            modelBuilder.Entity("LAP_RekursivDateisystem.DAL.Models.File", b =>
                {
                    b.Property<int>("FileID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FileID"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DirectoryID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("Size")
                        .HasColumnType("real");

                    b.HasKey("FileID");

                    b.HasIndex("DirectoryID");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("LAP_RekursivDateisystem.DAL.Models.File", b =>
                {
                    b.HasOne("LAP_RekursivDateisystem.DAL.Models.Directory", "Directory")
                        .WithMany("Files")
                        .HasForeignKey("DirectoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Directory");
                });

            modelBuilder.Entity("LAP_RekursivDateisystem.DAL.Models.Directory", b =>
                {
                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}
