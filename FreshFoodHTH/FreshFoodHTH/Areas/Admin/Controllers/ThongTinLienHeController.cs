using FreshFoodHTH.Models.DAO.Admin;
using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodHTH.Areas.Admin.Controllers
{
    public class ThongTinLienHeController : BaseController
    {
        ThongTinLienHeDAO ttlhDao = new ThongTinLienHeDAO();
        
        public ActionResult Index()
        {
            ThongTinLienHe info = ttlhDao.GetInfoObj();
            return View(info);
        }
        public ActionResult Edit()
        {
            ThongTinLienHe info = ttlhDao.GetInfoObj();
            return View(info);
        }
        [HttpPost]
        public ActionResult Edit(ThongTinLienHe info, string tencuahang, string diachi, string dienthoai1, string dienthoai2, string giomocua, string email,string facebook, string youtube, string instagram)
        {
            info.TenCuaHang = tencuahang;
            info.DiaChi = diachi;
            info.DienThoai1 = dienthoai1;
            info.DienThoai2 = dienthoai2;
            info.GioMoCua = giomocua;
            info.Email = email;
            info.LinkFacebook = facebook;
            info.LinkYoutube = youtube;
            info.LinkInstagram = instagram;
            if (ModelState.IsValid)
            {
                ttlhDao.Edit(info);
                return RedirectToAction("Index");
            }
            else
            {
                return View(info);
            }
        }
    }
}