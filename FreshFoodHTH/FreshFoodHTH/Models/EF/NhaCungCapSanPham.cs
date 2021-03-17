namespace FreshFoodHTH.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhaCungCapSanPham")]
    public partial class NhaCungCapSanPham
    {
        [Key]
        public Guid IDNhaCungCapSanPham { get; set; }

        public Guid IDNhaCungCap { get; set; }

        public Guid IDSanPham { get; set; }

        [StringLength(100)]
        public string DonViTinh { get; set; }

        public decimal? GiaCungUng { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayCapNhat { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(200)]
        public string ModifiedBy { get; set; }

        public virtual NhaCungCap NhaCungCap { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
