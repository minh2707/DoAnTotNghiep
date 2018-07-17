using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.Models
{
    public class NguoiDungEntity : IdentityUser
    {
        public string Ho { get; set; }
        public string Ten { get; set; }
        public string TenHienThi
        {
            get
            {
                return this.Ho + " " + this.Ten;
            }
        }

        public string DiaChi { get; set; }

    }
    public partial class LapTopStoreContext : IdentityDbContext<NguoiDungEntity>
    {
        public virtual DbSet<ChiTietDonHang> ChiTietDonHang { get; set; }
        public virtual DbSet<DonHang> DonHang { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        public virtual DbSet<LoaiSanPham> LoaiSanPham { get; set; }
        public virtual DbSet<SanPham> SanPham { get; set; }

        public LapTopStoreContext(DbContextOptions<LapTopStoreContext> options)
            : base(options)
        { }

        public LapTopStoreContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=MINH;Initial Catalog=LapTopStore;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietDonHang>(entity =>
            {
                entity.Property(e => e.IddonHang).HasColumnName("IDDonHang");

                entity.Property(e => e.IdsanPham).HasColumnName("IDSanPham");

                entity.HasOne(d => d.IddonHangNavigation)
                    .WithMany(p => p.ChiTietDonHang)
                    .HasForeignKey(d => d.IddonHang);

                entity.HasOne(d => d.IdsanPhamNavigation)
                    .WithMany(p => p.ChiTietDonHang)
                    .HasForeignKey(d => d.IdsanPham)
                    .HasConstraintName("FK_ChiTietDonHang_Product_ProductID");
            });

            modelBuilder.Entity<DonHang>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DiaChi)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.IdkhachHang)
                    .IsRequired()
                    .HasColumnName("IDKhachHang")
                    .HasMaxLength(100);

                entity.Property(e => e.NgayDat).HasColumnType("datetime");

                entity.Property(e => e.NgayGiao).HasColumnType("datetime");

                entity.HasOne(d => d.IdkhachHangNavigation)
                    .WithMany(p => p.DonHang)
                    .HasForeignKey(d => d.IdkhachHang);
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.DiaChi).HasMaxLength(1000);


                entity.Property(e => e.HoTen).IsRequired();

            });

            modelBuilder.Entity<LoaiSanPham>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MoTa).HasColumnType("text");

                entity.Property(e => e.Ten)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Hinh).IsRequired();

                entity.Property(e => e.Idloai).HasColumnName("IDLoai");

                entity.Property(e => e.MoTa).HasMaxLength(2000);

                entity.Property(e => e.Ten)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdloaiNavigation)
                    .WithMany(p => p.SanPham)
                    .HasForeignKey(d => d.Idloai)
                    .HasConstraintName("FK_Product_Loai");
            });

            base.OnModelCreating(modelBuilder);

        }
    }
}
