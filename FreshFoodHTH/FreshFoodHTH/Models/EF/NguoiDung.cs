namespace FreshFoodHTH.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NguoiDung")]
    public partial class NguoiDung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NguoiDung()
        {
            ChiTietGioHangs = new HashSet<ChiTietGioHang>();
            DonHangs = new HashSet<DonHang>();
            MaGiamGiaKhachHangs = new HashSet<MaGiamGiaKhachHang>();
            TKThanhToanNguoiDungs = new HashSet<TKThanhToanNguoiDung>();
            ThongTinNhanHangs = new HashSet<ThongTinNhanHang>();
        }

        [Key]
        public Guid IDNguoiDung { get; set; }

        public Guid IDLoaiNguoiDung { get; set; }

        public Guid? IDLoaiKhachHang { get; set; }

        [StringLength(200)]
        public string Ten { get; set; }

        [StringLength(12)]
        public string DienThoai { get; set; }

        [StringLength(300)]
        public string Email { get; set; }

        public string DiaChi { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(200)]
        public string Password { get; set; }

        public string Avatar { get; set; }

        public decimal? TongTienGioHang { get; set; }

        public int? SoDonHangDaMua { get; set; }

        public decimal? TongTienHangDaMua { get; set; }

        public bool? TrangThai { get; set; }

        public DateTime? LanHoatDongGanNhat { get; set; }

        public bool IsAdmin { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(200)]
        public string ModifiedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonHang> DonHangs { get; set; }

        public virtual LoaiNguoiDung LoaiNguoiDung { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaGiamGiaKhachHang> MaGiamGiaKhachHangs { get; set; }

        public virtual PhanLoaiKhachHang PhanLoaiKhachHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TKThanhToanNguoiDung> TKThanhToanNguoiDungs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThongTinNhanHang> ThongTinNhanHangs { get; set; }
    }
}
