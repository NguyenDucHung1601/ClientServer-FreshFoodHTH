namespace FreshFoodHTH.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietGioHang")]
    public partial class ChiTietGioHang
    {
        [Key]
        public Guid IDChiTietGioHang { get; set; }

        public Guid IDKhachHang { get; set; }

        public Guid IDSanPham { get; set; }

        public int? SoLuong { get; set; }

        public decimal? ThanhTien { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
