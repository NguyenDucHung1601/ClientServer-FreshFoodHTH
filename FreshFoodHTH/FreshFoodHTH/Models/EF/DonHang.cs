namespace FreshFoodHTH.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonHang")]
    public partial class DonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHang()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }

        [Key]
        public Guid IDDonHang { get; set; }

        public Guid IDKhachHang { get; set; }

        public string GhiChu { get; set; }

        public decimal? TienHang { get; set; }

        public decimal? TienShip { get; set; }

        public decimal? TienGiam { get; set; }

        public decimal? TongTien { get; set; }

        public Guid IDTrangThai { get; set; }

        public Guid IDPhuongThucThanhToan { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(200)]
        public string ModifiedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }

        public virtual PhuongThucThanhToan PhuongThucThanhToan { get; set; }

        public virtual TrangThai TrangThai { get; set; }
    }
}
