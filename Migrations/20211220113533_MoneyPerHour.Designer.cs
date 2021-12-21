﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkController.WebApi.DataBase.Context;

#nullable disable

namespace WebApiWorkControllerServer.Migrations
{
    [DbContext(typeof(WorkControllerContext))]
    [Migration("20211220113533_MoneyPerHour")]
    partial class MoneyPerHour
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WorkController.WebApi.DataBase.Models.AllowsEmployee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int?>("ChiefId")
                        .HasColumnType("int");

                    b.Property<string>("EmployeeEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ChiefId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Employes");
                });

            modelBuilder.Entity("WorkController.WebApi.DataBase.Models.Time", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Milleseconds")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasAlternateKey("DateTime", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Times");
                });

            modelBuilder.Entity("WorkController.WebApi.DataBase.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int?>("ChiefId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("MoneyPerHour")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("ID");

                    b.HasIndex("ChiefId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WorkController.WebApi.DataBase.Models.AllowsEmployee", b =>
                {
                    b.HasOne("WorkController.WebApi.DataBase.Models.User", "Chief")
                        .WithMany()
                        .HasForeignKey("ChiefId");

                    b.HasOne("WorkController.WebApi.DataBase.Models.User", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Chief");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("WorkController.WebApi.DataBase.Models.Time", b =>
                {
                    b.HasOne("WorkController.WebApi.DataBase.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkController.WebApi.DataBase.Models.User", b =>
                {
                    b.HasOne("WorkController.WebApi.DataBase.Models.User", "Chief")
                        .WithMany()
                        .HasForeignKey("ChiefId");

                    b.Navigation("Chief");
                });
#pragma warning restore 612, 618
        }
    }
}
