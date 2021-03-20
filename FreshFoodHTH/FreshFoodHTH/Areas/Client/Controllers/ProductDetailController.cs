using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodHTH.Areas.Client.Controllers
{
    public class ProductDetailController : Controller
    {
        // GET: Client/ProductDetail
        FreshFoodDBContext db = new FreshFoodDBContext();
        public ActionResult Index(Guid id)
        {
            SanPham sanpham = db.SanPhams.Find(id);
            sanpham.SoLuotXem = sanpham.SoLuotXem + 1;
            db.SaveChanges();

            ViewBag.Product = sanpham;

            ChiTietGioHang obj = new ChiTietGioHang();
            obj.IDChiTietGioHang = Guid.NewGuid();
            obj.IDSanPham = id;
            obj.SoLuong = 1;

            return View(obj);
        }

        public ActionResult RelatedProduct(Guid idtheloai, string ten)
        {
            IList<SanPham> list = db.SanPhams.Where(x => x.IDTheLoai == idtheloai).ToList();

            return PartialView(list);
        }
    }
}