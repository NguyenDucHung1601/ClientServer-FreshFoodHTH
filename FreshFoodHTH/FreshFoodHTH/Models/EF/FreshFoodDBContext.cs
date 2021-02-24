using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FreshFoodHTH.Models.EF
{
    public partial class FreshFoodDBContext : DbContext
    {
        public FreshFoodDBContext()
            : base("name=FreshFoodDBContext")
        {
        }

        public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }
        public virtual DbSet<ChiTietHoaDonNhap> ChiTietHoaDonNhaps { get; set; }
        public virtual DbSet<DonHang> DonHangs { get; set; }
        public virtual DbSet<HoaDonNhap> HoaDonNhaps { get; set; }
        public virtual DbSet<LoaiNguoiDung> LoaiNguoiDungs { get; set; }
        public virtual DbSet<MaGiamGia> MaGiamGias { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<NhaCungCapSanPham> NhaCungCapSanPhams { get; set; }
        public virtual DbSet<PhanLoaiKhachHang> PhanLoaiKhachHangs { get; set; }
        public virtual DbSet<PhuongThucThanhToan> PhuongThucThanhToans { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<TaiKhoanThanhToan> TaiKhoanThanhToans { get; set; }
        public virtual DbSet<TheLoai> TheLoais { get; set; }
        public virtual DbSet<TKThanhToanNguoiDung> TKThanhToanNguoiDungs { get; set; }
        public virtual DbSet<TrangThai> TrangThais { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietDonHang>()
                .Property(e => e.DonGiaBan)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ChiTietDonHang>()
                .Property(e => e.ThanhTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ChiTietGioHang>()
                .Property(e => e.ThanhTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ChiTietHoaDonNhap>()
                .Property(e => e.DonGiaNhap)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ChiTietHoaDonNhap>()
                .Property(e => e.ThanhTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DonHang>()
                .Property(e => e.TienHang)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DonHang>()
                .Property(e => e.TienShip)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DonHang>()
                .Property(e => e.TienGiam)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DonHang>()
                .Property(e => e.TongTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DonHang>()
                .HasMany(e => e.ChiTietDonHangs)
                .WithRequired(e => e.DonHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDonNhap>()
                .Property(e => e.TienHang)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HoaDonNhap>()
                .Property(e => e.TienShip)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HoaDonNhap>()
                .Property(e => e.TienGiam)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HoaDonNhap>()
                .Property(e => e.TongTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HoaDonNhap>()
                .HasMany(e => e.ChiTietHoaDonNhaps)
                .WithRequired(e => e.HoaDonNhap)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiNguoiDung>()
                .HasMany(e => e.NguoiDungs)
                .WithRequired(e => e.LoaiNguoiDung)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MaGiamGia>()
                .Property(e => e.MaGiamGia1)
                .IsUnicode(false);

            modelBuilder.Entity<MaGiamGia>()
                .Property(e => e.TienGiam)
                .HasPrecision(18, 0);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.DienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.TongTienGioHang)
                .HasPrecision(18, 0);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.TongTienHangDaMua)
                .HasPrecision(18, 0);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.ChiTietGioHangs)
                .WithRequired(e => e.NguoiDung)
                .HasForeignKey(e => e.IDKhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.DonHangs)
                .WithRequired(e => e.NguoiDung)
                .HasForeignKey(e => e.IDKhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.TKThanhToanNguoiDungs)
                .WithRequired(e => e.NguoiDung)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.MaGiamGias)
                .WithMany(e => e.NguoiDungs)
                .Map(m => m.ToTable("MaGiamGiaKhachHang").MapLeftKey("IDKhachHang").MapRightKey("IDMaGiamGia"));

            modelBuilder.Entity<NhaCungCap>()
                .Property(e => e.DienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<NhaCungCap>()
                .HasMany(e => e.NhaCungCapSanPhams)
                .WithRequired(e => e.NhaCungCap)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhaCungCapSanPham>()
                .Property(e => e.GiaCungUng)
                .HasPrecision(18, 0);

            modelBuilder.Entity<PhuongThucThanhToan>()
                .HasMany(e => e.DonHangs)
                .WithRequired(e => e.PhuongThucThanhToan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.MaSo)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.GiaTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.GiaKhuyenMai)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietDonHangs)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietGioHangs)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietHoaDonNhaps)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.NhaCungCapSanPhams)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoanThanhToan>()
                .Property(e => e.VietTat)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoanThanhToan>()
                .HasMany(e => e.TKThanhToanNguoiDungs)
                .WithRequired(e => e.TaiKhoanThanhToan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TheLoai>()
                .Property(e => e.MaSo)
                .IsUnicode(false);

            modelBuilder.Entity<TheLoai>()
                .HasMany(e => e.SanPhams)
                .WithRequired(e => e.TheLoai)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TKThanhToanNguoiDung>()
                .Property(e => e.TaiKhoan)
                .IsUnicode(false);

            modelBuilder.Entity<TKThanhToanNguoiDung>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<TKThanhToanNguoiDung>()
                .Property(e => e.TongTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<TrangThai>()
                .HasMany(e => e.DonHangs)
                .WithRequired(e => e.TrangThai)
                .WillCascadeOnDelete(false);
        }
    }
}
