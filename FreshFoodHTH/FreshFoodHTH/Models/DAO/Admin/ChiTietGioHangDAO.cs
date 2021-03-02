using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using FreshFoodHTH.Models.EFplus;

namespace FreshFoodHTH.Models.DAO.Admin
{
    public class ChiTietGioHangDAO
    {
        FreshFoodDBContext db;

        public ChiTietGioHangDAO()
        {
            db = new FreshFoodDBContext();
        }

        public List<ChiTietGioHang> GetListChiTietGioHang(Guid id)
        {
            return db.ChiTietGioHangs.Where(x => x.IDKhachHang == id).ToList();
        }
    }
}