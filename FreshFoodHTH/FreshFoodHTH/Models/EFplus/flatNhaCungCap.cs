using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodHTH.Models.EFplus
{
    public class flatNhaCungCap
    {
        public Guid IDNhaCungCap { get; set; }
        public string Ten { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}