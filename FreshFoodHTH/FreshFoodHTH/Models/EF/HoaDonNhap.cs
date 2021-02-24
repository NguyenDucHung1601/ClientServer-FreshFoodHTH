namespace FreshFoodHTH.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDonNhap")]
    public partial class HoaDonNhap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDonNhap()
        {
            ChiTietHoaDonNhaps = new HashSet<ChiTietHoaDonNhap>();
        }

        [Key]
        public Guid IDHoaDonNhap { get; set; }

        public Guid? IDNhaCungCap { get; set; }

        public string GhiChu { get; set; }

        public decimal? TienHang { get; set; }

        public decimal? TienShip { get; set; }

        public decimal TienGiam { get; set; }

        public decimal? TongTien { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(200)]
        public string ModifiedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDonNhap> ChiTietHoaDonNhaps { get; set; }

        public virtual NhaCungCap NhaCungCap { get; set; }
    }
}
