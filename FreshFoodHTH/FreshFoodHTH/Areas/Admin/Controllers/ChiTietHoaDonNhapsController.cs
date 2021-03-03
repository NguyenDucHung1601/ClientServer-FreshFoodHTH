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
    public class ChiTietHoaDonNhapsController : Controller
    {
        private FreshFoodDBContext db = new FreshFoodDBContext();
        public static Guid IDHoaDonNhapTemp;
        public HoaDonNhapDAO hdnDao = new HoaDonNhapDAO();
        public ChiTietHoaDonNhapDAO cthdnDao = new ChiTietHoaDonNhapDAO();
        // GET: Admin/ChiTietHoaDonNhaps
        public ActionResult Index()
        {
            var chiTietHoaDonNhaps = db.ChiTietHoaDonNhaps.Include(c => c.HoaDonNhap).Include(c => c.SanPham);
            return View(chiTietHoaDonNhaps.ToList());
        }

        // GET: Admin/ChiTietHoaDonNhaps/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDonNhap chiTietHoaDonNhap = db.ChiTietHoaDonNhaps.Find(id);
            if (chiTietHoaDonNhap == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHoaDonNhap);
        }

        // GET: Admin/ChiTietHoaDonNhaps/Create
        public ActionResult Create(Guid idhdn)
        {
            IDHoaDonNhapTemp = idhdn;
            ViewBag.ID = idhdn;
            ViewBag.MaSo = hdnDao.getByID(idhdn).MaSo.ToString("D6");
            ViewBag.IDHoaDonNhap = new SelectList(db.HoaDonNhaps, "IDHoaDonNhap", "MaSo");
            ViewBag.IDSanPham = new SelectList(db.SanPhams, "IDSanPham", "Ten");
            return View();
        }

        // POST: Admin/ChiTietHoaDonNhaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDChiTietHoaDonNhap,IDHoaDonNhap,IDSanPham,DonViTinh,DonGiaNhap,SoLuong,ThanhTien")] ChiTietHoaDonNhap chiTietHoaDonNhap)
        {
            if (ModelState.IsValid)
            {
                chiTietHoaDonNhap.IDHoaDonNhap = IDHoaDonNhapTemp;
                chiTietHoaDonNhap.IDChiTietHoaDonNhap = Guid.NewGuid();
                chiTietHoaDonNhap.ThanhTien = chiTietHoaDonNhap.SoLuong * chiTietHoaDonNhap.DonGiaNhap;
                db.ChiTietHoaDonNhaps.Add(chiTietHoaDonNhap);
                db.SaveChanges();
                var hdn = hdnDao.getByID(chiTietHoaDonNhap.IDHoaDonNhap);
                var listChiTietHoaDonNhap = cthdnDao.GetListChiTietHoaDonNhap(chiTietHoaDonNhap.IDHoaDonNhap);
                hdn.TienHang = listChiTietHoaDonNhap.Sum(x => x.ThanhTien);
                hdn.TongTien = hdn.TienHang + hdn.TienShip - hdn.TienGiam;
                hdnDao.Edit(hdn);
                return RedirectToAction("Details", "HoaDonNhaps", new { id = IDHoaDonNhapTemp });
            }

            ViewBag.IDHoaDonNhap = new SelectList(db.HoaDonNhaps, "IDHoaDonNhap", "MaSo", chiTietHoaDonNhap.IDHoaDonNhap);
            ViewBag.IDSanPham = new SelectList(db.SanPhams, "IDSanPham", "Ten", chiTietHoaDonNhap.IDSanPham);
            return RedirectToAction("Details", "HoaDonNhaps", new { id = IDHoaDonNhapTemp });
        }

        // GET: Admin/ChiTietHoaDonNhaps/Edit/5
        public ActionResult Edit(Guid? id , Guid idhdn)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDonNhap chiTietHoaDonNhap = db.ChiTietHoaDonNhaps.Find(id);
            if (chiTietHoaDonNhap == null)
            {
                return HttpNotFound();
            }
            IDHoaDonNhapTemp = idhdn;
            chiTietHoaDonNhap.IDHoaDonNhap = idhdn;
            ViewBag.IDHoaDonNhap = new SelectList(db.HoaDonNhaps, "IDHoaDonNhap", "MaSo", chiTietHoaDonNhap.IDHoaDonNhap);
            ViewBag.IDSanPham = new SelectList(db.SanPhams, "IDSanPham", "Ten", chiTietHoaDonNhap.IDSanPham);
            return View(chiTietHoaDonNhap);
        }

        // POST: Admin/ChiTietHoaDonNhaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDChiTietHoaDonNhap,IDHoaDonNhap,IDSanPham,DonViTinh,DonGiaNhap,SoLuong,ThanhTien")] ChiTietHoaDonNhap chiTietHoaDonNhap)
        {
            if (ModelState.IsValid)
            {
                chiTietHoaDonNhap.IDHoaDonNhap = IDHoaDonNhapTemp;
                chiTietHoaDonNhap.ThanhTien = chiTietHoaDonNhap.SoLuong * chiTietHoaDonNhap.DonGiaNhap;
                db.Entry(chiTietHoaDonNhap).State = EntityState.Modified;
                db.SaveChanges();
                var hdn = hdnDao.getByID(chiTietHoaDonNhap.IDHoaDonNhap);
                var listChiTietHoaDonNhap = cthdnDao.GetListChiTietHoaDonNhap(chiTietHoaDonNhap.IDHoaDonNhap);
                hdn.TienHang = listChiTietHoaDonNhap.Sum(x => x.ThanhTien);
                hdn.TongTien = hdn.TienHang + hdn.TienShip - hdn.TienGiam;
                hdnDao.Edit(hdn);
                return RedirectToAction("Details", "HoaDonNhaps", new { id = chiTietHoaDonNhap.IDHoaDonNhap });
            }
            ViewBag.IDHoaDonNhap = new SelectList(db.HoaDonNhaps, "IDHoaDonNhap", "MaSo", chiTietHoaDonNhap.IDHoaDonNhap);
            ViewBag.IDSanPham = new SelectList(db.SanPhams, "IDSanPham", "Ten", chiTietHoaDonNhap.IDSanPham);
            return RedirectToAction("Details", "HoaDonNhaps", new { id = chiTietHoaDonNhap.IDHoaDonNhap });
        }

        // GET: Admin/ChiTietHoaDonNhaps/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDonNhap chiTietHoaDonNhap = db.ChiTietHoaDonNhaps.Find(id);
            db.ChiTietHoaDonNhaps.Remove(chiTietHoaDonNhap);
            db.SaveChanges();
            return RedirectToAction("Details", "HoaDonNhaps", new { id = chiTietHoaDonNhap.IDHoaDonNhap });
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
