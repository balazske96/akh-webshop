﻿// <auto-generated />
using System;
using AKHWebshop.Models.Shop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AKHWebshop.Migrations
{
    [DbContext(typeof(ShopDataContext))]
    [Migration("20201223203651_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AKHWebshop.Models.Auth.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnName("id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnName("access_failed_count")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnName("concurrency_stamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnName("email_confirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnName("lockout_enabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnName("lockout_end")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnName("normalized_email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnName("normalized_username")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PasswordHash")
                        .HasColumnName("password_hash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnName("phone_number")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnName("phone_number_confirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnName("security_stamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnName("two_factor_enabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnName("username")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("AKHWebshop.Models.Auth.UserRole", b =>
                {
                    b.Property<string>("RoleId")
                        .HasColumnName("role_id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("user_role");
                });

            modelBuilder.Entity("AKHWebshop.Models.Shop.Data.BillingInfo", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnName("city")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnName("country")
                        .HasColumnType("varchar(256)");

                    b.Property<ushort?>("Door")
                        .HasColumnName("door")
                        .HasColumnType("smallint unsigned");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("first_name")
                        .HasColumnType("varchar(256)");

                    b.Property<byte?>("Floor")
                        .HasColumnName("floor")
                        .HasColumnType("tinyint unsigned");

                    b.Property<ushort>("HouseNumber")
                        .HasColumnName("house_number")
                        .HasColumnType("smallint unsigned");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("last_name")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PublicSpaceName")
                        .IsRequired()
                        .HasColumnName("public_space_name")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PublicSpaceType")
                        .IsRequired()
                        .HasColumnName("public_space_type")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("State")
                        .HasColumnName("state")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnName("zip_code")
                        .HasColumnType("varchar(4)");

                    b.HasKey("Id");

                    b.ToTable("billing_info");
                });

            modelBuilder.Entity("AKHWebshop.Models.Shop.Data.Order", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("varchar(36)");

                    b.Property<bool>("BillingInfoSameAsOrderInfo")
                        .HasColumnName("same_billing_info")
                        .HasColumnType("bool");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnName("city")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Comment")
                        .HasColumnName("comment")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnName("country")
                        .HasColumnType("varchar(256)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<ushort?>("Door")
                        .HasColumnName("door")
                        .HasColumnType("smallint unsigned");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("first_name")
                        .HasColumnType("varchar(256)");

                    b.Property<byte?>("Floor")
                        .HasColumnName("floor")
                        .HasColumnType("tinyint unsigned");

                    b.Property<ushort>("HouseNumber")
                        .HasColumnName("house_number")
                        .HasColumnType("smallint unsigned");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("last_name")
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("Paid")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("payed")
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<string>("PublicSpaceName")
                        .IsRequired()
                        .HasColumnName("public_space_name")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PublicSpaceType")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnName("public_space_type")
                        .HasColumnType("varchar(20)")
                        .HasDefaultValue("Utca");

                    b.Property<bool>("Shipped")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("shipped")
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<string>("State")
                        .HasColumnName("state")
                        .HasColumnType("varchar(256)");

                    b.Property<uint>("TotalPrice")
                        .HasColumnName("total_price")
                        .HasColumnType("int unsigned");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnName("zip_code")
                        .HasColumnType("varchar(4)");

                    b.Property<string>("billing_info_id")
                        .IsRequired()
                        .HasColumnType("varchar(36)");

                    b.HasKey("Id");

                    b.HasIndex("billing_info_id");

                    b.ToTable("order");
                });

            modelBuilder.Entity("AKHWebshop.Models.Shop.Data.OrderItem", b =>
                {
                    b.Property<string>("OrderId")
                        .HasColumnName("order_id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("ProductId")
                        .HasColumnName("product_id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Size")
                        .HasColumnName("size")
                        .HasColumnType("varchar(36)");

                    b.Property<ushort>("Amount")
                        .HasColumnName("amount")
                        .HasColumnType("smallint unsigned");

                    b.HasKey("OrderId", "ProductId", "Size");

                    b.ToTable("order_item");
                });

            modelBuilder.Entity("AKHWebshop.Models.Shop.Data.Product", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnName("display_name")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("ImageName")
                        .HasColumnName("image_name")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("varchar(256)");

                    b.Property<uint>("Price")
                        .HasColumnName("price")
                        .HasColumnType("int unsigned");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnName("status")
                        .HasColumnType("varchar(256)")
                        .HasDefaultValue("Hidden");

                    b.HasKey("Id");

                    b.HasIndex("DisplayName")
                        .IsUnique();

                    b.HasIndex("ImageName")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("product");
                });

            modelBuilder.Entity("AKHWebshop.Models.Shop.Data.SizeRecord", b =>
                {
                    b.Property<string>("ProductId")
                        .HasColumnName("product_id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Size")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("size")
                        .HasColumnType("varchar(36)")
                        .HasDefaultValue("UNDEFINED");

                    b.Property<ushort>("Quantity")
                        .HasColumnName("quantity")
                        .HasColumnType("smallint unsigned");

                    b.HasKey("ProductId", "Size");

                    b.ToTable("size_record");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("role");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole");
                });

            modelBuilder.Entity("AKHWebshop.Models.Auth.AppRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole");

                    b.HasDiscriminator().HasValue("AppRole");

                    b.HasData(
                        new
                        {
                            Id = "dafcb95a-d2ea-4d9d-bfbf-c58afea13c17",
                            ConcurrencyStamp = "15e3b432-ba6a-45b9-9b9c-b95ca8038bce",
                            Name = "admin",
                            NormalizedName = "Admin"
                        },
                        new
                        {
                            Id = "62564147-c6a8-4bd4-a037-ae0a09818745",
                            ConcurrencyStamp = "4de4b21f-b066-4571-9f75-b497067b2aaf",
                            Name = "user",
                            NormalizedName = "User"
                        });
                });

            modelBuilder.Entity("AKHWebshop.Models.Auth.UserRole", b =>
                {
                    b.HasOne("AKHWebshop.Models.Auth.AppRole", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AKHWebshop.Models.Auth.AppUser", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AKHWebshop.Models.Shop.Data.Order", b =>
                {
                    b.HasOne("AKHWebshop.Models.Shop.Data.BillingInfo", "BillingInfo")
                        .WithMany()
                        .HasForeignKey("billing_info_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AKHWebshop.Models.Shop.Data.OrderItem", b =>
                {
                    b.HasOne("AKHWebshop.Models.Shop.Data.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AKHWebshop.Models.Shop.Data.SizeRecord", b =>
                {
                    b.HasOne("AKHWebshop.Models.Shop.Data.Product", null)
                        .WithMany("Amount")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
