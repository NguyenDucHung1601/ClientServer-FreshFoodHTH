using FreshFoodHTH.Common;
using FreshFoodHTH.Models.DAO.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodHTH.Areas.Client.Controllers
{
    public class LoginController : Controller
    {
        // GET: Client/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var dao = new NguoiDungDAO();
                var result = dao.LoginClient(collection["inputUsername"].ToString(), collection["inputPassword"].ToString());
                if (result == 1)
                {
                    var user = dao.GetByUsername(collection["inputUsername"].ToString());
                    var userSession = new UserLogin();

                    userSession.IDNguoiDung = user.IDNguoiDung;
                    userSession.Username = user.Username;
                    userSession.Avatar = user.Avatar;
                    userSession.Ten = user.Ten;
                    userSession.NgayTao = user.CreatedDate;

                    Session.Add(Common.CommonConstants.USER_SESSION, userSession);
                    Session.Add(Common.CommonConstants.IDUSER_SESSION, userSession.IDNguoiDung);
                    Session.Add(Common.CommonConstants.USERNAME_SESSION, userSession.Username);
                    Session.Add(Common.CommonConstants.AVATAR_SESSION, userSession.Avatar);
                    Session.Add(Common.CommonConstants.NAME_SESSION, userSession.Ten);
                    Session.Add(Common.CommonConstants.CREATEDDATE_SESSION, userSession.NgayTao);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Username hoặc password không đúng");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            Session["USER_SESSION"] = null;
            Session["AVATAR_SESSION"] = null;
            Session["NAME_SESSION"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}