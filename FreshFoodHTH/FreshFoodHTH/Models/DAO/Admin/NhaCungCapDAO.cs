using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;


namespace FreshFoodHTH.Models.DAO.Admin
{
    public class NhaCungCapDAO
    {
        FreshFoodDBContext db = new FreshFoodDBContext();

        public NhaCungCapDAO()
        {
            db = new FreshFoodDBContext();
        }
        public List<NhaCungCap> ListNhaCungCap()
        {
            return db.NhaCungCaps.ToList();
        }

        public NhaCungCap GetByID(Guid id)
        {
            return db.NhaCungCaps.Find(id);
        }
        public void Add(NhaCungCap obj)
        {
            db.NhaCungCaps.Add(obj);
            db.SaveChanges();
        }
        public void Edit(NhaCungCap obj)
        {
            NhaCungCap nhacungcap = GetByID(obj.IDNhaCungCap);
            if (nhacungcap!=null)
            {
                nhacungcap.Ten = obj.Ten;
                nhacungcap.DiaChi = obj.DiaChi;
                nhacungcap.HinhAnh = obj.HinhAnh;
                nhacungcap.DienThoai = obj.DienThoai;
                nhacungcap.CreatedDate = obj.CreatedDate;

                db.SaveChanges();

            }
        }
        public int Delete(Guid id)
        {
            NhaCungCap nhacungcap = db.NhaCungCaps.Find(id);
            if (nhacungcap != null)
            {
                db.NhaCungCaps.Remove(nhacungcap);
                return db.SaveChanges();
            }
            else
                return -1;
        }
        public IEnumerable<NhaCungCap> ListSimple(string searching)
        {
            var list = db.Database.SqlQuery<NhaCungCap>($"SELECT * FROM dbo.NhaCungCap ncc " +
                $"WHERE ncc.Ten LIKE N'%{searching}%' " +
                $"OR ncc.DiaChi LIKE N'%{searching}%' " +
                $"OR ncc.DienThoai LIKE N'%{searching}%' " +
                $"ORDER BY ncc.Ten").ToList();

            return list;
        }

        public IEnumerable<NhaCungCap> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = db.Database.SqlQuery<NhaCungCap>($"SELECT * FROM dbo.NhaCungCap ncc " +
               $"WHERE ncc.Ten LIKE N'%{searching}%' " +
                $"OR ncc.DiaChi LIKE N'%{searching}%' " +
                $"OR ncc.DienThoai LIKE N'%{searching}%' " +
               $"ORDER BY ncc.Ten").ToPagedList<NhaCungCap>(PageNum, PageSize);

            return list;
        }
    }
}