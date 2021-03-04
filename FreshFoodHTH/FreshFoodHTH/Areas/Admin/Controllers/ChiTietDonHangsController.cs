using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FreshFoodHTH.Models.EF;

namespace FreshFoodHTH.Areas.Admin.Controllers
{
    public class ChiTietDonHangsController : Controller
    {
        private FreshFoodDBContext db = new FreshFoodDBContext();

        // GET: Admin/ChiTietDonHangs
        public ActionResult Index()
        {
            var chiTietDonHangs = db.ChiTietDonHangs.Include(c => c.DonHang).Include(c => c.SanPham);
            return View(chiTietDonHangs.ToList());
        }

        // GET: Admin/ChiTietDonHangs/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(id);
            if (chiTietDonHang == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDonHang);
        }

        // GET: Admin/ChiTietDonHangs/Create
        public ActionResult Create()
        {
            ViewBag.IDDonHang = new SelectList(db.DonHangs, "IDDonHang", "GhiChu");
            ViewBag.IDSanPham = new SelectList(db.SanPhams, "IDSanPham", "MaSo");
            return View();
        }

        // POST: Admin/ChiTietDonHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDDonHang,IDSanPham,DonViTinh,DonGiaBan,SoLuong,ThanhTien")] ChiTietDonHang chiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                chiTietDonHang.IDDonHang = Guid.NewGuid();
                db.ChiTietDonHangs.Add(chiTietDonHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDDonHang = new SelectList(db.DonHangs, "IDDonHang", "GhiChu", chiTietDonHang.IDDonHang);
            ViewBag.IDSanPham = new SelectList(db.SanPhams, "IDSanPham", "MaSo", chiTietDonHang.IDSanPham);
            return View(chiTietDonHang);
        }

        // GET: Admin/ChiTietDonHangs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(id);
            if (chiTietDonHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDDonHang = new SelectList(db.DonHangs, "IDDonHang", "GhiChu", chiTietDonHang.IDDonHang);
            ViewBag.IDSanPham = new SelectList(db.SanPhams, "IDSanPham", "MaSo", chiTietDonHang.IDSanPham);
            return View(chiTietDonHang);
        }

        // POST: Admin/ChiTietDonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDDonHang,IDSanPham,DonViTinh,DonGiaBan,SoLuong,ThanhTien")] ChiTietDonHang chiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietDonHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDDonHang = new SelectList(db.DonHangs, "IDDonHang", "GhiChu", chiTietDonHang.IDDonHang);
            ViewBag.IDSanPham = new SelectList(db.SanPhams, "IDSanPham", "MaSo", chiTietDonHang.IDSanPham);
            return View(chiTietDonHang);
        }

        // GET: Admin/ChiTietDonHangs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(id);
            if (chiTietDonHang == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDonHang);
        }

        // POST: Admin/ChiTietDonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(id);
            db.ChiTietDonHangs.Remove(chiTietDonHang);
            db.SaveChanges();
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
