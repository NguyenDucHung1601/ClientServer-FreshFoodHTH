using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using FreshFoodHTH.Models.EFplus;

namespace FreshFoodHTH.Models.DAO.Admin
{
    public class TKThanhToanNguoiDungDAO
    {
        FreshFoodDBContext db;

        public TKThanhToanNguoiDungDAO()
        {
            db = new FreshFoodDBContext();
        }

        public List<TKThanhToanNguoiDung> ListTKThanhToanNguoiDung()
        {
            return db.TKThanhToanNguoiDungs.ToList();
        }

        public TKThanhToanNguoiDung GetByID(Guid idnd,Guid idtk)
        {
            return db.TKThanhToanNguoiDungs.Where(x=>x.IDNguoiDung==idnd).Where(x=>x.IDTaiKhoan==idtk).ToList().ElementAt(0);
        }

        public string GetIdentity()
        {
            int idOrder = Convert.ToInt32(db.Database.SqlQuery<decimal>("SELECT IDENT_CURRENT('TKThanhToanNguoiDung')").FirstOrDefault());
            return idOrder.ToString("D4");
        }

        public void Add(TKThanhToanNguoiDung obj)
        {
            db.TKThanhToanNguoiDungs.Add(obj);
            db.SaveChanges();
        }

        public void Edit(TKThanhToanNguoiDung obj)
        {
            TKThanhToanNguoiDung tkthanhtoannguoidung = GetByID(obj.IDNguoiDung,obj.IDTaiKhoan);
            if (tkthanhtoannguoidung != null)
            {
                tkthanhtoannguoidung.Username = obj.Username;
                tkthanhtoannguoidung.Password = obj.Password;
                tkthanhtoannguoidung.TongTien = obj.TongTien;

                db.SaveChanges();
            }
        }

        public int Delete(Guid idnd,Guid idtk )
        {
            TKThanhToanNguoiDung tkthanhtoannguoidung = db.TKThanhToanNguoiDungs.Where(x => x.IDNguoiDung == idnd).Where(x => x.IDTaiKhoan == idtk).ToList().ElementAt(0);
            if (tkthanhtoannguoidung != null)
            {
                db.TKThanhToanNguoiDungs.Remove(tkthanhtoannguoidung);
                return db.SaveChanges();
            }
            else
                return -1;
        }

        public IEnumerable<TKThanhToanNguoiDung> ListSimple(string searching)
        {
            var list = db.Database.SqlQuery<TKThanhToanNguoiDung>($"SELECT * FROM dbo.TKThanhToanNguoiDung tkttnd " +
                $"WHERE tkttnd.TaiKhoan LIKE N'%{searching}%' " +
                $"ORDER BY tkttnd.TaiKhoan").ToList();

            return list;
        }

        public IEnumerable<TKThanhToanNguoiDung> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = db.Database.SqlQuery<TKThanhToanNguoiDung>($"SELECT * FROM dbo.TKThanhToanNguoiDung tkttnd " +
               $"WHERE tkttnd.TaiKhoan LIKE N'%{searching}%' " +
               $"ORDER BY tkttnd.TaiKhoan").ToPagedList<TKThanhToanNguoiDung>(PageNum, PageSize);

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