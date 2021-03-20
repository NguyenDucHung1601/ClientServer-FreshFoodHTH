using FreshFoodHTH.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodHTH.Models.DAO.Admin
{
    public class MaGiamGiaKhachHangDAO
    {
        FreshFoodDBContext db = new FreshFoodDBContext();

        public MaGiamGiaKhachHangDAO()
        {
            db = new FreshFoodDBContext();
        }

        public List<MaGiamGiaKhachHang> ListTheLoai()
        {
            return db.MaGiamGiaKhachHangs.ToList();
        }

        public MaGiamGiaKhachHang GetByID(Guid idmgg, Guid idkh)
        {
            return db.MaGiamGiaKhachHangs.Where(x => x.IDMaGiamGia == idmgg).Where(x => x.IDKhacHang == idkh).ToList().ElementAt(0);
        }

        public void Add(MaGiamGiaKhachHang obj)
        {
            db.MaGiamGiaKhachHangs.Add(obj);
            db.SaveChanges();
        }

        public int Delete(Guid idmgg, Guid idkh)
        {
            MaGiamGiaKhachHang obj = db.MaGiamGiaKhachHangs.Where(x => x.IDMaGiamGia == idmgg).Where(x => x.IDKhacHang == idkh).ToList().ElementAt(0);
            if (obj != null)
            {
                db.MaGiamGiaKhachHangs.Remove(obj);
                return db.SaveChanges();
            }
            else
                return -1;
        }

    }
}