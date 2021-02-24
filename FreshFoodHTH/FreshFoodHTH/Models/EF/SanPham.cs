namespace FreshFoodHTH.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
            ChiTietGioHangs = new HashSet<ChiTietGioHang>();
            ChiTietHoaDonNhaps = new HashSet<ChiTietHoaDonNhap>();
            NhaCungCapSanPhams = new HashSet<NhaCungCapSanPham>();
        }

        [Key]
        public Guid IDSanPham { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaOrder { get; set; }

        [StringLength(50)]
        public string MaSo { get; set; }

        [StringLength(200)]
        public string Ten { get; set; }

        [StringLength(100)]
        public string DonViTinh { get; set; }

        public decimal? GiaTien { get; set; }

        public decimal? GiaKhuyenMai { get; set; }

        public string HinhAnh { get; set; }

        public string MoTa { get; set; }

        public bool? CoSan { get; set; }

        public long? SoLuong { get; set; }

        public Guid IDTheLoai { get; set; }

        public int? SoLuotXem { get; set; }

        public int? SoLuotMua { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(200)]
        public string ModifiedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDonNhap> ChiTietHoaDonNhaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhaCungCapSanPham> NhaCungCapSanPhams { get; set; }

        public virtual TheLoai TheLoai { get; set; }
    }
}
