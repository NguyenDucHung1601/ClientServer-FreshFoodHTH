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

        // GET: Client/Home
        public ActionResult Index(string searching)
        {
            IEnumerable<SanPham> list;
            ViewBag.Searching = searching;
            if (!string.IsNullOrEmpty(searching))
                list = db.SanPhams.Where(x => x.Ten.Contains(searching) || x.TheLoai.Ten.Contains(searching)).ToList();
            else
                list = db.SanPhams.ToList();
            ViewBag.SearchList = list;
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
    }
}