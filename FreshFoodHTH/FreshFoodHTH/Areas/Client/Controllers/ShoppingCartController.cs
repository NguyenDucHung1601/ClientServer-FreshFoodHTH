using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FreshFoodHTH.Common;

namespace FreshFoodHTH.Areas.Client.Controllers
{
    public class ShoppingCartController : BaseController
    {
        FreshFoodDBContext db = new FreshFoodDBContext();

        // GET: Client/ShoppingCart
        public ActionResult Index(Guid id)
        {
            var list = db.ChiTietGioHangs.Where(x => x.IDKhachHang == id).ToList();
            return View(list);
        }

        public ActionResult DeleteCartDetail(Guid id)
        {
            ChiTietGioHang obj = db.ChiTietGioHangs.Find(id);
            db.ChiTietGioHangs.Remove(obj);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = Session["IDUSER_SESSION"] });
        }

        public ActionResult UpdateCart()
        {
            return RedirectToAction("Index", "ShoppingCart", new { id = Session["IDUSER_SESSION"] });
        }

        public ActionResult HeaderCart()
        {
            return PartialView();
        }
    }
}