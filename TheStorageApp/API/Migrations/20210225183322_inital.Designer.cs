﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheStorageApp.API.Data;

namespace TheStorageApp.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210225183322_inital")]
    partial class inital
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Value")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ReceiptTag", b =>
                {
                    b.Property<string>("ReceiptsId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("TagsId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("ReceiptsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("ReceiptTag");
                });

            modelBuilder.Entity("TheStorageApp.API.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = "607183e7-b778-4ce1-9754-89a0e5f3d7bf",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "44067204-ce00-447e-aa62-e64578b771cb",
                            Email = "system@email.com",
                            EmailConfirmed = false,
                            FirstName = "system",
                            LastName = "user",
                            LockoutEnabled = false,
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "960203d5-d5f6-4333-b54c-d0a7b2fd7f30",
                            TwoFactorEnabled = false,
                            UserName = "<SYSTEM>"
                        });
                });

            modelBuilder.Entity("TheStorageApp.API.Models.Category", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Color")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CreatedById")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ReceiptId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("ReceiptId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = "c3f28ac9-41a2-40cc-ad41-f1af2ade333d",
                            Color = "555555",
                            CreatedById = "607183e7-b778-4ce1-9754-89a0e5f3d7bf",
                            CreatedOn = new DateTime(2021, 2, 25, 20, 33, 21, 929, DateTimeKind.Local).AddTicks(1447),
                            ModifiedById = "607183e7-b778-4ce1-9754-89a0e5f3d7bf",
                            ModifiedOn = new DateTime(2021, 2, 25, 20, 33, 21, 929, DateTimeKind.Local).AddTicks(1900),
                            Name = "<DEFAULT>"
                        });
                });

            modelBuilder.Entity("TheStorageApp.API.Models.LogEntry", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("CreatedById")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ErrorCode")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ModifiedById1")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById1");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("TheStorageApp.API.Models.Receipt", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("CategoryId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("CreatedById")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ReceiptHolderId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ShopId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("ReceiptHolderId");

                    b.HasIndex("ShopId");

                    b.ToTable("Receipts");
                });

            modelBuilder.Entity("TheStorageApp.API.Models.ReceiptImage", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("CreatedById")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("longblob");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("ReceiptId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.ToTable("ReceiptImages");
                });

            modelBuilder.Entity("TheStorageApp.API.Models.Shop", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Address")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CreatedById")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("GPSLocation")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Website")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.ToTable("Shops");

                    b.HasData(
                        new
                        {
                            Id = "97176ee6-6301-4af1-b0cd-52c0b0222bb8",
                            Address = "",
                            CreatedById = "607183e7-b778-4ce1-9754-89a0e5f3d7bf",
                            CreatedOn = new DateTime(2021, 2, 25, 20, 33, 21, 943, DateTimeKind.Local).AddTicks(5049),
                            GPSLocation = "",
                            ModifiedById = "607183e7-b778-4ce1-9754-89a0e5f3d7bf",
                            ModifiedOn = new DateTime(2021, 2, 25, 20, 33, 21, 943, DateTimeKind.Local).AddTicks(5475),
                            Name = "<DEFAULT>",
                            Website = ""
                        });
                });

            modelBuilder.Entity("TheStorageApp.API.Models.Tag", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Color")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CreatedById")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = "a32a340f-8c59-4fec-8569-cf7fe0efa671",
                            Color = "555555",
                            CreatedById = "607183e7-b778-4ce1-9754-89a0e5f3d7bf",
                            CreatedOn = new DateTime(2021, 2, 25, 20, 33, 21, 947, DateTimeKind.Local).AddTicks(3470),
                            ModifiedById = "607183e7-b778-4ce1-9754-89a0e5f3d7bf",
                            ModifiedOn = new DateTime(2021, 2, 25, 20, 33, 21, 947, DateTimeKind.Local).AddTicks(3900),
                            Name = "<DEFAULT>"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TheStorageApp.API.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TheStorageApp.API.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TheStorageApp.API.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("TheStorageApp.API.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReceiptTag", b =>
                {
                    b.HasOne("TheStorageApp.API.Models.Receipt", null)
                        .WithMany()
                        .HasForeignKey("ReceiptsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TheStorageApp.API.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TheStorageApp.API.Models.Category", b =>
                {
                    b.HasOne("TheStorageApp.API.Models.AppUser", "CreatedBy")
                        .WithMany("CreatedByCategories")
                        .HasForeignKey("CreatedById");

                    b.HasOne("TheStorageApp.API.Models.AppUser", "ModifiedBy")
                        .WithMany("ModifiedByCategories")
                        .HasForeignKey("ModifiedById");

                    b.HasOne("TheStorageApp.API.Models.Receipt", null)
                        .WithMany("Categories")
                        .HasForeignKey("ReceiptId");

                    b.Navigation("CreatedBy");

                    b.Navigation("ModifiedBy");
                });

            modelBuilder.Entity("TheStorageApp.API.Models.LogEntry", b =>
                {
                    b.HasOne("TheStorageApp.API.Models.AppUser", "CreatedBy")
                        .WithMany("CreatedByLogEntries")
                        .HasForeignKey("CreatedById");

                    b.HasOne("TheStorageApp.API.Models.AppUser", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById1");

                    b.Navigation("CreatedBy");

                    b.Navigation("ModifiedBy");
                });

            modelBuilder.Entity("TheStorageApp.API.Models.Receipt", b =>
                {
                    b.HasOne("TheStorageApp.API.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("TheStorageApp.API.Models.AppUser", "CreatedBy")
                        .WithMany("CreatedByReceipts")
                        .HasForeignKey("CreatedById");

                    b.HasOne("TheStorageApp.API.Models.AppUser", "ModifiedBy")
                        .WithMany("ModifiedByReceipts")
                        .HasForeignKey("ModifiedById");

                    b.HasOne("TheStorageApp.API.Models.AppUser", "ReceiptHolder")
                        .WithMany("Receipts")
                        .HasForeignKey("ReceiptHolderId");

                    b.HasOne("TheStorageApp.API.Models.Shop", "Shop")
                        .WithMany()
                        .HasForeignKey("ShopId");

                    b.Navigation("Category");

                    b.Navigation("CreatedBy");

                    b.Navigation("ModifiedBy");

                    b.Navigation("ReceiptHolder");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("TheStorageApp.API.Models.ReceiptImage", b =>
                {
                    b.HasOne("TheStorageApp.API.Models.AppUser", "CreatedBy")
                        .WithMany("CreatedByReceiptImages")
                        .HasForeignKey("CreatedById");

                    b.HasOne("TheStorageApp.API.Models.Receipt", "Receipt")
                        .WithMany("RecipetImages")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TheStorageApp.API.Models.AppUser", "ModifiedBy")
                        .WithMany("ModifiedByReceiptImages")
                        .HasForeignKey("ModifiedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("ModifiedBy");

                    b.Navigation("Receipt");
                });

            modelBuilder.Entity("TheStorageApp.API.Models.Shop", b =>
                {
                    b.HasOne("TheStorageApp.API.Models.AppUser", "CreatedBy")
                        .WithMany("CreatedByShops")
                        .HasForeignKey("CreatedById");

                    b.HasOne("TheStorageApp.API.Models.AppUser", "ModifiedBy")
                        .WithMany("ModifiedByShops")
                        .HasForeignKey("ModifiedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("ModifiedBy");
                });

            modelBuilder.Entity("TheStorageApp.API.Models.Tag", b =>
                {
                    b.HasOne("TheStorageApp.API.Models.AppUser", "CreatedBy")
                        .WithMany("CreatedByTags")
                        .HasForeignKey("CreatedById");

                    b.HasOne("TheStorageApp.API.Models.AppUser", "ModifiedBy")
                        .WithMany("ModifiedByTags")
                        .HasForeignKey("ModifiedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("ModifiedBy");
                });

            modelBuilder.Entity("TheStorageApp.API.Models.AppUser", b =>
                {
                    b.Navigation("CreatedByCategories");

                    b.Navigation("CreatedByLogEntries");

                    b.Navigation("CreatedByReceiptImages");

                    b.Navigation("CreatedByReceipts");

                    b.Navigation("CreatedByShops");

                    b.Navigation("CreatedByTags");

                    b.Navigation("ModifiedByCategories");

                    b.Navigation("ModifiedByReceiptImages");

                    b.Navigation("ModifiedByReceipts");

                    b.Navigation("ModifiedByShops");

                    b.Navigation("ModifiedByTags");

                    b.Navigation("Receipts");
                });

            modelBuilder.Entity("TheStorageApp.API.Models.Receipt", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("RecipetImages");
                });
#pragma warning restore 612, 618
        }
    }
}