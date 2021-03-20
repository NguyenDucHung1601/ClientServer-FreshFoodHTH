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
    public class DonHangsController : BaseController
    {
        private FreshFoodDBContext db = new FreshFoodDBContext();
        public DonHangDAO dhDao = new DonHangDAO();
        public ChiTietDonHangDAO ctdhDao = new ChiTietDonHangDAO();


        // GET: Admin/DonHangs
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
            ViewBag.Count = dhDao.ListSimple(searching).Count();
            //var donHangs = db.DonHangs.Include(d => d.NguoiDung).Include(d => d.PhuongThucThanhToan).Include(d => d.TrangThai);
            return View(dhDao.ListSimpleSearch(pageNumber, pagesize, searching));
        }

        // GET: Admin/DonHangs/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = dhDao.getByID(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // GET: Admin/DonHangs/Create
        public ActionResult Create()
        {
            ViewBag.IDKhachHang = new SelectList(db.NguoiDungs, "IDNguoiDung", "Ten");
            ViewBag.IDPhuongThucThanhToan = new SelectList(db.PhuongThucThanhToans, "IDPhuongThucThanhToan", "TenPhuongThucThanhToan");
            ViewBag.IDTrangThai = new SelectList(db.TrangThais, "IDTrangThai", "TenTrangThai");
            return View();
        }

        // POST: Admin/DonHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //,CreatedBy,ModifiedDate,ModifiedBy
        public ActionResult Create([Bind(Include = "IDDonHang,IDKhachHang,GhiChu,TienHang,TienShip,TienGiam,TongTien,IDTrangThai,IDPhuongThucThanhToan,CreatedDate")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                donHang.IDDonHang = Guid.NewGuid();
                donHang.CreatedDate = DateTime.Now;
                donHang.CreatedBy = (string)Session["USERNAME_SESSION"];
                donHang.ModifiedDate = DateTime.Now;
                donHang.ModifiedBy = (string)Session["USERNAME_SESSION"];
                donHang.TongTien = 0;
                db.DonHangs.Add(donHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDKhachHang = new SelectList(db.NguoiDungs, "IDNguoiDung", "Ten", donHang.IDKhachHang);
            ViewBag.IDPhuongThucThanhToan = new SelectList(db.PhuongThucThanhToans, "IDPhuongThucThanhToan", "TenPhuongThucThanhToan", donHang.IDPhuongThucThanhToan);
            ViewBag.IDTrangThai = new SelectList(db.TrangThais, "IDTrangThai", "TenTrangThai", donHang.IDTrangThai);
            return View(donHang);
        }

        // GET: Admin/DonHangs/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = dhDao.getByID(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDKhachHang = new SelectList(db.NguoiDungs, "IDNguoiDung", "Ten", donHang.IDKhachHang);
            ViewBag.IDPhuongThucThanhToan = new SelectList(db.PhuongThucThanhToans, "IDPhuongThucThanhToan", "TenPhuongThucThanhToan", donHang.IDPhuongThucThanhToan);
            ViewBag.IDTrangThai = new SelectList(db.TrangThais, "IDTrangThai", "TenTrangThai", donHang.IDTrangThai);
            return View(donHang);
        }

        // POST: Admin/DonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDDonHang,IDKhachHang,GhiChu,TienHang,TienShip,TienGiam,TongTien,IDTrangThai,IDPhuongThucThanhToan,CreatedDate")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                donHang.ModifiedDate = DateTime.Now;
                donHang.ModifiedBy = (string)Session["USERNAME_SESSION"];
                db.Entry(donHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDKhachHang = new SelectList(db.NguoiDungs, "IDNguoiDung", "Ten", donHang.IDKhachHang);
            ViewBag.IDPhuongThucThanhToan = new SelectList(db.PhuongThucThanhToans, "IDPhuongThucThanhToan", "TenPhuongThucThanhToan", donHang.IDPhuongThucThanhToan);
            ViewBag.IDTrangThai = new SelectList(db.TrangThais, "IDTrangThai", "TenTrangThai", donHang.IDTrangThai);
            return View(donHang);
        }

        // GET: Admin/DonHangs/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dhDao.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult IndexChiTietDonHang(Guid id)
        {
            var listChiTietDonHang = ctdhDao.GetListChiTietDonHang(id);

            if (listChiTietDonHang == null)
                return HttpNotFound();

            ViewBag.IDDonHang = id;

            return PartialView(listChiTietDonHang);
        }


        public ActionResult Accept(Guid id)
        {
            var donHang = db.DonHangs.SingleOrDefault(x => x.IDDonHang == id);
            donHang.IDTrangThai = new Guid("3240c2e6-fc6c-4feb-9313-382bd05cf522");
            donHang.ModifiedDate = DateTime.Now;
            donHang.ModifiedBy = (string)Session["USERNAME_SESSION"];
            db.Entry(donHang).State = EntityState.Modified;
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
