namespace FreshFoodHTH.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoanThanhToan")]
    public partial class TaiKhoanThanhToan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiKhoanThanhToan()
        {
            TKThanhToanNguoiDungs = new HashSet<TKThanhToanNguoiDung>();
        }

        [Key]
        public Guid IDTaiKhoan { get; set; }

        [StringLength(20)]
        public string VietTat { get; set; }

        [StringLength(200)]
        public string Ten { get; set; }

        [StringLength(200)]
        public string LoaiTaiKhoan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TKThanhToanNguoiDung> TKThanhToanNguoiDungs { get; set; }
    }
}
