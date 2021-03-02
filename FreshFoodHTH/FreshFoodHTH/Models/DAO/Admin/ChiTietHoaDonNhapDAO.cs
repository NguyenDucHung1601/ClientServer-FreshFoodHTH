using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using FreshFoodHTH.Models.EFplus;

namespace FreshFoodHTH.Models.DAO.Admin
{
    public class ChiTietHoaDonNhapDAO
    {
        FreshFoodDBContext db;

        public ChiTietHoaDonNhapDAO()
        {
            db = new FreshFoodDBContext();
        }

        public List<ChiTietHoaDonNhap> GetListChiTietHoaDonNhap(Guid id)
        {
            return db.ChiTietHoaDonNhaps.Where(x => x.IDHoaDonNhap == id).ToList();
        }
    }
}