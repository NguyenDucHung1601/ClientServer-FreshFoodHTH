using FreshFoodHTH.Models.DAO.Admin;
using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodHTH.Areas.Client.Controllers
{
    public class HomeController : Controller
    {
        FreshFoodDBContext db = new FreshFoodDBContext();
        TheLoaiDAO tlDao = new TheLoaiDAO();

        // GET: Client/Home
        public ActionResult Index(string searching)
        {
            IEnumerable<SanPham> list;
            ViewBag.Searching = searching;
            if (!string.IsNullOrEmpty(searching))
                list = db.SanPhams.Where(x => x.Ten.Contains(searching)).ToList();
            else
                list = db.SanPhams.ToList();
            ViewBag.SearchList = list;
            return View(list);
        }

        public ActionResult IndexByCategory(Guid idcategory)
        {
            IEnumerable<SanPham> list;
            list = db.SanPhams.Where(x => x.IDTheLoai == idcategory).ToList();
            ViewBag.CategoryName = (tlDao.GetByID(idcategory)).Ten;
            return View(list);
        }

        public ActionResult CategoryShow()
        {
            return PartialView(db.TheLoais.ToList());
        }

        public ActionResult CategoryShowImage()
        {
            return PartialView(db.TheLoais.ToList());
        }

        public ActionResult ListCategoryShow()
        {
            return PartialView(db.TheLoais.ToList());
        }

        public ActionResult HeaderCart()
        {
            return PartialView();
        }

        public ActionResult ProductTopNew()
        {
            return PartialView(db.SanPhams.OrderByDescending(p => p.ModifiedDate).Take(9));
        }

        public ActionResult ProductTopSale()
        {
            return PartialView(db.SanPhams.OrderByDescending(p => p.SoLuotMua).Take(9));
        }

        public ActionResult ProductTopReview()
        {
            return PartialView(db.SanPhams.OrderByDescending(p => p.SoLuotXem).Take(9));
        }

        public ActionResult SaleOff()
        {
            return PartialView(db.SanPhamKhuyenMais.Where(x => x.ThoiGianKetThuc < DateTime.Now).ToList());
        }
    }
}