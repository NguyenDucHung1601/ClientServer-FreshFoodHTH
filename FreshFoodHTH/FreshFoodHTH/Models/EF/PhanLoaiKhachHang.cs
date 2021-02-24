namespace FreshFoodHTH.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhanLoaiKhachHang")]
    public partial class PhanLoaiKhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhanLoaiKhachHang()
        {
            NguoiDungs = new HashSet<NguoiDung>();
        }

        [Key]
        public Guid IDLoaiKhachHang { get; set; }

        [StringLength(200)]
        public string Ten { get; set; }

        public string DieuKien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiDung> NguoiDungs { get; set; }
    }
}
