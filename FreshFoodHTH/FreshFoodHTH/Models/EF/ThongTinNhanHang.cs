namespace FreshFoodHTH.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongTinNhanHang")]
    public partial class ThongTinNhanHang
    {
        [Key]
        public Guid IDThongTinNhanHang { get; set; }

        public Guid? IDNguoiDung { get; set; }

        [StringLength(200)]
        public string TenNhanHang { get; set; }

        [StringLength(12)]
        public string SdtNhanHang { get; set; }

        public string DiaChiNhanHang { get; set; }

        public bool? MacDinh { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(200)]
        public string ModifiedBy { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}
