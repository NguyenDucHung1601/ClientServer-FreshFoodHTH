namespace FreshFoodHTH.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MaGiamGiaKhachHang")]
    public partial class MaGiamGiaKhachHang
    {
        [Key]
        [Column(Order = 0)]
        public Guid IDMaGiamGia { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid IDKhacHang { get; set; }

        public bool? ConHanSuDung { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(200)]
        public string ModifiedBy { get; set; }

        public virtual MaGiamGia MaGiamGia { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}
