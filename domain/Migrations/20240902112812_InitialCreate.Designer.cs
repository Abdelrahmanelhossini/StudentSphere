﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using domain_and_repo;

#nullable disable

namespace domain_and_repo.Migrations
{
    [DbContext(typeof(Db_context))]
    [Migration("20240902112812_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("domain_and_repo.models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CourseId");

                    b.ToTable("courses");
                });

            modelBuilder.Entity("domain_and_repo.models.Enroll", b =>
                {
                    b.Property<int>("EnrollId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EnrollId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int?>("degree")
                        .HasColumnType("int");

                    b.HasKey("EnrollId");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("enroll");
                });

            modelBuilder.Entity("domain_and_repo.models.Level", b =>
                {
                    b.Property<int>("LevelID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LevelID"));

                    b.Property<decimal>("Addition")
                        .HasColumnType("decimal(18, 0)");

                    b.Property<decimal>("Base")
                        .HasColumnType("decimal(18, 0)");

                    b.Property<decimal>("Deduction")
                        .HasColumnType("decimal(18, 0)");

                    b.Property<string>("LevelName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("LevelID");

                    b.ToTable("levels");
                });

            modelBuilder.Entity("domain_and_repo.models.Payment", b =>
                {
                    b.Property<int>("PymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PymentId"));

                    b.Property<int>("LevelId")
                        .HasColumnType("int");

                    b.Property<int?>("PaidValue")
                        .HasColumnType("int");

                    b.Property<DateTime>("PymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<decimal?>("TotalRequirs")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PymentId");

                    b.HasIndex("LevelId");

                    b.HasIndex("StudentId");

                    b.ToTable("payments");
                });

            modelBuilder.Entity("domain_and_repo.models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal?>("Installments")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("LevelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<decimal?>("TotalFees")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("StudentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("domain_and_repo.models.Enroll", b =>
                {
                    b.HasOne("domain_and_repo.models.Course", "Course")
                        .WithMany("enrolls")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("domain_and_repo.models.Student", "Student")
                        .WithMany("Enrolls")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("domain_and_repo.models.Payment", b =>
                {
                    b.HasOne("domain_and_repo.models.Level", "level")
                        .WithMany("Payments")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("domain_and_repo.models.Student", "Student")
                        .WithMany("Payments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("level");
                });

            modelBuilder.Entity("domain_and_repo.models.Course", b =>
                {
                    b.Navigation("enrolls");
                });

            modelBuilder.Entity("domain_and_repo.models.Level", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("domain_and_repo.models.Student", b =>
                {
                    b.Navigation("Enrolls");

                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}