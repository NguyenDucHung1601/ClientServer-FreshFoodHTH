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
    public class NguoiDungClientController : Controller
    {
        // GET: Admin/NguoiDungClient
        FreshFoodDBContext db = new FreshFoodDBContext();
        NguoiDungDAO ndDao = new NguoiDungDAO();

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
            ViewBag.Count = ndDao.ListClientSimple(searching).Count();
            return View(ndDao.ListClientSimpleSearch(pageNumber, pagesize, searching));
        }

        public ActionResult Details(Guid id)
        {
            NguoiDung nguoidung = ndDao.GetByID(id);
            return View(nguoidung);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string username, string password, string ten, string dienthoai, string diachi, HttpPostedFileBase avatar)
        {
            NguoiDung nguoidung = new NguoiDung();
            nguoidung.IDNguoiDung = Guid.NewGuid();
            nguoidung.IsAdmin = true;
            nguoidung.IDLoaiNguoiDung = db.LoaiNguoiDungs.Where(x => x.Ten.Equals("Admin")).Select(x => x.IDLoaiNguoiDung).ToList().ElementAt(0);
            nguoidung.CreatedDate = DateTime.Now;
            nguoidung.ModifiedDate = DateTime.Now;
            nguoidung.Username = username;
            nguoidung.Password = BCrypt.Net.BCrypt.HashPassword(password);
            nguoidung.Ten = ten;
            nguoidung.DienThoai = dienthoai;
            nguoidung.DiaChi = diachi;

            if (ModelState.IsValid)
            {
                if (avatar != null && avatar.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(avatar.FileName));
                    avatar.SaveAs(path);
                    nguoidung.Avatar = avatar.FileName;
                }
                ndDao.AddAdmin(nguoidung);
                return RedirectToAction("Index");
            }
            else
            {
                return View(nguoidung);
            }
        }

        public ActionResult Edit(Guid id)
        {
            return View(ndDao.GetByID(id));
        }

        [HttpPost]
        public ActionResult Edit(Guid id, string username, string password, string ten, string dienthoai, string diachi, HttpPostedFileBase avatar)
        {
            NguoiDung nguoidung = ndDao.GetByID(id);
            nguoidung.ModifiedDate = DateTime.Now;
            nguoidung.Username = username;
            nguoidung.Password = password;
            nguoidung.Ten = ten;
            nguoidung.DienThoai = dienthoai;
            nguoidung.DiaChi = diachi;

            if (ModelState.IsValid)
            {
                if (avatar != null && avatar.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(avatar.FileName));
                    avatar.SaveAs(path);
                    nguoidung.Avatar = avatar.FileName;
                }
                ndDao.EditAdmin(nguoidung);
                return RedirectToAction("Index");
            }
            else
            {
                return View(nguoidung);
            }
        }

        public ActionResult Delete(Guid id)
        {
            ndDao.DeleteAdmin(id);
            return RedirectToAction("Index");
        }
    }
}