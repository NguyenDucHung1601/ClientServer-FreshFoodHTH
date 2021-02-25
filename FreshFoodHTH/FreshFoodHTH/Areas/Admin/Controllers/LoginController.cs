using FreshFoodHTH.Common;
using FreshFoodHTH.Models.DAO.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodHTH.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var dao = new NguoiDungDAO();
                var result = dao.Login(collection["inputUsername"].ToString(), collection["inputPassword"].ToString());
                if (result == 1)
                {
                    var user = dao.GetById(collection["inputUsername"].ToString());
                    var userSession = new UserLogin();

                    userSession.IDNguoiDung = user.IDNguoiDung;
                    userSession.Username = user.Username;
                    userSession.Ten = user.Ten;
                    userSession.NgayTao = user.CreatedDate;

                    Session.Add(Common.CommonConstants.USER_SESSION, userSession);
                    Session.Add(Common.CommonConstants.IDUSER_SESSION, userSession.IDNguoiDung);
                    Session.Add(Common.CommonConstants.USERNAME_SESSION, userSession.Username);
                    Session.Add(Common.CommonConstants.NAME_SESSION, userSession.Ten);
                    Session.Add(Common.CommonConstants.CREATEDDATE_SESSION, userSession.NgayTao);

                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không thành công");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            Session["USER_SESSION"] = null;
            return RedirectToAction("Index");
        }
    }
}