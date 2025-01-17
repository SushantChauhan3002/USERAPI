﻿// <auto-generated />
using System;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    [DbContext(typeof(UserDbContext))]
    [Migration("20241224094700_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.AdminRole", b =>
                {
                    b.Property<int>("AdminRoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AdminRoleID"));

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("AdminRoleID");

                    b.ToTable("AdminRoles");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.AdminUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("AdminUsers");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.AdminUserRole", b =>
                {
                    b.Property<int>("AdminUserRoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AdminUserRoleID"));

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("AdminUserRoleID");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AdminUserRoles");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.AdminUserUtility", b =>
                {
                    b.Property<int>("AdminUserUtilityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AdminUserUtilityID"));

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("UtilityId")
                        .HasColumnType("integer");

                    b.HasKey("AdminUserUtilityID");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("AdminUserUtilities");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.UserUtility", b =>
                {
                    b.Property<int>("UserUtilityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserUtilityID"));

                    b.Property<string>("UtilityDetails")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserUtilityID");

                    b.ToTable("UserUtilitys");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.AdminUserRole", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.AdminRole", "AdminRole")
                        .WithMany("AdminUserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanArchitecture.Domain.Entities.AdminUser", "AdminUser")
                        .WithMany("AdminUserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AdminRole");

                    b.Navigation("AdminUser");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.AdminUserUtility", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.AdminUser", "AdminUser")
                        .WithOne("AdminUserUtilities")
                        .HasForeignKey("CleanArchitecture.Domain.Entities.AdminUserUtility", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AdminUser");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.AdminRole", b =>
                {
                    b.Navigation("AdminUserRoles");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.AdminUser", b =>
                {
                    b.Navigation("AdminUserRoles");

                    b.Navigation("AdminUserUtilities")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
