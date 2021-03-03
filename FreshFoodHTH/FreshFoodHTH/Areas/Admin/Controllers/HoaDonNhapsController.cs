using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FreshFoodHTH.Models.DAO.Admin;
using FreshFoodHTH.Models.EF;

namespace FreshFoodHTH.Areas.Admin.Controllers
{
    public class HoaDonNhapsController : Controller
    {
        private FreshFoodDBContext db = new FreshFoodDBContext();
        public HoaDonNhapDAO hdnDao = new HoaDonNhapDAO();
        public ChiTietHoaDonNhapDAO cthdnDao = new ChiTietHoaDonNhapDAO();

        // GET: Admin/HoaDonNhaps
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
            ViewBag.Count = hdnDao.ListSimple(searching).Count();
            //var hoaDonNhaps = db.HoaDonNhaps.Include(h => h.NhaCungCap);
            return View(hdnDao.ListSimpleSearch(pageNumber, pagesize, searching));
        }

        // GET: Admin/HoaDonNhaps/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonNhap hoaDonNhap = db.HoaDonNhaps.Find(id);
            if (hoaDonNhap == null)
            {
                return HttpNotFound();
            }
            return View(hoaDonNhap);
        }

        // GET: Admin/HoaDonNhaps/Create
        public ActionResult Create()
        {
            ViewBag.IDNhaCungCap = new SelectList(db.NhaCungCaps, "IDNhaCungCap", "Ten");
            return View();
        }

        // POST: Admin/HoaDonNhaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDHoaDonNhap,MaSo,IDNhaCungCap,GhiChu,TienHang,TienShip,TienGiam,TongTien,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] HoaDonNhap hoaDonNhap)
        {
            if (ModelState.IsValid)
            {
                hoaDonNhap.IDHoaDonNhap = Guid.NewGuid();
                hoaDonNhap.CreatedDate = DateTime.Now;
                hoaDonNhap.CreatedBy = (string)Session["USERNAME_SESSION"];
                hoaDonNhap.ModifiedDate = DateTime.Now;
                hoaDonNhap.ModifiedBy = (string)Session["USERNAME_SESSION"];
                hoaDonNhap.TongTien = hoaDonNhap.TienHang + hoaDonNhap.TienShip - hoaDonNhap.TienGiam;
                db.HoaDonNhaps.Add(hoaDonNhap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDNhaCungCap = new SelectList(db.NhaCungCaps, "IDNhaCungCap", "Ten", hoaDonNhap.IDNhaCungCap);
            return View(hoaDonNhap);
        }

        // GET: Admin/HoaDonNhaps/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonNhap hoaDonNhap = hdnDao.getByID(id);
            if (hoaDonNhap == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDNhaCungCap = new SelectList(db.NhaCungCaps, "IDNhaCungCap", "Ten", hoaDonNhap.IDNhaCungCap);
            return View(hoaDonNhap);
        }

        // POST: Admin/HoaDonNhaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDHoaDonNhap,MaSo,IDNhaCungCap,GhiChu,TienHang,TienShip,TienGiam,TongTien,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] HoaDonNhap hoaDonNhap)
        {
            if (ModelState.IsValid)
            {
                hoaDonNhap.CreatedDate = hoaDonNhap.CreatedDate == null ? DateTime.Now : hoaDonNhap.CreatedDate;
                hoaDonNhap.ModifiedDate = DateTime.Now;
                hoaDonNhap.ModifiedBy = (string)Session["USERNAME_SESSION"];
                hoaDonNhap.TongTien = hoaDonNhap.TienHang + hoaDonNhap.TienShip - hoaDonNhap.TienGiam;
                db.Entry(hoaDonNhap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDNhaCungCap = new SelectList(db.NhaCungCaps, "IDNhaCungCap", "Ten", hoaDonNhap.IDNhaCungCap);
            return View(hoaDonNhap);
        }

        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hdnDao.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult IndexChiTietHoaDonNhap(Guid id)
        {
            var listChiTietHoaDonNhap = cthdnDao.GetListChiTietHoaDonNhap(id);

            if (listChiTietHoaDonNhap == null)
                return HttpNotFound();

            ViewBag.IDHoaDonNhap = id;

            return PartialView(listChiTietHoaDonNhap);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
