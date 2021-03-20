using FreshFoodHTH.Models.DAO.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodHTH.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: Admin/Dashboard
        public DonHangDAO dhDao = new DonHangDAO();
        public HoaDonNhapDAO hdnDao = new HoaDonNhapDAO();
        public NguoiDungDAO ndDao = new NguoiDungDAO();

        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            //Tính theo khoảng thời gian từ đầu tháng -> hiện tại
            ViewBag.TongHoaDonNhap = hdnDao.TongHoaDonNhap(DateTime.Today.AddDays((-DateTime.Today.Day + 1)), DateTime.Today.AddDays(1));
            ViewBag.TongDonHang = dhDao.TongDonHang(DateTime.Today.AddDays((-DateTime.Today.Day + 1)), DateTime.Today.AddDays(1));
            ViewBag.DoanhThu = dhDao.DoanhThu(DateTime.Today.AddDays((-DateTime.Today.Day + 1)), DateTime.Today.AddDays(1));
            ViewBag.TongSoThanhVien = ndDao.TongSoThanhVien();
            return View();
        }
    }
}