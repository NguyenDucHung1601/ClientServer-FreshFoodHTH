using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using FreshFoodHTH.Models.EFplus;

namespace FreshFoodHTH.Models.DAO.Admin
{
    public class HoaDonNhapDAO
    {
        FreshFoodDBContext db;

        public HoaDonNhapDAO()
        {
            db = new FreshFoodDBContext();
        }

        public List<HoaDonNhap> ListHoaDonNhap()
        {
            return db.HoaDonNhaps.ToList();
        }

        public HoaDonNhap GetByID(Guid id)
        {
            return db.HoaDonNhaps.Find(id);
        }

        public void Add(HoaDonNhap obj)
        {
            db.HoaDonNhaps.Add(obj);
            db.SaveChanges();
        }

        public void Edit(HoaDonNhap obj)
        {
            HoaDonNhap hoaDonNhap = GetByID(obj.IDHoaDonNhap);
            if (hoaDonNhap != null)
            {
                hoaDonNhap.TienHang = obj.TienHang;
                hoaDonNhap.TienShip = obj.TienShip;
                hoaDonNhap.TienGiam = obj.TienGiam;
                hoaDonNhap.TongTien = obj.TongTien;
                hoaDonNhap.GhiChu = obj.GhiChu;
                db.SaveChanges();
            }
        }

        public void Update(HoaDonNhap obj)
        {
            HoaDonNhap hoaDonNhap = GetByID(obj.IDHoaDonNhap);
            if (hoaDonNhap != null)
            {
                hoaDonNhap.TienHang = obj.TienHang;
                hoaDonNhap.TienShip = obj.TienShip;
                hoaDonNhap.TienGiam = obj.TienGiam;
                hoaDonNhap.TongTien = obj.TongTien;
                hoaDonNhap.GhiChu = obj.GhiChu;
                db.SaveChanges();
            }
        }

        public int Delete(Guid id)
        {
            HoaDonNhap hoadonnhap = db.HoaDonNhaps.Find(id);
            if (hoadonnhap != null)
            {
                db.HoaDonNhaps.Remove(hoadonnhap);
                return db.SaveChanges();
            }
            else
                return -1;
        }

        public IEnumerable<flatHoaDonNhap> ListSimple(string searching)
        {
            var list = db.Database.SqlQuery<flatHoaDonNhap>($"SELECT h.IDHoaDonNhap, h.MaSo, ncc.Ten AS TenNhaCungCap, h.TongTien, h.CreatedDate " +
                $"FROM dbo.HoaDonNhap h INNER JOIN dbo.NhaCungCap ncc ON h.IDNhaCungCap = ncc.IDNhaCungCap " +
                $"WHERE h.IDHoaDonNhap LIKE N'%{searching}%' " +
                $"OR ncc.Ten LIKE N'%{searching}%' " +
                $"OR h.TongTien LIKE N'%{searching}%' " +
                $"OR h.CreatedDate LIKE N'%{searching}%' " +
                $"ORDER BY h.CreatedDate DESC").ToList();
            return list;
        }

        public IEnumerable<flatHoaDonNhap> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = db.Database.SqlQuery<flatHoaDonNhap>($"SELECT h.IDHoaDonNhap, h.MaSo, ncc.Ten AS TenNhaCungCap, h.TongTien, h.CreatedDate " +
                $"FROM dbo.HoaDonNhap h INNER JOIN dbo.NhaCungCap ncc ON h.IDNhaCungCap = ncc.IDNhaCungCap " +
                $"WHERE h.IDHoaDonNhap LIKE N'%{searching}%' " +
                $"OR ncc.Ten LIKE N'%{searching}%' " +
                $"OR h.TongTien LIKE N'%{searching}%' " +
                $"OR h.CreatedDate LIKE N'%{searching}%' " +
                $"ORDER BY h.CreatedDate DESC").ToPagedList<flatHoaDonNhap>(PageNum, PageSize);
            return list;
        }

        public int TongHoaDonNhap()
        {
            var count = db.Database.SqlQuery<flatHoaDonNhap>($"SELECT * FROM dbo.HoaDonNhap hdn").ToList().Count;
            return count;
        }
    }
}