using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodHTH.Areas.Client.Controllers
{
    public class ShopController : BaseController
    {
        FreshFoodDBContext db = new FreshFoodDBContext();
        // GET: Client/Shop
        public ActionResult Index(string name, string idcategory, string priceFrom, string priceTo)
        {
            ViewBag.ProductName = name;
            ViewBag.IdCategory = idcategory;
            ViewBag.PriceFrom = priceFrom;
            ViewBag.PriceTo = priceTo;

            SelectList theloaiList = new SelectList(db.TheLoais.ToList(), "IDTheLoai", "Ten", "IDTheLoai");
            ViewBag.CategoryList = theloaiList;

            List<SanPham> list = db.SanPhams.ToList();
            if (!string.IsNullOrEmpty(name))
                list = list.Where(x => x.Ten.ToLower().Contains(name.ToLower())).ToList();
            if (!string.IsNullOrEmpty(idcategory))
                list = list.Where(x => x.IDTheLoai.ToString().Equals(idcategory)).ToList();
            if (!string.IsNullOrEmpty(priceFrom) && !string.IsNullOrEmpty(priceTo))
                if (Convert.ToDecimal(priceFrom) < Convert.ToDecimal(priceTo))
                    list = list.Where(x => x.GiaKhuyenMai >= Convert.ToDecimal(priceFrom) && x.GiaKhuyenMai <= Convert.ToDecimal(priceTo)).ToList();
            return View(list);
        }

        public ActionResult ListCategoryShow()
        {
            return PartialView(db.TheLoais.ToList());
        }

        public ActionResult ProductTopNew()
        {
            return PartialView(db.SanPhams.OrderByDescending(p => p.ModifiedDate).Take(9));
        }

        public ActionResult ProductTopSale()
        {
            return PartialView(db.SanPhams.OrderByDescending(p => p.SoLuotMua).Take(9));
        }

        public ActionResult SaleOff()
        {
            return PartialView(db.SanPhamKhuyenMais.Where(x => x.ThoiGianKetThuc < DateTime.Now).ToList());
        }
    }
}