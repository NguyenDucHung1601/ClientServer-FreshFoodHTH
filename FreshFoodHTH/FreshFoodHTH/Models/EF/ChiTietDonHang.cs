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
        public Guid IDChiTietDonHang { get; set; }

        public Guid IDDonHang { get; set; }

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
