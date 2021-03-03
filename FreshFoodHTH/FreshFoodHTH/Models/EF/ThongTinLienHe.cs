namespace FreshFoodHTH.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongTinLienHe")]
    public partial class ThongTinLienHe
    {
        public Guid ID { get; set; }

        [Required]
        [StringLength(400)]
        public string TenCuaHang { get; set; }

        public string DiaChi { get; set; }

        [StringLength(12)]
        public string DienThoai1 { get; set; }

        [StringLength(12)]
        public string DienThoai2 { get; set; }

        [StringLength(50)]
        public string GioMoCua { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public string LinkFacebook { get; set; }

        public string LinkYoutube { get; set; }

        public string LinkInstagram { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(200)]
        public string ModifiedBy { get; set; }
    }
}
