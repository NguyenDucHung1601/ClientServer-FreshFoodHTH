namespace FreshFoodHTH.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietHoaDonNhap")]
    public partial class ChiTietHoaDonNhap
    {
        [Key]
        [Column(Order = 0)]
        public Guid IDHoaDonNhap { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid IDSanPham { get; set; }

        [StringLength(100)]
        public string DonViTinh { get; set; }

        public decimal? DonGiaNhap { get; set; }

        public int? SoLuong { get; set; }

        public decimal? ThanhTien { get; set; }

        public virtual HoaDonNhap HoaDonNhap { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
