using FreshFoodHTH.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            return db.PhanLoaiKhachHangs.OrderBy(x => x.CapDo).ToList();
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
            PhanLoaiKhachHang phanloaiKH = GetByID(obj.IDLoaiKhachHang);
            if (phanloaiKH != null)
            {
                phanloaiKH.CapDo = obj.CapDo;
                phanloaiKH.Ten = obj.Ten;
                phanloaiKH.SoDonHangToiThieu = obj.SoDonHangToiThieu;
                phanloaiKH.TongTienHangToiThieu = obj.TongTienHangToiThieu;
                phanloaiKH.DieuKien = obj.DieuKien;

                db.SaveChanges();
            }
        }

        public int Delete(Guid id)
        {
            PhanLoaiKhachHang phanloaiKH = db.PhanLoaiKhachHangs.Find(id);
            if (phanloaiKH != null)
            {
                db.PhanLoaiKhachHangs.Remove(phanloaiKH);
                return db.SaveChanges();
            }
            else
                return -1;
        }


        public IEnumerable<PhanLoaiKhachHang> ListSimple(string searching)
        {
            var list = db.Database.SqlQuery<PhanLoaiKhachHang>($"SELECT * FROM dbo.PhanLoaiKhachHang plkh " +
                $"WHERE plkh.CapDo LIKE N'%{searching}%' " +
                $"OR plkh.Ten LIKE N'%{searching}%' " +
                $"ORDER BY plkh.CapDo").ToList();

            return list;
        }

        public IEnumerable<PhanLoaiKhachHang> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = db.Database.SqlQuery<PhanLoaiKhachHang>($"SELECT * FROM dbo.PhanLoaiKhachHang plkh " +
                $"WHERE plkh.CapDo LIKE N'%{searching}%' " +
                $"OR plkh.Ten LIKE N'%{searching}%' " +
                $"ORDER BY plkh.CapDo").ToPagedList<PhanLoaiKhachHang>(PageNum, PageSize);

            return list;
        }
    }
}