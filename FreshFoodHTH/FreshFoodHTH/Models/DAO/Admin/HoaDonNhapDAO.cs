using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using FreshFoodHTH.Models.EFplus;

namespace FreshFoodHTH.Models.DAO.Admin
{
    public class HoaDonNhapDAO
    {
        FreshFoodDBContext db;

        public HoaDonNhapDAO()
        {
            db = new FreshFoodDBContext();
        }

        public List<HoaDonNhap> ListHoaDonNhap()
        {
            return db.HoaDonNhaps.ToList();
        }

        public HoaDonNhap getByID(Guid id)
        {
            return db.HoaDonNhaps.Find(id);
        }

        public void Add(HoaDonNhap HoaDonNhap)
        {
            db.HoaDonNhaps.Add(HoaDonNhap);
            db.SaveChanges();
        }

        public void Edit(HoaDonNhap HoaDonNhap)
        {
            HoaDonNhap hoaDonNhap = getByID(HoaDonNhap.IDHoaDonNhap);
            if (hoaDonNhap != null)
            {
                hoaDonNhap.IDHoaDonNhap = HoaDonNhap.IDHoaDonNhap;
                hoaDonNhap.IDNhaCungCap = HoaDonNhap.IDNhaCungCap;
                hoaDonNhap.GhiChu = HoaDonNhap.GhiChu;
                hoaDonNhap.TienHang = HoaDonNhap.TienHang;
                hoaDonNhap.TienShip = HoaDonNhap.TienShip;
                hoaDonNhap.TienGiam = HoaDonNhap.TienGiam;
                hoaDonNhap.TongTien = HoaDonNhap.TongTien;
                hoaDonNhap.CreatedDate = HoaDonNhap.CreatedDate;
                db.SaveChanges();
            }
        }

        public int Delete(Guid id)
        {
            HoaDonNhap HoaDonNhap = db.HoaDonNhaps.Find(id);
            if (HoaDonNhap != null)
            {
                db.HoaDonNhaps.Remove(HoaDonNhap);
                return db.SaveChanges();
            }
            else
            {
                return -1;
            }
        }

        public IEnumerable<flatHoaDonNhap> ListSimple(string searching)
        {
            var list = db.Database.SqlQuery<flatHoaDonNhap>($"SELECT h.IDHoaDonNhap, h.MaSo, ncc.Ten AS TenNhaCungCap, h.TienHang, h.TienShip, h.TienGiam, h.TongTien, h.CreatedDate , h.ModifiedDate " +
                $"FROM dbo.HoaDonNhap h INNER JOIN dbo.NhaCungCap ncc ON h.IDNhaCungCap = ncc.IDNhaCungCap " +
                $"WHERE h.IDHoaDonNhap LIKE N'%{searching}%' " +
                $"OR ncc.Ten LIKE N'%{searching}%' " +
                $"OR h.TienGiam LIKE N'%{searching}%' " +
                $"OR h.TienShip LIKE N'%{searching}%' " +
                $"OR h.TienHang LIKE N'%{searching}%' " +
                $"OR h.TongTien LIKE N'%{searching}%' " +
                $"OR h.CreatedDate LIKE N'%{searching}%' " +
                $"ORDER BY h.CreatedDate DESC").ToList();
            return list;
        }

        public IEnumerable<flatHoaDonNhap> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = db.Database.SqlQuery<flatHoaDonNhap>($"SELECT h.IDHoaDonNhap, h.MaSo , ncc.Ten AS TenNhaCungCap, h.TienHang, h.TienShip, h.TienGiam, h.TongTien, h.CreatedDate , h.ModifiedDate " +
                $"FROM dbo.HoaDonNhap h INNER JOIN dbo.NhaCungCap ncc ON h.IDNhaCungCap = ncc.IDNhaCungCap " +
                $"WHERE h.IDHoaDonNhap LIKE N'%{searching}%' " +
                $"OR ncc.Ten LIKE N'%{searching}%' " +
                $"OR h.TienGiam LIKE N'%{searching}%' " +
                $"OR h.TienShip LIKE N'%{searching}%' " +
                $"OR h.TienHang LIKE N'%{searching}%' " +
                $"OR h.TongTien LIKE N'%{searching}%' " +
                $"OR h.CreatedDate LIKE N'%{searching}%' " +
                $"ORDER BY h.CreatedDate DESC").ToPagedList<flatHoaDonNhap>(PageNum, PageSize);
            return list;
        }

        //public IEnumerable<flatHoaDonNhap> ListAdvanced(string idHoaDonNhap, string customerName, string phone, string address, string discountCode, string discountFrom, string discountTo, string subtotalFrom, string subtotalTo, string totalFrom, string totalTo, string status)
        //{
        //    string querySearch = $"SELECT b.id_HoaDonNhap, c.name AS customerName, b.phone, b.address, b.discountCode, b.discount, b.subtotal, b.total, b.creatDate, bs.status AS statusName " +
        //        $"FROM dbo.HoaDonNhap b, dbo.Customer c, dbo.HoaDonNhapStatus bs " +
        //        $"WHERE b.id_customer = c.id_customer AND b.id_status = bs.id_status";

        //    string queryCondition = "";
        //    if (idHoaDonNhap != "" && idHoaDonNhap != null)
        //    {
        //        queryCondition += $" AND b.id_HoaDonNhap LIKE N'%{idHoaDonNhap}%'";
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

        //    var list = db.Database.SqlQuery<flatHoaDonNhap>(querySearch).ToList();

        //    return list;
        //}

        //public IEnumerable<flatHoaDonNhap> ListAdvancedSearch(int PageNum, int PageSize, string idHoaDonNhap, string customerName, string phone, string address, string discountCode, string discountFrom, string discountTo, string subtotalFrom, string subtotalTo, string totalFrom, string totalTo, string status)
        //{
        //    string querySearch = $"SELECT b.id_HoaDonNhap, c.name AS customerName, b.phone, b.address, b.discountCode, b.discount, b.subtotal, b.total, b.creatDate, bs.status AS statusName " +
        //        $"FROM dbo.HoaDonNhap b, dbo.Customer c, dbo.HoaDonNhapStatus bs " +
        //        $"WHERE b.id_customer = c.id_customer AND b.id_status = bs.id_status";

        //    string queryCondition = "";
        //    if (idHoaDonNhap != "" && idHoaDonNhap != null)
        //    {
        //        queryCondition += $" AND b.id_HoaDonNhap LIKE N'%{idHoaDonNhap}%'";
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

        //    var list = db.Database.SqlQuery<flatHoaDonNhap>(querySearch).ToPagedList<flatHoaDonNhap>(PageNum, PageSize);

        //    return list;
        //}

        //internal object HoaDonNhapDetail(int? id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}