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

        public NhaCungCapSanPham getByID(Guid id)
        {
            return db.NhaCungCapSanPhams.Find(id);
        }

        public void Add(NhaCungCapSanPham NhaCungCapSanPham)
        {
            db.NhaCungCapSanPhams.Add(NhaCungCapSanPham);
            db.SaveChanges();
        }

        public void Edit(NhaCungCapSanPham NhaCungCapSanPham)
        {
            NhaCungCapSanPham nhaCungCapSanPham = getByID(NhaCungCapSanPham.IDNhaCungCap);
            if (nhaCungCapSanPham != null)
            {
                nhaCungCapSanPham.IDNhaCungCapSanPham = NhaCungCapSanPham.IDNhaCungCapSanPham;
                nhaCungCapSanPham.IDNhaCungCap= NhaCungCapSanPham.IDNhaCungCap;
                nhaCungCapSanPham.IDSanPham = NhaCungCapSanPham.IDSanPham;

                nhaCungCapSanPham.DonViTinh = NhaCungCapSanPham.DonViTinh;
                nhaCungCapSanPham.GiaCungUng = NhaCungCapSanPham.GiaCungUng;
                nhaCungCapSanPham.NgayCapNhat = NhaCungCapSanPham.NgayCapNhat;
                db.SaveChanges();
            }
        }

        public int Delete(Guid id)
        {
            NhaCungCapSanPham NhaCungCapSanPham = db.NhaCungCapSanPhams.Find(id);
            if (NhaCungCapSanPham != null)
            {
                db.NhaCungCapSanPhams.Remove(NhaCungCapSanPham);
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

        //public IEnumerable<flatNhaCungCap> ListAdvanced(string idNhaCungCap, string customerName, string phone, string address, string discountCode, string discountFrom, string discountTo, string subtotalFrom, string subtotalTo, string totalFrom, string totalTo, string status)
        //{
        //    string querySearch = $"SELECT b.id_NhaCungCap, c.name AS customerName, b.phone, b.address, b.discountCode, b.discount, b.subtotal, b.total, b.creatDate, bs.status AS statusName " +
        //        $"FROM dbo.NhaCungCapSanPham b, dbo.Customer c, dbo.NhaCungCapStatus bs " +
        //        $"WHERE b.id_customer = c.id_customer AND b.id_status = bs.id_status";

        //    string queryCondition = "";
        //    if (idNhaCungCap != "" && idNhaCungCap != null)
        //    {
        //        queryCondition += $" AND b.id_NhaCungCap LIKE N'%{idNhaCungCap}%'";
        //    }
        //    if (customerName != "" && customerName != null)
        //    {
        //        queryCondition += $" AND c.name LIKE N'%{customerName}%'";
        //    }
        //    if (phone != "" && phone != null)
        //    {
        //        queryCondition += $" AND b.id_category LIKE N'%{phone}%'";
        //    }
        //    if (address != "" && address != null)
        //    {
        //        queryCondition += $" AND b.address LIKE N'%{address}%'";
        //    }
        //    if (discountCode != "" && discountCode != null)
        //    {
        //        queryCondition += $" AND b.discountCode LIKE N'%{discountCode}%'";
        //    }
        //    if (discountFrom != null && discountTo != null && discountFrom != "" && discountTo != "" && Convert.ToDecimal(discountFrom) <= Convert.ToDecimal(discountTo))
        //    {
        //        queryCondition += $" AND p.discount >= {discountFrom} AND p.discount <= {discountTo}";
        //    }
        //    if (subtotalFrom != null && subtotalTo != null && subtotalFrom != "" && subtotalTo != "" && Convert.ToDecimal(subtotalFrom) <= Convert.ToDecimal(subtotalTo))
        //    {
        //        queryCondition += $" AND p.subtotal >= {subtotalFrom} AND p.subtotal <= {subtotalTo}";
        //    }
        //    if (totalFrom != null && totalTo != null && totalFrom != "" && totalTo != "" && Convert.ToDecimal(totalFrom) <= Convert.ToDecimal(totalTo))
        //    {
        //        queryCondition += $" AND p.total >= {totalFrom} AND p.total <= {totalTo}";
        //    }
        //    if (status != "" && status != null)
        //    {
        //        queryCondition += $" AND b.id_status = {status}";
        //    }

        //    if (!queryCondition.Equals(""))
        //    {
        //        querySearch = querySearch + queryCondition;
        //    }

        //    var list = db.Database.SqlQuery<flatNhaCungCap>(querySearch).ToList();

        //    return list;
        //}

        //public IEnumerable<flatNhaCungCap> ListAdvancedSearch(int PageNum, int PageSize, string idNhaCungCap, string customerName, string phone, string address, string discountCode, string discountFrom, string discountTo, string subtotalFrom, string subtotalTo, string totalFrom, string totalTo, string status)
        //{
        //    string querySearch = $"SELECT b.id_NhaCungCap, c.name AS customerName, b.phone, b.address, b.discountCode, b.discount, b.subtotal, b.total, b.creatDate, bs.status AS statusName " +
        //        $"FROM dbo.NhaCungCapSanPham b, dbo.Customer c, dbo.NhaCungCapStatus bs " +
        //        $"WHERE b.id_customer = c.id_customer AND b.id_status = bs.id_status";

        //    string queryCondition = "";
        //    if (idNhaCungCap != "" && idNhaCungCap != null)
        //    {
        //        queryCondition += $" AND b.id_NhaCungCap LIKE N'%{idNhaCungCap}%'";
        //    }
        //    if (customerName != "" && customerName != null)
        //    {
        //        queryCondition += $" AND c.name LIKE N'%{customerName}%'";
        //    }
        //    if (phone != "" && phone != null)
        //    {
        //        queryCondition += $" AND b.id_category LIKE N'%{phone}%'";
        //    }
        //    if (address != "" && address != null)
        //    {
        //        queryCondition += $" AND b.address LIKE N'%{address}%'";
        //    }
        //    if (discountCode != "" && discountCode != null)
        //    {
        //        queryCondition += $" AND b.discountCode LIKE N'%{discountCode}%'";
        //    }
        //    if (discountFrom != null && discountTo != null && discountFrom != "" && discountTo != "" && Convert.ToDecimal(discountFrom) <= Convert.ToDecimal(discountTo))
        //    {
        //        queryCondition += $" AND p.discount >= {discountFrom} AND p.discount <= {discountTo}";
        //    }
        //    if (subtotalFrom != null && subtotalTo != null && subtotalFrom != "" && subtotalTo != "" && Convert.ToDecimal(subtotalFrom) <= Convert.ToDecimal(subtotalTo))
        //    {
        //        queryCondition += $" AND p.subtotal >= {subtotalFrom} AND p.subtotal <= {subtotalTo}";
        //    }
        //    if (totalFrom != null && totalTo != null && totalFrom != "" && totalTo != "" && Convert.ToDecimal(totalFrom) <= Convert.ToDecimal(totalTo))
        //    {
        //        queryCondition += $" AND p.total >= {totalFrom} AND p.total <= {totalTo}";
        //    }
        //    if (status != "" && status != null)
        //    {
        //        queryCondition += $" AND b.id_status = {status}";
        //    }

        //    if (!queryCondition.Equals(""))
        //    {
        //        querySearch = querySearch + queryCondition;
        //    }

        //    var list = db.Database.SqlQuery<flatNhaCungCap>(querySearch).ToPagedList<flatNhaCungCap>(PageNum, PageSize);

        //    return list;
        //}

        //internal object NhaCungCapDetail(int? id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}