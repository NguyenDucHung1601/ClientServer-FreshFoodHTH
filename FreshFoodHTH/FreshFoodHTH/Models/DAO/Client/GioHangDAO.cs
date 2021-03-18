using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodHTH.Models.DAO.Client
{
    public class GioHangDAO
    {
        FreshFoodDBContext db;

        public GioHangDAO()
        {
            db = new FreshFoodDBContext();
        }
        public bool KTGIOHANG(ChiTietGioHang obj)
        {
            if (obj.SoLuong <= (db.SanPhams.Find(obj.IDSanPham)).SoLuong)
                return true;
            return false;
        }
        public void Add(ChiTietGioHang obj)
        {
            db.ChiTietGioHangs.Add(obj);
            NguoiDung nguoidung = db.NguoiDungs.Where(x => x.IDNguoiDung == obj.IDKhachHang).SingleOrDefault();
            nguoidung.TongTienGioHang += obj.ThanhTien;
            db.SaveChanges();

        }
        public bool CNGioHang(Guid id)
        {
            var listChitietgiohang = db.ChiTietGioHangs.Where(x => x.IDKhachHang == id).ToList();
            NguoiDung user = db.NguoiDungs.Find(id);
            ChiTietGioHang cartDetail = new ChiTietGioHang();
            foreach (var item in listChitietgiohang)
            {
                if (!KTGIOHANG(item))
                    return false;
                cartDetail = db.ChiTietGioHangs.Where(x => x.IDKhachHang==id && x.IDSanPham == item.IDSanPham).SingleOrDefault();
                user.TongTienGioHang -= cartDetail.ThanhTien;
                cartDetail.ThanhTien = item.ThanhTien;
                user.TongTienGioHang += cartDetail.ThanhTien;
            }
            db.SaveChanges();
            return true;
        }
    }
    
}