using FreshFoodHTH.Models.DAO.Client;
using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodHTH.Areas.Client.Controllers
{
    public class ShoppingCartController : BaseController
    {
        FreshFoodDBContext db = new FreshFoodDBContext();
        ClientDonHangDAO cdhDao = new ClientDonHangDAO();


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
            var idKH = (Guid)Session["IDUSER_SESSION"];
            cdhDao.CapNhatTongTienGioHangSoBo(idKH);
            return RedirectToAction("Index", "ShoppingCart", new { id = Session["IDUSER_SESSION"] });
        }
    }
}