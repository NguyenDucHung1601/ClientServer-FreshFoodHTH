using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace FreshFoodHTH.Models.DAO.Admin
{
    public class LoaiNguoiDungDAO
    {
        FreshFoodDBContext db = new FreshFoodDBContext();

        public LoaiNguoiDungDAO()
        {
            db = new FreshFoodDBContext();
        }

        public List<LoaiNguoiDung> ListLoaiNguoiDung()
        {
            return db.LoaiNguoiDungs.ToList();
        }

        public LoaiNguoiDung GetByID(Guid id)
        {
            return db.LoaiNguoiDungs.Find(id);
        }
        public void Add(LoaiNguoiDung obj)
        {
            db.LoaiNguoiDungs.Add(obj);
            db.SaveChanges();
        }

        public void Edit(LoaiNguoiDung obj)
        {
            LoaiNguoiDung loainguoidung = GetByID(obj.IDLoaiNguoiDung);
            if (loainguoidung != null)
            {
                loainguoidung.Ten= obj.Ten;
                db.SaveChanges();
            }
        }

        public int Delete(Guid id)
        {
            LoaiNguoiDung loainguoidung = db.LoaiNguoiDungs.Find(id);
            if (loainguoidung != null)
            {
                db.LoaiNguoiDungs.Remove(loainguoidung);
                return db.SaveChanges();
            }
            else
                return -1;
        }


        public IEnumerable<LoaiNguoiDung> ListSimple(string searching)
        {
            var list = db.Database.SqlQuery<LoaiNguoiDung>($"SELECT * FROM dbo.LoaiNguoiDung lnd " +
                $"WHERE lnd.Ten LIKE N'%{searching}%' " +
                $"ORDER BY lnd.Ten").ToList();

            return list;
        }

        public IEnumerable<LoaiNguoiDung> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = db.Database.SqlQuery<LoaiNguoiDung>($"SELECT * FROM dbo.LoaiNguoiDung lnd " +
               $"WHERE lnd.Ten LIKE N'%{searching}%' " +
               $"ORDER BY lnd.Ten").ToPagedList<LoaiNguoiDung>(PageNum, PageSize);

            return list;
        }
    }
}