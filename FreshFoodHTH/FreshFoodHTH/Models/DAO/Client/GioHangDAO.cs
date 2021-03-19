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
        public ChiTietGioHang GetChiTietGioHangByID(Guid id)
        {
            return db.ChiTietGioHangs.Find(id);
        }
        public List<ChiTietGioHang> GetListChiTietGioHangByIDKhachHang(Guid id)
        {
            return db.ChiTietGioHangs.Where(x => x.IDKhachHang == id).ToList();
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
            NguoiDung nguoidung = db.NguoiDungs.Find(obj.IDKhachHang);
            nguoidung.TongTienGioHang += obj.ThanhTien;
            db.SaveChanges();
        }
        public void Edit(ChiTietGioHang obj)
        {
            ChiTietGioHang ctGioHang = GetChiTietGioHangByID(obj.IDChiTietGioHang);
            if (ctGioHang != null)
            {
                NguoiDung nguoidung = db.NguoiDungs.Find(obj.IDKhachHang);
                nguoidung.TongTienGioHang -= ctGioHang.ThanhTien;
                ctGioHang.SoLuong = obj.SoLuong;
                ctGioHang.ThanhTien = obj.ThanhTien;
                nguoidung.TongTienGioHang += ctGioHang.ThanhTien;
                ctGioHang.DuocChon = obj.DuocChon;
                db.SaveChanges();
            }
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
                cartDetail = db.ChiTietGioHangs.Where(x => x.IDKhachHang == id && x.IDSanPham == item.IDSanPham).SingleOrDefault();
                user.TongTienGioHang -= cartDetail.ThanhTien;
                cartDetail.ThanhTien = item.ThanhTien;
                user.TongTienGioHang += cartDetail.ThanhTien;
            }
            db.SaveChanges();
            return true;
        }
    }

}