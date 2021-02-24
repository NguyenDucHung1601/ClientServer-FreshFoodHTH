namespace FreshFoodHTH.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietDonHang")]
    public partial class ChiTietDonHang
    {
        [Key]
        [Column(Order = 0)]
        public Guid IDDonHang { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid IDSanPham { get; set; }

        [StringLength(100)]
        public string DonViTinh { get; set; }

        public decimal? DonGiaBan { get; set; }

        public int? SoLuong { get; set; }

        public decimal? ThanhTien { get; set; }

        public virtual DonHang DonHang { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
