using FreshFoodHTH.Models.DAO.Admin;
using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodHTH.Areas.Admin.Controllers
{
    public class LoaiNguoiDungController : BaseController
    {
        LoaiNguoiDungDAO lndDao = new LoaiNguoiDungDAO();

        // GET: Admin/LoaiNguoiDung
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
            ViewBag.Count = lndDao.ListSimple(searching).Count();
            return View(lndDao.ListSimpleSearch(pageNumber, pagesize, searching));
        }

        public ActionResult Details(Guid id)
        {
            LoaiNguoiDung loainguoidung = lndDao.GetByID(id);
            return View(loainguoidung);
        }

        public ActionResult Create()
        {
            List<LoaiNguoiDung> loainguoidung = lndDao.ListLoaiNguoiDung();
            SelectList loainguoidungList = new SelectList(loainguoidung, "IDLoaiNguoiDung", "Ten", "Ten");
            ViewBag.LoaiNguoiDung= loainguoidungList;
            return View();
        }

        [HttpPost]
        public ActionResult Create(string ten)
        {
            LoaiNguoiDung loainguoidung = new LoaiNguoiDung();
            loainguoidung.IDLoaiNguoiDung = Guid.NewGuid();
            loainguoidung.Ten = ten;
            if (ModelState.IsValid)
            {
                lndDao.Add(loainguoidung);
                return RedirectToAction("Index");
            }
            else
            {
                return View(loainguoidung);
            }
        }

        public ActionResult Edit(Guid id)
        {
            return View(lndDao.GetByID(id));
        }

        [HttpPost]
        public ActionResult Edit(Guid id,string ten)
        {
            LoaiNguoiDung loainguoidung = lndDao.GetByID(id);
            loainguoidung.Ten = ten;
            if (ModelState.IsValid)
            {
                lndDao.Edit(loainguoidung);
                return RedirectToAction("Index");
            }
            else
            {
                return View(loainguoidung);
            }
        }

        public ActionResult Delete(Guid id)
        {
            lndDao.Delete(id);
            return RedirectToAction("Index");
        }

    }
}