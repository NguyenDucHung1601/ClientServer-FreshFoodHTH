using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using FreshFoodHTH.Models.EFplus;

namespace FreshFoodHTH.Models.DAO.Admin
{
    public class ChiTietDonHangDAO
    {
        FreshFoodDBContext db;

        public ChiTietDonHangDAO()
        {
            db = new FreshFoodDBContext();
        }

        public List<ChiTietDonHang> GetListChiTietDonHang(Guid id)
        {
            return db.ChiTietDonHangs.Where(x => x.IDDonHang == id).ToList();
        }
    }
}