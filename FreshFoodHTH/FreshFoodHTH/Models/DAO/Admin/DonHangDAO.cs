using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using FreshFoodHTH.Models.EFplus;

namespace FreshFoodHTH.Models.DAO.Admin
{
    public class DonHangDAO
    {
        FreshFoodDBContext db;

        public DonHangDAO()
        {
            db = new FreshFoodDBContext();
        }

        public List<DonHang> ListDonHang()
        {
            return db.DonHangs.ToList();
        }

        public DonHang getByID(Guid id)
        {
            return db.DonHangs.Find(id);
        }

        public void Add(DonHang DonHang)
        {
            db.DonHangs.Add(DonHang);
            db.SaveChanges();
        }

        public void Edit(DonHang DonHang)
        {
            DonHang donHang = getByID(DonHang.IDDonHang);
            if (donHang != null)
            {
                donHang.IDDonHang = DonHang.IDDonHang;
                donHang.MaSo = DonHang.MaSo;
                donHang.IDKhachHang = DonHang.IDKhachHang;
                donHang.GhiChu = DonHang.GhiChu;
                donHang.TienHang = DonHang.TienHang;
                donHang.TienShip = DonHang.TienShip;
                donHang.TienGiam = DonHang.TienGiam;
                donHang.TongTien = DonHang.TongTien;
                donHang.IDTrangThai = DonHang.IDTrangThai;
                donHang.IDPhuongThucThanhToan = DonHang.IDPhuongThucThanhToan;
                donHang.CreatedDate = DonHang.CreatedDate;
                db.SaveChanges();
            }
        }

        public int Delete(Guid id)
        {
            DonHang DonHang = db.DonHangs.Find(id);
            if (DonHang != null)
            {
                db.DonHangs.Remove(DonHang);
                return db.SaveChanges();
            }
            else
            {
                return -1;
            }
        }

        public IEnumerable<flatDonHang> ListSimple(string searching)
        {
            var list = db.Database.SqlQuery<flatDonHang>($"SELECT b.IDDonHang, b.MaSo, c.Ten AS TenKhachHang, bpttt.TenPhuongThucThanhToan AS TenPhuongThucThanhToan , b.TienHang, b.TienShip, b.TienGiam, b.TongTien, b.CreatedDate , b.ModifiedDate, b.GhiChu,bs.TenTrangThai AS TenTrangThai " +
                $"FROM dbo.DonHang b INNER JOIN dbo.NguoiDung c ON b.IDKhachHang = c.IDNguoiDung INNER JOIN dbo.TrangThai bs ON " +
                $"b.IDTrangThai = bs.IDTrangThai INNER JOIN dbo.PhuongThucThanhToan bpttt ON b.IDPhuongThucThanhToan = bpttt.IDPhuongThucThanhToan " +
                $"WHERE b.IDDonHang LIKE N'%{searching}%' " +
                $"OR c.Ten LIKE N'%{searching}%' " +
                $"OR b.TienGiam LIKE N'%{searching}%' " +
                $"OR b.TienShip LIKE N'%{searching}%' " +
                $"OR b.TienHang LIKE N'%{searching}%' " +
                $"OR b.TongTien LIKE N'%{searching}%' " +
                $"OR b.CreatedDate LIKE N'%{searching}%' " +
                $"OR bpttt.TenPhuongThucThanhToan LIKE N'%{searching}%' " +
                $"OR bs.TenTrangThai LIKE N'%{searching}%' " +
                $"ORDER BY b.CreatedDate DESC").ToList();
            return list;
        }

        public IEnumerable<flatDonHang> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = db.Database.SqlQuery<flatDonHang>($"SELECT b.IDDonHang,b.MaSo, c.Ten AS TenKhachHang, bpttt.TenPhuongThucThanhToan AS TenPhuongThucThanhToan , b.TienHang, b.TienShip, b.TienGiam, b.TongTien, b.CreatedDate, b.ModifiedDate ,b.GhiChu ,bs.TenTrangThai AS TenTrangThai " +
                 $"FROM dbo.DonHang b INNER JOIN dbo.NguoiDung c ON b.IDKhachHang = c.IDNguoiDung INNER JOIN dbo.TrangThai bs ON " +
                 $"b.IDTrangThai = bs.IDTrangThai INNER JOIN dbo.PhuongThucThanhToan bpttt ON b.IDPhuongThucThanhToan = bpttt.IDPhuongThucThanhToan " +
                 $"WHERE b.IDDonHang LIKE N'%{searching}%' " +
                 $"OR c.Ten LIKE N'%{searching}%' " +
                 $"OR b.TienGiam LIKE N'%{searching}%' " +
                 $"OR b.TienShip LIKE N'%{searching}%' " +
                 $"OR b.TienHang LIKE N'%{searching}%' " +
                 $"OR b.TongTien LIKE N'%{searching}%' " +
                 $"OR b.CreatedDate LIKE N'%{searching}%' " +
                 $"OR bpttt.TenPhuongThucThanhToan LIKE N'%{searching}%' " +
                 $"OR bs.TenTrangThai LIKE N'%{searching}%' " +
                 $"ORDER BY b.CreatedDate DESC").ToPagedList<flatDonHang>(PageNum, PageSize);
            return list;
        }

        public int TongDonHang()
        {
            var count = db.Database.SqlQuery<flatDonHang>($"SELECT * FROM dbo.DonHang dh").ToList().Count;

            return count;
        }

        public decimal DoanhThu()
        {
            var SumMoney = db.Database.SqlQuery<flatDonHang>($"SELECT * FROM dbo.DonHang dh").ToList().Sum(x => x.TongTien);

            return SumMoney;
        }

        public void AdminXacNhanDonHang(DonHang dh)
        {   // cập nhật trạng thái => đang giao hàng
            var donHang = getByID(dh.IDDonHang);
            donHang.IDTrangThai = new Guid("3240c2e6-fc6c-4feb-9313-382bd05cf522");
            db.SaveChanges();
        }

    }
}