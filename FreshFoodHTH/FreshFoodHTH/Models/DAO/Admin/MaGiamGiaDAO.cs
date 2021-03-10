using FreshFoodHTH.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodHTH.Models.DAO.Admin
{
    public class MaGiamGiaDAO
    {
        FreshFoodDBContext db = new FreshFoodDBContext();

        public MaGiamGiaDAO()
        {
            db = new FreshFoodDBContext();
        }

        public List<MaGiamGia> ListMaGiamGia()
        {
            return db.MaGiamGias.ToList();
        }

        public MaGiamGia GetByID(Guid id)
        {
            return db.MaGiamGias.Find(id);
        }

        public string GetMaSo(Guid id)
        {
            return db.MaGiamGias.Find(id).MaGiamGia1;
        }

        public void Add(MaGiamGia obj)
        {
            db.MaGiamGias.Add(obj);
            db.SaveChanges();
        }

        public void Edit(MaGiamGia obj)
        {
            MaGiamGia magiamgia = GetByID(obj.IDMaGiamGia);
            if (magiamgia != null)
            {
                magiamgia.MaGiamGia1 = obj.MaGiamGia1;
                magiamgia.TienGiam = obj.TienGiam;
                magiamgia.DieuKienApDung = obj.DieuKienApDung;
                magiamgia.HanSuDung = obj.HanSuDung;

                db.SaveChanges();
            }
        }

        public int Delete(Guid id)
        {
            MaGiamGia magiamgia = db.MaGiamGias.Find(id);
            if (magiamgia != null)
            {
                db.MaGiamGias.Remove(magiamgia);
                return db.SaveChanges();
            }
            else
                return -1;
        }


        public IEnumerable<MaGiamGia> ListSimple(string searching)
        {
            var list = db.Database.SqlQuery<MaGiamGia>($"SELECT mgg.IDMaGiamGia, mgg.MaGiamGia AS MaGiamGia1, mgg.IDLoaiKhachHang, mgg.TienGiam, mgg.DieuKienApDung, mgg.HanSuDung, " +
                $"mgg.CreatedDate, mgg.CreatedBy, mgg.ModifiedDate, mgg.Modifiedby FROM dbo.MaGiamGia mgg " +
                $"WHERE mgg.MaGiamGia LIKE N'%{searching}%' " +
                $"OR mgg.TienGiam LIKE N'%{searching}%' " +
                $"OR mgg.HanSuDung LIKE N'%{searching}%' " +
                $"ORDER BY mgg.CreatedDate").ToList();

            return list;
        }

        public IEnumerable<MaGiamGia> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = db.Database.SqlQuery<MaGiamGia>($"SELECT mgg.IDMaGiamGia, mgg.MaGiamGia AS MaGiamGia1, mgg.IDLoaiKhachHang, mgg.TienGiam, mgg.DieuKienApDung, mgg.HanSuDung, " +
                $"mgg.CreatedDate, mgg.CreatedBy, mgg.ModifiedDate, mgg.Modifiedby FROM dbo.MaGiamGia mgg " +
                $"WHERE mgg.MaGiamGia LIKE N'%{searching}%' " +
                $"OR mgg.TienGiam LIKE N'%{searching}%' " +
                $"OR mgg.HanSuDung LIKE N'%{searching}%' " +
                $"ORDER BY mgg.CreatedDate").ToPagedList<MaGiamGia>(PageNum, PageSize);

            return list;
        }
    }
}