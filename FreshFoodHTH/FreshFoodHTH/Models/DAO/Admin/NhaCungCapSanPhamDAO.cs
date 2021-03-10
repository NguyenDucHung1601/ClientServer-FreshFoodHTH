using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using FreshFoodHTH.Models.EFplus;

namespace FreshFoodHTH.Models.DAO.Admin
{
    public class NhaCungCapSanPhamDAO
    {
        FreshFoodDBContext db;

        public NhaCungCapSanPhamDAO()
        {
            db = new FreshFoodDBContext();
        }

        public List<NhaCungCapSanPham> ListNhaCungCap()
        {
            return db.NhaCungCapSanPhams.ToList();
        }
        public List<NhaCungCapSanPham> GetListSPCungUngByIDNhaCungCap(Guid idncc)
        {
            return db.NhaCungCapSanPhams.Where(x => x.IDNhaCungCap == idncc).ToList();
        }

        public string SPCungUng_GetDonViTinh(Guid? idncc, Guid idsp)
        {
            return db.NhaCungCapSanPhams
                .Where(x => x.IDNhaCungCap == idncc)
                .Where(x => x.IDSanPham == idsp)
                .Select(x => x.DonViTinh)
                .FirstOrDefault();
        }

        public decimal SPCungUng_GetGiaCungUng(Guid? idncc, Guid idsp)
        {
            return Convert.ToDecimal(db.NhaCungCapSanPhams
                .Where(x => x.IDNhaCungCap == idncc)
                .Where(x => x.IDSanPham == idsp)
                .Select(x => x.GiaCungUng)
                .FirstOrDefault());
        }

        public NhaCungCapSanPham GetByID(Guid id)
        {
            return db.NhaCungCapSanPhams.Find(id);
        }

        public void Add(NhaCungCapSanPham obj)
        {
            db.NhaCungCapSanPhams.Add(obj);
            db.SaveChanges();
        }

        public void Edit(NhaCungCapSanPham obj)
        {
            NhaCungCapSanPham nhacungcapsanpham = GetByID(obj.IDNhaCungCapSanPham);

            if (nhacungcapsanpham != null)
            {
                nhacungcapsanpham.DonViTinh = obj.DonViTinh;
                nhacungcapsanpham.GiaCungUng = obj.GiaCungUng;
                db.SaveChanges();
            }
        }

        public int Delete(Guid id)
        {
            NhaCungCapSanPham nhacungcapsanpham = db.NhaCungCapSanPhams.Find(id);
            if (nhacungcapsanpham != null)
            {
                db.NhaCungCapSanPhams.Remove(nhacungcapsanpham);
                return db.SaveChanges();
            }
            else
            {
                return -1;
            }
        }

        public IEnumerable<flatNhaCungCapSanPham> ListSimple(string searching)
        {
            var list = db.Database.SqlQuery<flatNhaCungCapSanPham>($"SELECT nccsp.IDNhaCungCapSanPham, sp.Ten AS TenSanPham, ncc.Ten AS TenNhaCungCap, nccsp.DonViTinh, nccsp.GiaCungUng, nccsp.NgayCapNhat " +
                $"FROM dbo.NhaCungCapSanPham nccsp INNER JOIN dbo.NhaCungCap ncc ON nccsp.IDNhaCungCap = ncc.IDNhaCungCap INNER JOIN dbo.SanPham sp ON nccsp.IDSanPham = sp.IDSanPham " +
                $"WHERE sp.Ten LIKE N'%%' " +
                $"OR ncc.Ten LIKE N'%%' " +
                $"OR nccsp.DonViTinh LIKE N'%%' " +
                $"OR nccsp.GiaCungUng LIKE N'%%' " +
                $"OR nccsp.NgayCapNhat LIKE N'%%' " +
                $"ORDER BY nccsp.NgayCapNhat DESC").ToList();
            return list;
        }

        public IEnumerable<flatNhaCungCapSanPham> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = db.Database.SqlQuery<flatNhaCungCapSanPham>($"SELECT nccsp.IDNhaCungCapSanPham, sp.Ten AS TenSanPham, ncc.Ten AS TenNhaCungCap, nccsp.DonViTinh, nccsp.GiaCungUng, nccsp.NgayCapNhat " +
            $"FROM dbo.NhaCungCapSanPham nccsp INNER JOIN dbo.NhaCungCap ncc ON nccsp.IDNhaCungCap = ncc.IDNhaCungCap INNER JOIN dbo.SanPham sp ON nccsp.IDSanPham = sp.IDSanPham " +
            $"WHERE sp.Ten LIKE N'%%' " +
            $"OR ncc.Ten LIKE N'%%' " +
            $"OR nccsp.DonViTinh LIKE N'%%' " +
            $"OR nccsp.GiaCungUng LIKE N'%%' " +
            $"OR nccsp.NgayCapNhat LIKE N'%%' " +
            $"ORDER BY nccsp.NgayCapNhat DESC").ToPagedList<flatNhaCungCapSanPham>(PageNum, PageSize);

            return list;
        }
    }
}