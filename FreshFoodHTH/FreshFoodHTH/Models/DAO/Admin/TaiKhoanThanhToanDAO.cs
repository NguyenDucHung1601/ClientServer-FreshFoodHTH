using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;


namespace FreshFoodHTH.Models.DAO.Admin
{
    public class TaiKhoanThanhToanDAO
    {
        FreshFoodDBContext db = new FreshFoodDBContext();

        public TaiKhoanThanhToanDAO()
        {
            db = new FreshFoodDBContext();
        }
        public List<TaiKhoanThanhToan> ListTaiKhoanThanhToan()
        {
            return db.TaiKhoanThanhToans.ToList();
        }

        public TaiKhoanThanhToan GetByID(Guid id)
        {
            return db.TaiKhoanThanhToans.Find(id);
        }
        public void Add(TaiKhoanThanhToan obj)
        {
            db.TaiKhoanThanhToans.Add(obj);
            db.SaveChanges();
        }
        public void Edit(TaiKhoanThanhToan obj)
        {
            TaiKhoanThanhToan taiKhoanThanhToan = GetByID(obj.IDTaiKhoan);
            if (taiKhoanThanhToan!=null)
            {
                taiKhoanThanhToan.Ten = obj.Ten;
                taiKhoanThanhToan.VietTat = obj.VietTat;
                taiKhoanThanhToan.Logo = obj.Logo;
                taiKhoanThanhToan.LoaiTaiKhoan = obj.LoaiTaiKhoan;

                db.SaveChanges();

            }
        }
        public int Delete(Guid id)
        {
            TaiKhoanThanhToan taiKhoanThanhToan = db.TaiKhoanThanhToans.Find(id);
            if (taiKhoanThanhToan != null)
            {
                db.TaiKhoanThanhToans.Remove(taiKhoanThanhToan);
                return db.SaveChanges();
            }
            else
                return -1;
        }
        public IEnumerable<TaiKhoanThanhToan> ListSimple(string searching)
        {
            var list = db.Database.SqlQuery<TaiKhoanThanhToan>($"SELECT * FROM dbo.TaiKhoanThanhToan tktt " +
                $"WHERE tktt.Ten LIKE N'%{searching}%' " +
                $"OR tktt.VietTat LIKE N'%{searching}%' " +
                $"OR tktt.LoaiTaiKhoan LIKE N'%{searching}%' " +
                $"ORDER BY tktt.Ten").ToList();

            return list;
        }

        public IEnumerable<TaiKhoanThanhToan> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = db.Database.SqlQuery<TaiKhoanThanhToan>($"SELECT * FROM dbo.TaiKhoanThanhToan tktt " +
               $"WHERE tktt.Ten LIKE N'%{searching}%' " +
               $"OR tktt.VietTat LIKE N'%{searching}%' " +
               $"OR tktt.LoaiTaiKhoan LIKE N'%{searching}%' " +
               $"ORDER BY tktt.Ten").ToPagedList<TaiKhoanThanhToan>(PageNum, PageSize);

            return list;
        }
    }
}