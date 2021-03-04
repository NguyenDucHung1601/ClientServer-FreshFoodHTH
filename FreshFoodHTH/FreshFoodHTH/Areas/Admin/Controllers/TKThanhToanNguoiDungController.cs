using FreshFoodHTH.Models.DAO.Admin;
using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodHTH.Areas.Admin.Controllers
{
    public class TKThanhToanNguoiDungController : BaseController
    {
        TKThanhToanNguoiDungDAO tkttndDao = new TKThanhToanNguoiDungDAO();
        NguoiDungDAO ndDao = new NguoiDungDAO();
        TaiKhoanThanhToanDAO tkttDao = new TaiKhoanThanhToanDAO();




        // GET: Admin/SanPham
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
            ViewBag.Count = tkttndDao.ListSimple(searching).Count();
            return View(tkttndDao.ListSimpleSearch(pageNumber, pagesize, searching));
        }

        public ActionResult Details(Guid idnd, Guid idtk)
        {
            TKThanhToanNguoiDung tkthanhtoannguoidung = tkttndDao.GetByID(idnd, idtk);
            return View(tkthanhtoannguoidung);
        }

        public ActionResult Create()
        {
            List<TaiKhoanThanhToan> taikhoanthanhtoan = tkttDao.ListTaiKhoanThanhToan();
            SelectList taikhoanthanhtoanList = new SelectList(taikhoanthanhtoan, "IDTaiKhoan", "Ten", "IDTaiKhoan");
            ViewBag.TaiKhoan = taikhoanthanhtoanList;

            List<NguoiDung> nguoidung = ndDao.ListNguoiDung();
            SelectList nguoidungList = new SelectList(nguoidung, "IDNguoiDung", "Ten", "IDNguoiDung");
            ViewBag.NguoiDung = nguoidungList;


            return View();

        }

        [HttpPost]
        public ActionResult Create(string taikhoan, string Password, string tongtien, Guid idnguoidung, Guid idtaikhoan)
        {
            TKThanhToanNguoiDung tkthanhtoannguoidung = new TKThanhToanNguoiDung();
            tkthanhtoannguoidung.IDNguoiDung = idnguoidung;
            tkthanhtoannguoidung.IDTaiKhoan = idtaikhoan;
            tkthanhtoannguoidung.TaiKhoan = taikhoan;
            tkthanhtoannguoidung.Password = Password;
            tkthanhtoannguoidung.TongTien = Convert.ToDecimal(tongtien);


            if (ModelState.IsValid)
            {
                tkttndDao.Add(tkthanhtoannguoidung);
                return RedirectToAction("Index");
            }
            else
            {
                return View(tkthanhtoannguoidung);
            }
        }

        public ActionResult Edit(Guid idnd, Guid idtk)
        {

            return View(tkttndDao.GetByID(idnd,idtk));

        }   
        [HttpPost]
        public ActionResult Edit(Guid idnguoidung, Guid idtaikhoan, string taikhoan, string Password, string tongtien)
        {
            TKThanhToanNguoiDung tkthanhtoannguoidung = tkttndDao.GetByID(idnguoidung,idtaikhoan);
            tkthanhtoannguoidung.TaiKhoan = taikhoan;
            tkthanhtoannguoidung.Password = Password;
            tkthanhtoannguoidung.TongTien = Convert.ToDecimal(tongtien);

            if (ModelState.IsValid)
            {
                
                tkttndDao.Edit(tkthanhtoannguoidung);
                return RedirectToAction("Index");
            }
            else
            {
                return View(tkthanhtoannguoidung);
            }
        }

        public ActionResult Delete(Guid idnd,Guid idtk)
        {
            tkttndDao.Delete(idnd,idtk);
            return RedirectToAction("Index");
        }
    }
}