﻿// <auto-generated />
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace API.Migrations
{
    [DbContext(typeof(LapTopStoreContext))]
    partial class LapTopStoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("API.Models.ChiTietDonHang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("DonGia");

                    b.Property<double>("GiamGia");

                    b.Property<int>("IddonHang")
                        .HasColumnName("IDDonHang");

                    b.Property<int>("IdsanPham")
                        .HasColumnName("IDSanPham");

                    b.Property<int>("SoLuong");

                    b.HasKey("Id");

                    b.HasIndex("IddonHang");

                    b.HasIndex("IdsanPham");

                    b.ToTable("ChiTietDonHang");
                });

            modelBuilder.Entity("API.Models.DonHang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<string>("HinhThucThanhToan")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<string>("IdkhachHang")
                        .IsRequired()
                        .HasColumnName("IDKhachHang")
                        .HasMaxLength(100);

                    b.Property<string>("MoTa");

                    b.Property<DateTime>("NgayDat")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("NgayGiao")
                        .HasColumnType("datetime");

                    b.Property<double>("SoLuong");

                    b.Property<bool>("TrangThai");

                    b.HasKey("Id");

                    b.HasIndex("IdkhachHang");

                    b.ToTable("DonHang");
                });

            modelBuilder.Entity("API.Models.KhachHang", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasMaxLength(100);

                    b.Property<string>("DiaChi")
                        .HasMaxLength(1000);

                    b.Property<string>("HoTen")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("KhachHang");
                });

            modelBuilder.Entity("API.Models.LoaiSanPham", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<string>("MoTa")
                        .HasColumnType("text");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("LoaiSanPham");
                });

            modelBuilder.Entity("API.Models.NguoiDungEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("DiaChi");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("Ho");

                    b.Property<string>("IdKhachHang");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Ten");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("IdKhachHang")
                        .IsUnique()
                        .HasFilter("[IdKhachHang] IS NOT NULL");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("API.Models.SanPham", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<double>("Gia");

                    b.Property<double>("GiamGia");

                    b.Property<string>("Hinh")
                        .IsRequired();

                    b.Property<int>("Idloai")
                        .HasColumnName("IDLoai");

                    b.Property<string>("MoTa")
                        .HasMaxLength(2000);

                    b.Property<int>("SoLuong");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("TrangThai");

                    b.HasKey("Id");

                    b.HasIndex("Idloai");

                    b.ToTable("SanPham");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("API.Models.ChiTietDonHang", b =>
                {
                    b.HasOne("API.Models.DonHang", "IddonHangNavigation")
                        .WithMany("ChiTietDonHang")
                        .HasForeignKey("IddonHang")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API.Models.SanPham", "IdsanPhamNavigation")
                        .WithMany("ChiTietDonHang")
                        .HasForeignKey("IdsanPham")
                        .HasConstraintName("FK_ChiTietDonHang_Product_ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Models.DonHang", b =>
                {
                    b.HasOne("API.Models.KhachHang", "IdkhachHangNavigation")
                        .WithMany("DonHang")
                        .HasForeignKey("IdkhachHang")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Models.NguoiDungEntity", b =>
                {
                    b.HasOne("API.Models.KhachHang", "KhachHang")
                        .WithOne("TaiKhoan")
                        .HasForeignKey("API.Models.NguoiDungEntity", "IdKhachHang");
                });

            modelBuilder.Entity("API.Models.SanPham", b =>
                {
                    b.HasOne("API.Models.LoaiSanPham", "IdloaiNavigation")
                        .WithMany("SanPham")
                        .HasForeignKey("Idloai")
                        .HasConstraintName("FK_Product_Loai")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("API.Models.NguoiDungEntity")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("API.Models.NguoiDungEntity")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API.Models.NguoiDungEntity")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("API.Models.NguoiDungEntity")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}