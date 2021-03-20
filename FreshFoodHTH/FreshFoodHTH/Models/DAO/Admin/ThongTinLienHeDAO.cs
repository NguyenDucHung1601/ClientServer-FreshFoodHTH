using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using FreshFoodHTH.Models.EFplus;

namespace FreshFoodHTH.Models.DAO.Admin
{
    public class ThongTinLienHeDAO
    {
        FreshFoodDBContext db;

        public ThongTinLienHeDAO()
        {
            db = new FreshFoodDBContext();
        }

        public ThongTinLienHe GetInfoObj()
        {
            return db.ThongTinLienHes.ToList().ElementAt(0);
        }

        public Guid GetInfoID()
        {
            return GetInfoObj().ID;
        }

        public void Edit(ThongTinLienHe obj)
        {
            ThongTinLienHe info = GetInfoObj();
            info.ID = obj.ID;
            info.TenCuaHang = obj.TenCuaHang;
            info.DiaChi = obj.DiaChi;
            info.DienThoai1 = obj.DienThoai1;
            info.DienThoai2 = obj.DienThoai2;
            info.GioMoCua = obj.GioMoCua;
            info.Email = obj.Email;
            info.LinkFacebook = obj.LinkFacebook;
            info.LinkYoutube = obj.LinkYoutube;
            info.LinkInstagram = obj.LinkInstagram;

            db.SaveChanges();
        }
    }
}