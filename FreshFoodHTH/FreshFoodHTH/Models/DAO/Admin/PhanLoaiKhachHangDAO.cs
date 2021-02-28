using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace FreshFoodHTH.Models.DAO.Admin
{
    public class PhanLoaiKhachHangDAO
    {
        FreshFoodDBContext db = new FreshFoodDBContext();

        public PhanLoaiKhachHangDAO()
        {
            db = new FreshFoodDBContext();
        }

        public List<PhanLoaiKhachHang> ListPhanLoaiKhachHang()
        {
            return db.PhanLoaiKhachHangs.ToList();
        }

        public PhanLoaiKhachHang GetByID(Guid id)
        {
            return db.PhanLoaiKhachHangs.Find(id);
        }
        public void Add(PhanLoaiKhachHang obj)
        {
            db.PhanLoaiKhachHangs.Add(obj);
            db.SaveChanges();
        }

        public void Edit(PhanLoaiKhachHang obj)
        {
            PhanLoaiKhachHang phanloaikhachhang = GetByID(obj.IDLoaiKhachHang);
            if (phanloaikhachhang != null)
            {
                phanloaikhachhang.Ten = obj.Ten;
                phanloaikhachhang.DieuKien = obj.DieuKien;
                db.SaveChanges();
            }
        }

        public int Delete(Guid id)
        {
            PhanLoaiKhachHang phanloaikhachhang = db.PhanLoaiKhachHangs.Find(id);
            if (phanloaikhachhang != null)
            {
                db.PhanLoaiKhachHangs.Remove(phanloaikhachhang);
                return db.SaveChanges();
            }
            else
                return -1;
        }


        public IEnumerable<PhanLoaiKhachHang> ListSimple(string searching)
        {
            var list = db.Database.SqlQuery<PhanLoaiKhachHang>($"SELECT * FROM dbo.PhanLoaiKhachHang plkh " +
                $"WHERE plkh.Ten LIKE N'%{searching}%' " +

                $"ORDER BY plkh.Ten").ToList();

            return list;
        }

        public IEnumerable<PhanLoaiKhachHang> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = db.Database.SqlQuery<PhanLoaiKhachHang>($"SELECT * FROM dbo.PhanLoaiKhachHang plkh " +
               $"WHERE plkh.Ten LIKE N'%{searching}%' " +
               $"ORDER BY plkh.Ten").ToPagedList<PhanLoaiKhachHang>(PageNum, PageSize);

            return list;
        }
    }
}