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
    public class NhaCungCapSanPhamsController : Controller
    {
        private FreshFoodDBContext db = new FreshFoodDBContext();
        public NhaCungCapSanPhamDAO nccspDao = new NhaCungCapSanPhamDAO();
        // GET: Admin/NhaCungCapSanPhams
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
            ViewBag.Count = nccspDao.ListSimple(searching).Count();
            return View(nccspDao.ListSimpleSearch(pageNumber, pagesize, searching));
        }

        // GET: Admin/NhaCungCapSanPhams/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCungCapSanPham nhaCungCapSanPham = db.NhaCungCapSanPhams.Find(id);
            if (nhaCungCapSanPham == null)
            {
                return HttpNotFound();
            }
            return View(nhaCungCapSanPham);
        }

        // GET: Admin/NhaCungCapSanPhams/Create
        public ActionResult Create()
        {
            ViewBag.IDNhaCungCap = new SelectList(db.NhaCungCaps, "IDNhaCungCap", "Ten");
            ViewBag.IDSanPham = new SelectList(db.SanPhams, "IDSanPham", "Ten");
            return View();
        }

        // POST: Admin/NhaCungCapSanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDNhaCungCapSanPham,IDNhaCungCap,IDSanPham,DonViTinh,GiaCungUng,NgayCapNhat")] NhaCungCapSanPham nhaCungCapSanPham)
        {
            if (ModelState.IsValid)
            {
                nhaCungCapSanPham.IDNhaCungCapSanPham = Guid.NewGuid();
                nhaCungCapSanPham.NgayCapNhat = DateTime.Now;
                db.NhaCungCapSanPhams.Add(nhaCungCapSanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDNhaCungCap = new SelectList(db.NhaCungCaps, "IDNhaCungCap", "Ten", nhaCungCapSanPham.IDNhaCungCap);
            ViewBag.IDSanPham = new SelectList(db.SanPhams, "IDSanPham", "Ten", nhaCungCapSanPham.IDSanPham);
            return View(nhaCungCapSanPham);
        }

        // GET: Admin/NhaCungCapSanPhams/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCungCapSanPham nhaCungCapSanPham = db.NhaCungCapSanPhams.Find(id);
            if (nhaCungCapSanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDNhaCungCap = new SelectList(db.NhaCungCaps, "IDNhaCungCap", "Ten", nhaCungCapSanPham.IDNhaCungCap);
            ViewBag.IDSanPham = new SelectList(db.SanPhams, "IDSanPham", "Ten", nhaCungCapSanPham.IDSanPham);
            return View(nhaCungCapSanPham);
        }

        // POST: Admin/NhaCungCapSanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDNhaCungCapSanPham,IDNhaCungCap,IDSanPham,DonViTinh,GiaCungUng,NgayCapNhat")] NhaCungCapSanPham nhaCungCapSanPham)
        {
            if (ModelState.IsValid)
            {
                nhaCungCapSanPham.NgayCapNhat = DateTime.Now;
                db.Entry(nhaCungCapSanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDNhaCungCap = new SelectList(db.NhaCungCaps, "IDNhaCungCap", "Ten", nhaCungCapSanPham.IDNhaCungCap);
            ViewBag.IDSanPham = new SelectList(db.SanPhams, "IDSanPham", "Ten", nhaCungCapSanPham.IDSanPham);
            return View(nhaCungCapSanPham);
        }

        // GET: Admin/NhaCungCapSanPhams/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nccspDao.Delete(id);
            return RedirectToAction("Index");
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
