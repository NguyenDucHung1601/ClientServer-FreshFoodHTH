using FreshFoodHTH.Models.DAO.Admin;
using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodHTH.Areas.Admin.Controllers
{
    public class ThongTinLienHeController : BaseController
    {
        ThongTinLienHeDAO ttlhDao = new ThongTinLienHeDAO();
        // GET: Admin/ThongTinLienHe
        public ActionResult Index(int? page, int? PageSize, string searching = "")
        {
            ViewBag.SearchString = searching;
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="20", Text= "20" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" }
            };
            int pageNumber = (page ?? 1);
            int pagesize = (PageSize ?? 10);
            ViewBag.psize = pagesize;
            ViewBag.Count = ttlhDao.ListSimple(searching).Count();
            return View(ttlhDao.ListSimpleSearch(pageNumber, pagesize, searching));
        }
        public ActionResult Details(Guid id)
        {
            ThongTinLienHe thongtinlienhe = ttlhDao.GetByID(id);
            return View(thongtinlienhe);
        }
        public ActionResult Edit(Guid id)
        {
            return View(ttlhDao.GetByID(id));
        }
        [HttpPost]
        public ActionResult Edit(Guid id, string tencuahang, string diachi, string dienthoai1, string dienthoai2, string giomocua, string email,string facebook, string youtube, string instagram)
        {
            ThongTinLienHe thongtinlienhe = ttlhDao.GetByID(id);
            thongtinlienhe.TenCuaHang = tencuahang;
            thongtinlienhe.DiaChi = diachi;
            thongtinlienhe.DienThoai1 = dienthoai1;
            thongtinlienhe.DienThoai2 = dienthoai2;
            thongtinlienhe.GioMoCua = giomocua;
            thongtinlienhe.Email = email;
            thongtinlienhe.LinkFacebook = facebook;
            thongtinlienhe.LinkYoutube = youtube;
            thongtinlienhe.LinkInstagram = instagram;
            if (ModelState.IsValid)
            {
                ttlhDao.Edit(thongtinlienhe);
                return RedirectToAction("Index");
            }
            else
            {
                return View(thongtinlienhe);
            }
        }
    }
}