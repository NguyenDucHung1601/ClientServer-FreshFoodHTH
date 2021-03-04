using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace FreshFoodHTH.Models.DAO.Admin
{
    public class PhuongThucThanhToanDAO
    {
        FreshFoodDBContext db = new FreshFoodDBContext();

        public PhuongThucThanhToanDAO()
        {
            db = new FreshFoodDBContext();
        }

        public List<PhuongThucThanhToan> ListPhuongThucThanhToan()
        {
            return db.PhuongThucThanhToans.ToList();
        }

        public PhuongThucThanhToan GetByID(Guid id)
        {
            return db.PhuongThucThanhToans.Find(id);
        }
        public void Add(PhuongThucThanhToan obj)
        {
            db.PhuongThucThanhToans.Add(obj);
            db.SaveChanges();
        }

        public void Edit(PhuongThucThanhToan obj)
        {
            PhuongThucThanhToan phuongthucthanhtoan = GetByID(obj.IDPhuongThucThanhToan);
            if (phuongthucthanhtoan != null)
            {
                phuongthucthanhtoan.TenPhuongThucThanhToan = obj.TenPhuongThucThanhToan;
                db.SaveChanges();
            }
        }

        public int Delete(Guid id)
        {
            PhuongThucThanhToan phuongthucthanhtoan = db.PhuongThucThanhToans.Find(id);
            if (phuongthucthanhtoan != null)
            {
                db.PhuongThucThanhToans.Remove(phuongthucthanhtoan);
                return db.SaveChanges();
            }
            else
                return -1;
        }


        public IEnumerable<PhuongThucThanhToan> ListSimple(string searching)
        {
            var list = db.Database.SqlQuery<PhuongThucThanhToan>($"SELECT * FROM dbo.PhuongThucThanhToan pttt " +
                $"WHERE pttt.TenPhuongThucThanhToan LIKE N'%{searching}%' " +
                $"ORDER BY pttt.TenPhuongThucThanhToan").ToList();

            return list;
        }

        public IEnumerable<PhuongThucThanhToan> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = db.Database.SqlQuery<PhuongThucThanhToan>($"SELECT * FROM dbo.PhuongThucThanhToan pttt " +
               $"WHERE pttt.TenPhuongThucThanhToan LIKE N'%{searching}%' " +
               $"ORDER BY pttt.TenPhuongThucThanhToan").ToPagedList<PhuongThucThanhToan>(PageNum, PageSize);

            return list;
        }
    }
}