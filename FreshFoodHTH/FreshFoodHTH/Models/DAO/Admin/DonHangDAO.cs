using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using FreshFoodHTH.Models.EFplus;

namespace FreshFoodHTH.Models.DAO.Admin
{
    public class DonHangDAO
    {
        FreshFoodDBContext db;

        public DonHangDAO()
        {
            db = new FreshFoodDBContext();
        }

        public List<DonHang> ListDonHang()
        {
            return db.DonHangs.ToList();
        }

        public DonHang getByID(Guid id)
        {
            return db.DonHangs.Find(id);
        }

        public void Add(DonHang DonHang)
        {
            db.DonHangs.Add(DonHang);
            db.SaveChanges();
        }

        public void Edit(DonHang DonHang)
        {
            DonHang donHang = getByID(DonHang.IDDonHang);
            if (donHang != null)
            {
                donHang.IDKhachHang = DonHang.IDKhachHang;
                donHang.GhiChu = DonHang.GhiChu;
                donHang.TienHang = DonHang.TienHang;
                donHang.TienShip = DonHang.TienShip;
                donHang.TienGiam = DonHang.TienGiam;
                donHang.TongTien = DonHang.TongTien;
                donHang.IDTrangThai = DonHang.IDTrangThai;
                donHang.IDPhuongThucThanhToan = DonHang.IDPhuongThucThanhToan;
                donHang.CreatedDate = DonHang.CreatedDate;
                db.SaveChanges();
            }
        }

        public int Delete(Guid id)
        {
            DonHang DonHang = db.DonHangs.Find(id);
            if (DonHang != null)
            {
                db.DonHangs.Remove(DonHang);
                return db.SaveChanges();
            }
            else
            {
                return -1;
            }
        }

        public IEnumerable<flatDonHang> ListSimple(string searching)
        {
            var list = db.Database.SqlQuery<flatDonHang>($"SELECT b.IDDonHang, c.Ten AS TenKhachHang, bpttt.TenPhuongThucThanhToan AS TenPhuongThucThanhToan , b.TienHang, b.TienShip, b.TienGiam, b.TongTien, b.CreatedDate, bs.TenTrangThai AS TenTrangThai " +
                $"FROM dbo.DonHang b, dbo.NguoiDung c, dbo.TrangThai bs, dbo.PhuongThucThanhToan bpttt " +
                $"WHERE b.IDKhachHang = c.IDNguoiDung AND b.IDTrangThai = bs.IDTrangThai AND b.IDPhuongThucThanhToan = bpttt.IDPhuongThucThanhToan " +
                $"AND b.IDDonHang LIKE N'%{searching}%' " +
                $"OR c.Ten LIKE N'%{searching}%' " +
                $"OR b.TienGiam LIKE N'%{searching}%' " +
                $"OR b.TienShip LIKE N'%{searching}%' " +
                $"OR b.TienHang LIKE N'%{searching}%' " +
                $"OR b.TongTien LIKE N'%{searching}%' " +
                $"OR b.CreatedDate LIKE N'%{searching}%' " +
                $"OR bpttt.TenPhuongThucThanhToan LIKE N'%{searching}%' " +
                $"OR bs.TenTrangThai LIKE N'%{searching}%' " +
                $"ORDER BY b.CreatedDate DESC").ToList();
            return list;
        }

        public IEnumerable<flatDonHang> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = db.Database.SqlQuery<flatDonHang>($"SELECT b.IDDonHang, c.Ten AS TenKhachHang, bpttt.TenPhuongThucThanhToan AS TenPhuongThucThanhToan , b.TienHang, b.TienShip, b.TienGiam, b.TongTien, b.CreatedDate, bs.TenTrangThai AS TenTrangThai " +
                $"FROM dbo.DonHang b, dbo.NguoiDung c, dbo.TrangThai bs, dbo.PhuongThucThanhToan bpttt " +
                $"WHERE b.IDKhachHang = c.IDNguoiDung AND b.IDTrangThai = bs.IDTrangThai AND b.IDPhuongThucThanhToan = bpttt.IDPhuongThucThanhToan " +
                $"AND b.IDDonHang LIKE N'%{searching}%' " +
                $"OR c.Ten LIKE N'%{searching}%' " +
                $"OR b.TienGiam LIKE N'%{searching}%' " +
                $"OR b.TienShip LIKE N'%{searching}%' " +
                $"OR b.TienHang LIKE N'%{searching}%' " +
                $"OR b.TongTien LIKE N'%{searching}%' " +
                $"OR b.CreatedDate LIKE N'%{searching}%' " +
                $"OR bpttt.TenPhuongThucThanhToan LIKE N'%{searching}%' " +
                $"OR bs.TenTrangThai LIKE N'%{searching}%' " +
                $"ORDER BY b.CreatedDate DESC").ToPagedList<flatDonHang>(PageNum,PageSize);
            return list;
        }    

        //public IEnumerable<flatDonHang> ListAdvanced(string idDonHang, string customerName, string phone, string address, string discountCode, string discountFrom, string discountTo, string subtotalFrom, string subtotalTo, string totalFrom, string totalTo, string status)
        //{
        //    string querySearch = $"SELECT b.id_DonHang, c.name AS customerName, b.phone, b.address, b.discountCode, b.discount, b.subtotal, b.total, b.creatDate, bs.status AS statusName " +
        //        $"FROM dbo.DonHang b, dbo.Customer c, dbo.DonHangStatus bs " +
        //        $"WHERE b.id_customer = c.id_customer AND b.id_status = bs.id_status";

        //    string queryCondition = "";
        //    if (idDonHang != "" && idDonHang != null)
        //    {
        //        queryCondition += $" AND b.id_DonHang LIKE N'%{idDonHang}%'";
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

        //    var list = db.Database.SqlQuery<flatDonHang>(querySearch).ToList();

        //    return list;
        //}

        //public IEnumerable<flatDonHang> ListAdvancedSearch(int PageNum, int PageSize, string idDonHang, string customerName, string phone, string address, string discountCode, string discountFrom, string discountTo, string subtotalFrom, string subtotalTo, string totalFrom, string totalTo, string status)
        //{
        //    string querySearch = $"SELECT b.id_DonHang, c.name AS customerName, b.phone, b.address, b.discountCode, b.discount, b.subtotal, b.total, b.creatDate, bs.status AS statusName " +
        //        $"FROM dbo.DonHang b, dbo.Customer c, dbo.DonHangStatus bs " +
        //        $"WHERE b.id_customer = c.id_customer AND b.id_status = bs.id_status";

        //    string queryCondition = "";
        //    if (idDonHang != "" && idDonHang != null)
        //    {
        //        queryCondition += $" AND b.id_DonHang LIKE N'%{idDonHang}%'";
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

        //    var list = db.Database.SqlQuery<flatDonHang>(querySearch).ToPagedList<flatDonHang>(PageNum, PageSize);

        //    return list;
        //}

        //internal object DonHangDetail(int? id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}