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

        public List<ThongTinLienHe> ListThongTinLienHe()
        {
            return db.ThongTinLienHes.ToList();
        }
        public ThongTinLienHe GetByID(Guid id)
        {
            return db.ThongTinLienHes.Find(id);
        }
        public string GetIdentity()
        {
            int idOrder = Convert.ToInt32(db.Database.SqlQuery<decimal>("SELECT IDENT_CURRENT('SanPham')").FirstOrDefault());
            return idOrder.ToString("D4");
        }
        public void Add(ThongTinLienHe obj)
        {
            db.ThongTinLienHes.Add(obj);
            db.SaveChanges();
        }
        public void Edit(ThongTinLienHe obj)
        {
            ThongTinLienHe thongtinlienhe = GetByID(obj.ID);
            if (thongtinlienhe != null)
            {
                thongtinlienhe.ID = obj.ID;
                thongtinlienhe.TenCuaHang = obj.TenCuaHang;
                thongtinlienhe.DiaChi = obj.DiaChi;
                thongtinlienhe.DienThoai1 = obj.DienThoai1;
                thongtinlienhe.DienThoai2 = obj.DienThoai2;
                thongtinlienhe.GioMoCua = obj.GioMoCua;
                thongtinlienhe.Email = obj.Email;
                thongtinlienhe.LinkFacebook = obj.LinkFacebook;
                thongtinlienhe.LinkYoutube = obj.LinkYoutube;
                thongtinlienhe.LinkInstagram = obj.LinkInstagram;
                thongtinlienhe.CreatedDate = obj.CreatedDate;
                thongtinlienhe.CreatedBy = obj.CreatedBy;
                thongtinlienhe.ModifiedDate = obj.ModifiedDate;
                thongtinlienhe.ModifiedBy = obj.ModifiedBy;

                db.SaveChanges();
            }

        }
        public int Delete(Guid id)
        {
            ThongTinLienHe thongtinlienhe = db.ThongTinLienHes.Find(id);
            if (thongtinlienhe != null)
            {
                db.ThongTinLienHes.Remove(thongtinlienhe);
                return db.SaveChanges();
            }
            else
                return -1;
        }
        public IEnumerable<ThongTinLienHe> ListSimple(string searching)
        {
            var list = db.Database.SqlQuery<ThongTinLienHe>($"SELECT * FROM dbo.ThongTinLienHe ttlh " +
                $"WHERE ttlh.TenCuaHang LIKE N'%{searching}%' " +
                $"OR ttlh.DiaChi LIKE N'%{searching}%' " +
                $"OR ttlh.DienThoai1 LIKE N'%{searching}%' " +
                $"OR ttlh.DienThoai2 LIKE N'%{searching}%' " +
                $"OR ttlh.Email LIKE N'%{searching}%' " +
                $"OR ttlh.CreatedDate LIKE N'%{searching}%' " +
                $"ORDER BY ttlh.CreatedDate").ToList();

            return list;
        }

        public IEnumerable<ThongTinLienHe> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = db.Database.SqlQuery<ThongTinLienHe>($"SELECT * FROM dbo.ThongTinLienHe ttlh " +
               $"WHERE ttlh.TenCuaHang LIKE N'%{searching}%' " +
               $"OR ttlh.DiaChi LIKE N'%{searching}%' " +
               $"OR ttlh.DienThoai1 LIKE N'%{searching}%' " +
               $"OR ttlh.DienThoai2 LIKE N'%{searching}%' " +
               $"OR ttlh.Email LIKE N'%{searching}%' " +
               $"OR ttlh.CreatedDate LIKE N'%{searching}%' " +
               $"ORDER BY ttlh.CreatedDate").ToPagedList<ThongTinLienHe>(PageNum, PageSize);

            return list;
        }
    }
}