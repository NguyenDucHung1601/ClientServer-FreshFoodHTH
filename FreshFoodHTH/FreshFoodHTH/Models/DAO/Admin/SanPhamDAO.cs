using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using FreshFoodHTH.Models.EFplus;

namespace FreshFoodHTH.Models.DAO.Admin
{
    public class SanPhamDAO
    {
        FreshFoodDBContext db;

        public SanPhamDAO()
        {
            db = new FreshFoodDBContext();
        }

        public List<SanPham> ListSanPham()
        {
            return db.SanPhams.ToList();
        }

        public SanPham GetByID(Guid id)
        {
            return db.SanPhams.Find(id);
        }

        public string GetIdentity()
        {
            int idOrder = Convert.ToInt32(db.Database.SqlQuery<decimal>("SELECT IDENT_CURRENT('SanPham')").FirstOrDefault());
            return idOrder.ToString("D4");
        }

        public void Add(SanPham obj)
        {
            db.SanPhams.Add(obj);
            db.SaveChanges();
        }

        public void Edit(SanPham obj)
        {
            SanPham sanpham = GetByID(obj.IDSanPham);
            if (sanpham != null)
            {
                sanpham.MaSo = obj.MaSo;
                sanpham.Ten = obj.Ten;
                sanpham.DonViTinh = obj.DonViTinh;
                sanpham.GiaTien = obj.GiaTien;
                sanpham.GiaKhuyenMai = obj.GiaKhuyenMai;
                sanpham.HinhAnh = obj.HinhAnh;
                sanpham.MoTa = obj.MoTa;
                sanpham.CoSan = obj.CoSan;
                sanpham.SoLuong = obj.SoLuong;
                sanpham.CreatedDate = obj.CreatedDate;
                sanpham.IDTheLoai = obj.IDTheLoai;

                db.SaveChanges();
            }
        }

        public int Delete(Guid id)
        {
            SanPham sanpham = db.SanPhams.Find(id);
            if (sanpham != null)
            {
                db.SanPhams.Remove(sanpham);
                return db.SaveChanges();
            }
            else
                return -1;
        }

        public IEnumerable<flatSanPham> ListSimple(string searching)
        {
            var list = db.Database.SqlQuery<flatSanPham>($"SELECT sp.IDSanPham, sp.MaSo, sp.Ten AS TenSanPham, tl.Ten AS TenTheLoai, sp.DonViTinh, sp.GiaTien, sp.GiaKhuyenMai, sp.HinhAnh, sp.CoSan, sp.SoLuong, sp.CreatedDate " +
               $"FROM dbo.SanPham sp LEFT JOIN dbo.TheLoai tl ON tl.IDTheLoai = sp.IDTheLoai " +
               $"WHERE sp.MaSo LIKE N'%{searching}%' " +
               $"OR sp.Ten LIKE N'%{searching}%' " +
               $"OR tl.Ten LIKE N'%{searching}%' " +
               $"OR sp.DonViTinh LIKE N'%{searching}%' " +
               $"OR sp.GiaTien LIKE N'%{searching}%' " +
               $"OR sp.GiaKhuyenMai LIKE N'%{searching}%' " +
               $"OR sp.SoLuong LIKE N'%{searching}%' " +
               $"OR sp.CreatedDate LIKE N'%{searching}%' " +
               $"ORDER BY sp.CreatedDate DESC").ToList();

            return list;
        }

        public IEnumerable<flatSanPham> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = db.Database.SqlQuery<flatSanPham>($"SELECT sp.IDSanPham, sp.MaSo, sp.Ten AS TenSanPham, tl.Ten AS TenTheLoai, sp.DonViTinh, sp.GiaTien, sp.GiaKhuyenMai, sp.HinhAnh, sp.CoSan, sp.SoLuong, sp.CreatedDate " +
                $"FROM dbo.SanPham sp LEFT JOIN dbo.TheLoai tl ON tl.IDTheLoai = sp.IDTheLoai " +
                $"WHERE sp.MaSo LIKE N'%{searching}%' " +
                $"OR sp.Ten LIKE N'%{searching}%' " +
                $"OR tl.Ten LIKE N'%{searching}%' " +
                $"OR sp.DonViTinh LIKE N'%{searching}%' " +
                $"OR sp.GiaTien LIKE N'%{searching}%' " +
                $"OR sp.GiaKhuyenMai LIKE N'%{searching}%' " +
                $"OR sp.SoLuong LIKE N'%{searching}%' " +
                $"OR sp.CreatedDate LIKE N'%{searching}%' " +
                $"ORDER BY sp.CreatedDate DESC").ToPagedList<flatSanPham>(PageNum, PageSize);

            return list;
        }

        //public IEnumerable<ProductView> ListAdvanced(string idProduct, string name, string category, string availability, string priceFrom, string priceTo, string salePercentFrom, string salePercentTo, string salePriceFrom, string salePriceTo, string rateFrom, string rateTo)
        //{
        //    string querySearch = "SELECT p.id_product, p.name as productName, c.name as categoryName, p.availability, p.price, p.salePercent, p.salePrice, p.rate, p.mainPhoto, p.updated " +
        //         $"FROM dbo.Product p LEFT JOIN dbo.Category c ON c.id_category = p.id_category ";

        //    string queryCondition = "";
        //    if (idProduct != "" && idProduct != null)
        //    {
        //        queryCondition += $" AND p.id_product LIKE N'%{idProduct}%'";
        //    }
        //    if (name != "" && name != null)
        //    {
        //        queryCondition += $" AND p.name LIKE N'%{name}%'";
        //    }
        //    if (category != "" && category != null)
        //    {
        //        queryCondition += $" AND p.id_category = {category}";
        //    }
        //    if (availability != "" && availability != null)
        //    {
        //        if (availability == "True")
        //            queryCondition += $" AND p.availability = 1";
        //        else
        //            queryCondition += $" AND p.availability = 0";
        //    }
        //    if (priceFrom != null && priceTo != null && priceFrom != "" && priceTo != "" && Convert.ToDecimal(priceFrom) <= Convert.ToDecimal(priceTo))
        //    {
        //        queryCondition += $" AND p.price >= {priceFrom} AND p.price <= {priceTo}";
        //    }
        //    if (salePercentFrom != null && salePercentTo != null && salePercentFrom != "" && salePercentTo != "" && Convert.ToInt32(salePercentFrom) <= Convert.ToInt32(salePercentTo))
        //    {
        //        queryCondition += $" AND p.salePercent >= {salePercentFrom} AND p.salePercent <= {salePercentTo}";
        //    }
        //    if (salePriceFrom != null && salePriceTo != null && salePriceFrom != "" && salePriceTo != "" && Convert.ToDecimal(salePriceFrom) <= Convert.ToDecimal(salePriceTo))
        //    {
        //        queryCondition += $" AND p.salePrice >= {salePriceFrom} AND p.salePrice <= {salePriceTo}";
        //    }
        //    if (rateFrom != null && rateTo != null && rateFrom != "" && rateTo != null && Convert.ToDouble(rateFrom) <= Convert.ToDouble(rateTo))
        //    {
        //        queryCondition += $" AND p.rate >= {rateFrom} AND p.rate<={rateTo}";
        //    }

        //    if (!queryCondition.Equals(""))
        //    {
        //        querySearch = querySearch + " WHERE" + queryCondition.Remove(0, 4);
        //    }

        //    var list = db.Database.SqlQuery<ProductView>(querySearch).ToList();

        //    return list;
        //}

        //public IEnumerable<ProductView> ListAdvancedSearch(int PageNum, int PageSize, string idProduct, string name, string category, string availability, string priceFrom, string priceTo, string salePercentFrom, string salePercentTo, string salePriceFrom, string salePriceTo, string rateFrom, string rateTo)
        //{
        //    string querySearch = "SELECT p.id_product, p.name as productName, c.name as categoryName, p.availability, p.price, p.salePercent, p.salePrice, p.rate, p.mainPhoto, p.updated " +
        //         $"FROM dbo.Product p LEFT JOIN dbo.Category c ON c.id_category = p.id_category ";

        //    string queryCondition = "";
        //    if (idProduct != "" && idProduct != null)
        //    {
        //        queryCondition += $" AND p.id_product LIKE N'%{idProduct}%'";
        //    }
        //    if (name != "" && name != null)
        //    {
        //        queryCondition += $" AND p.name LIKE N'%{name}%'";
        //    }
        //    if (category != "" && category != null)
        //    {
        //        queryCondition += $" AND p.id_category = {category}";
        //    }
        //    if (availability != "" && availability != null)
        //    {
        //        if (availability == "True")
        //            queryCondition += $" AND p.availability = 1";
        //        else
        //            queryCondition += $" AND p.availability = 0";
        //    }
        //    if (priceFrom != null && priceTo != null && priceFrom != "" && priceTo != "" && Convert.ToDecimal(priceFrom) <= Convert.ToDecimal(priceTo))
        //    {
        //        queryCondition += $" AND p.price >= {priceFrom} AND p.price <= {priceTo}";
        //    }
        //    if (salePercentFrom != null && salePercentTo != null && salePercentFrom != "" && salePercentTo != "" && Convert.ToInt32(salePercentFrom) <= Convert.ToInt32(salePercentTo))
        //    {
        //        queryCondition += $" AND p.salePercent >= {salePercentFrom} AND p.salePercent <= {salePercentTo}";
        //    }
        //    if (salePriceFrom != null && salePriceTo != null && salePriceFrom != "" && salePriceTo != "" && Convert.ToDecimal(salePriceFrom) <= Convert.ToDecimal(salePriceTo))
        //    {
        //        queryCondition += $" AND p.salePrice >= {salePriceFrom} AND p.salePrice <= {salePriceTo}";
        //    }
        //    if (rateFrom != null && rateTo != null && rateFrom != "" && rateTo != "" && Convert.ToDouble(rateFrom) <= Convert.ToDouble(rateTo))
        //    {
        //        queryCondition += $" AND p.rate >= {rateFrom} AND p.rate<={rateTo}";
        //    }

        //    if (!queryCondition.Equals(""))
        //    {
        //        querySearch = querySearch + " WHERE" + queryCondition.Remove(0, 4);
        //    }

        //    var list = db.Database.SqlQuery<ProductView>(querySearch).ToPagedList<ProductView>(PageNum, PageSize);

        //    return list;
        //}
    }
}