namespace FreshFoodHTH.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TKThanhToanNguoiDung")]
    public partial class TKThanhToanNguoiDung
    {
        [Key]
        [Column(Order = 0)]
        public Guid IDTaiKhoan { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid IDNguoiDung { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(500)]
        public string Password { get; set; }

        public decimal? TongTien { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(200)]
        public string ModifiedBy { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }

        public virtual TaiKhoanThanhToan TaiKhoanThanhToan { get; set; }
    }
}
