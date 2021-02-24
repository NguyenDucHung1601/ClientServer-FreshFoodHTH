using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace FreshFoodHTH.Models.DAO.Admin
{
    public class TheLoaiDAO
    {
        FreshFoodDBContext db = new FreshFoodDBContext();

        public TheLoaiDAO()
        {
            db = new FreshFoodDBContext();
        }

        public List<TheLoai> ListTheLoai()
        {
            return db.TheLoais.ToList();
        }

        public TheLoai GetByID(Guid id)
        {
            return db.TheLoais.Find(id);
        }

        public string GetMaSo(Guid id)
        {
            return db.TheLoais.Find(id).MaSo;
        }

        public void Add(TheLoai obj)
        {
            db.TheLoais.Add(obj);
            db.SaveChanges();
        }

        public void Edit(TheLoai obj)
        {
            TheLoai theloai = GetByID(obj.IDTheLoai);
            if (theloai != null)
            {
                theloai.MaSo = obj.MaSo;
                theloai.Ten = obj.Ten;
                theloai.MoTa = obj.MoTa;

                db.SaveChanges();
            }
        }

        public int Delete(Guid id)
        {
            TheLoai theloai = db.TheLoais.Find(id);
            if (theloai != null)
            {
                db.TheLoais.Remove(theloai);
                return db.SaveChanges();
            }
            else
                return -1;
        }


        public IEnumerable<TheLoai> ListSimple(string searching)
        {
            var list = db.Database.SqlQuery<TheLoai>($"SELECT * FROM dbo.TheLoai tl " +
                $"WHERE tl.Ten LIKE N'%{searching}%' " +
                $"ORDER BY tl.Ten").ToList();

            return list;
        }

        public IEnumerable<TheLoai> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = db.Database.SqlQuery<TheLoai>($"SELECT * FROM dbo.TheLoai tl " +
               $"WHERE tl.Ten LIKE N'%{searching}%' " +
               $"ORDER BY tl.Ten").ToPagedList<TheLoai>(PageNum, PageSize);

            return list;
        }
    }
}