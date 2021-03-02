using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using FreshFoodHTH.Models.EFplus;

namespace FreshFoodHTH.Models.DAO.Admin
{
    public class NhaCungCapDAO
    {
        FreshFoodDBContext db;

        public NhaCungCapDAO()
        {
            db = new FreshFoodDBContext();
        }

        public List<NhaCungCap> ListNhaCungCap()
        {
            return db.NhaCungCaps.ToList();
        }

        public NhaCungCap getByID(Guid id)
        {
            return db.NhaCungCaps.Find(id);
        }

        public void Add(NhaCungCap NhaCungCap)
        {
            db.NhaCungCaps.Add(NhaCungCap);
            db.SaveChanges();
        }

        public void Edit(NhaCungCap NhaCungCap)
        {
            NhaCungCap nhaCungCap = getByID(NhaCungCap.IDNhaCungCap);
            if (nhaCungCap != null)
            {
                nhaCungCap.Ten = NhaCungCap.Ten;
                nhaCungCap.DiaChi = NhaCungCap.DiaChi;
                nhaCungCap.DienThoai = NhaCungCap.DienThoai;
                nhaCungCap.CreatedDate = NhaCungCap.CreatedDate;
                db.SaveChanges();
            }
        }

        public int Delete(Guid id)
        {
            NhaCungCap NhaCungCap = db.NhaCungCaps.Find(id);
            if (NhaCungCap != null)
            {
                db.NhaCungCaps.Remove(NhaCungCap);
                return db.SaveChanges();
            }
            else
            {
                return -1;
            }
        }

        public IEnumerable<flatNhaCungCap> ListSimple(string searching)
        {
            var list = db.Database.SqlQuery<flatNhaCungCap>($"SELECT ncc.IDNhaCungCap, ncc.Ten AS TenNhaCungCap, ncc.DiaChi, ncc.DienThoai, ncc.CreatedDate " +
                $"FROM dbo.NhaCungCap ncc " +
                $"WHERE ncc.IDNhaCungCap = ncc.IDNhaCungCap " +
                $"AND ncc.IDNhaCungCap LIKE N'%{searching}%' " +
                $"OR ncc.Ten LIKE N'%{searching}%' " +
                $"OR ncc.DienThoai LIKE N'%{searching}%' " +
                $"OR ncc.DiaChi LIKE N'%{searching}%' " +
                $"OR ncc.CreatedDate LIKE N'%{searching}%' " +
                $"ORDER BY ncc.CreatedDate DESC").ToList();
            return list;
        }

        public IEnumerable<flatNhaCungCap> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = db.Database.SqlQuery<flatNhaCungCap>($"SELECT ncc.IDNhaCungCap, ncc.Ten AS TenNhaCungCap, ncc.DiaChi, ncc.DienThoai, ncc.CreatedDate " +
                $"FROM dbo.NhaCungCap ncc " +
                $"WHERE ncc.IDNhaCungCap = ncc.IDNhaCungCap " +
                $"AND ncc.IDNhaCungCap LIKE N'%{searching}%' " +
                $"OR ncc.Ten LIKE N'%{searching}%' " +
                $"OR ncc.DienThoai LIKE N'%{searching}%' " +
                $"OR ncc.DiaChi LIKE N'%{searching}%' " +
                $"OR ncc.CreatedDate LIKE N'%{searching}%' " +
                $"ORDER BY ncc.CreatedDate DESC").ToPagedList<flatNhaCungCap>(PageNum, PageSize);
            return list;
        }    

        //public IEnumerable<flatNhaCungCap> ListAdvanced(string idNhaCungCap, string customerName, string phone, string address, string discountCode, string discountFrom, string discountTo, string subtotalFrom, string subtotalTo, string totalFrom, string totalTo, string status)
        //{
        //    string querySearch = $"SELECT b.id_NhaCungCap, c.name AS customerName, b.phone, b.address, b.discountCode, b.discount, b.subtotal, b.total, b.creatDate, bs.status AS statusName " +
        //        $"FROM dbo.NhaCungCap b, dbo.Customer c, dbo.NhaCungCapStatus bs " +
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
        //        $"FROM dbo.NhaCungCap b, dbo.Customer c, dbo.NhaCungCapStatus bs " +
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