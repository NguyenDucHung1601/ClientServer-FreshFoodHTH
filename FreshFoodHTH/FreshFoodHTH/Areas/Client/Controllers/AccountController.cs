using FreshFoodHTH.Models.DAO.Admin;
using FreshFoodHTH.Models.DAO.Client;
using FreshFoodHTH.Models.EF;
using FreshFoodHTH.Models.EFplus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodHTH.Areas.Client.Controllers
{
    public class AccountController : BaseController
    {
        FreshFoodDBContext db = new FreshFoodDBContext();
        NguoiDungDAO ndDao = new NguoiDungDAO();
        DonHangDAO dhDao = new DonHangDAO();
        ClientDonHangDAO cdhDao = new ClientDonHangDAO();
        // GET: Client/Account
        public ActionResult Index(Guid? id, int? page, int? PageSize, string searching = "")
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
            return View(dhDao.ListSimpleSearch(pageNumber, pagesize, searching));
        }

        public ActionResult Edit(Guid id)
        {
            return View(ndDao.GetByID(id));
        }

        [HttpPost]
        public ActionResult Edit(Guid id, string ten, string dienthoai, string email, string diachi, HttpPostedFileBase avatar)
        {
            NguoiDung nguoidung = ndDao.GetByID(id);
            nguoidung.Ten = ten;
            nguoidung.DienThoai = dienthoai;
            nguoidung.Email = email;
            nguoidung.DiaChi = diachi;

            nguoidung.ModifiedDate = DateTime.Now;
            nguoidung.ModifiedBy = (string)Session["USERNAME_SESSION"];

            if (ModelState.IsValid)
            {
                if (avatar != null && avatar.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(avatar.FileName));
                    avatar.SaveAs(path);
                    nguoidung.Avatar = avatar.FileName;
                }
                ndDao.Edit(nguoidung);

                if (Session["USER_SESSION"] != null)
                {
                    Session["AVATAR_SESSION"] = nguoidung.Avatar;
                    Session["NAME_SESSION"] = nguoidung.Ten;
                }    

                return RedirectToAction("Index", "Account", new { id = Session["IDUSER_SESSION"] });
            }
            else
            {
                return View(nguoidung);
            }
        }

        public ActionResult ChangePassword(Guid id)
        {
            flatChangePassword obj = new flatChangePassword() { IDUser = id, Username = (ndDao.GetByID(id)).Username };
            return View(obj);
        }

        [HttpPost]
        public ActionResult ChangePassword(Guid id, string oldpass, string newpass, string confirm)
        {
            NguoiDung nguoidung = ndDao.GetByID(id);
            flatChangePassword userchange = new flatChangePassword() { IDUser = id, Username = (ndDao.GetByID(id)).Username, OldPass = oldpass, NewPass = newpass, Confirm = confirm };

            nguoidung.ModifiedDate = DateTime.Now;
            nguoidung.ModifiedBy = (string)Session["USERNAME_SESSION"];


            if (ModelState.IsValid)
            {
                if (BCrypt.Net.BCrypt.Verify(oldpass, nguoidung.Password))
                {
                    ViewBag.OldPassword = string.Empty;
                    if (BCrypt.Net.BCrypt.Verify(newpass, nguoidung.Password))
                        ViewBag.NewPassword = "** Mật khẩu mới phải khác mật khẩu cũ";
                    else
                    {
                        ViewBag.NewPassword = string.Empty;
                        if (!newpass.Equals(confirm))
                            ViewBag.ConfirmPassword = "** Mật khẩu mới và xác nhận mật khẩu không khớp";
                        else
                        {
                            ViewBag.ConfirmPassword = string.Empty;
                            ndDao.ChangePassword(nguoidung, newpass);
                            return RedirectToAction("Index", new { id = nguoidung.IDNguoiDung });
                        }
                    }
                    return View(userchange);
                }
                ViewBag.OldPassword = "** Mật khẩu cũ không đúng";
                return View(userchange);
            }
            else
            {
                return View(userchange);
            }
        }

        public ActionResult Accept(Guid id)
        {
            var donHang = db.DonHangs.Find(id);
            cdhDao.XacNhanDaNhanHang(donHang);
            return RedirectToAction("Index");
        }
    }
}