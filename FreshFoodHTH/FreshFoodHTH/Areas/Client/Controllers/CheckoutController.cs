using FreshFoodHTH.Models.DAO.Admin;
using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodHTH.Areas.Client.Controllers
{
    public class CheckoutController : Controller
    {
        FreshFoodDBContext db = new FreshFoodDBContext();
        NguoiDungDAO ndDao = new NguoiDungDAO();
        PhuongThucThanhToanDAO ptttDao = new PhuongThucThanhToanDAO();

        // GET: Client/Checkout
        public ActionResult Index(Guid id)
        {
            NguoiDung nguoidung = ndDao.GetByID(id);

            var lisPTTT = ptttDao.ListPhuongThucThanhToan();
            ViewBag.PayWayList = new SelectList(lisPTTT, "IDPhuongThucThanhToan", "TenPhuongThucThanhToan", "IDPhuongThucThanhToan");

            ViewBag.User = nguoidung;

            // listChiTietDonHang = listChiTietGioHang
            // waiting mapping data from listChiTietGioHang
            var list = db.ChiTietGioHangs.Where(x => x.IDKhachHang == id).ToList();
            
            return View(list);
        }
    }
}