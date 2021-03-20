using FreshFoodHTH.Models.DAO.Admin;
using FreshFoodHTH.Models.DAO.Client;
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
        ClientDonHangDAO cdhDao = new ClientDonHangDAO();
        DonHangDAO dhDao = new DonHangDAO();

        static DonHang donHangAdd;
        static List<ChiTietDonHang> lstCTDH;

        // GET: Client/Checkout
        public ActionResult Index(Guid id)
        {
            NguoiDung nguoidung = ndDao.GetByID(id);
            ViewBag.User = nguoidung;

            var res = RouteData.Values["magiamgia"] + Request.Url.Query;
            string mgg = res.Split('&').First().Split('=').Last().ToString();

            var lisPTTT = ptttDao.ListPhuongThucThanhToan();
            ViewBag.PayWayList = new SelectList(lisPTTT, "IDPhuongThucThanhToan", "TenPhuongThucThanhToan", "IDPhuongThucThanhToan");

            decimal TienGiam = 0;
            ViewBag.mgg = mgg;
            // Áp dụng giảm giá
            if (mgg != "" && mgg != null)
            {
                if (cdhDao.KiemTraDoiTuongApDung(mgg, nguoidung))
                {
                    TienGiam = db.MaGiamGias.SingleOrDefault(x => x.MaGiamGia1 == mgg).TienGiam;
                }
            }
            //create donHang
            var lstctgh = db.ChiTietGioHangs.Where(x => x.IDKhachHang == id).ToList();

            var donHang = new DonHang();
            donHang.IDDonHang = Guid.NewGuid();
            donHang.CreatedDate = DateTime.Now;
            donHang.CreatedBy = (string)Session["USERNAME_SESSION"];
            donHang.ModifiedDate = DateTime.Now;
            donHang.ModifiedBy = (string)Session["USERNAME_SESSION"];
            donHang.TienHang = nguoidung.TongTienGioHang;
            donHang.TienShip = 30000;
            donHang.TienGiam = TienGiam;
            donHang.TongTien = nguoidung.TongTienGioHang + donHang.TienShip - donHang.TienGiam;
            donHang.IDKhachHang = id;
            donHang.IDTrangThai = new Guid("5404ec28-c908-48b1-a7e5-e5a366b51d5a"); // chờ xác nhận
            donHang.IDPhuongThucThanhToan = new Guid("af8ad04b-0200-4e0b-ba12-ef94175af1d9"); // tiền mặt
            ViewBag.donhang = donHang;
            donHangAdd = donHang;
            var lstResult = cdhDao.CapNhatTongTienDonHangSoBo(donHang, lstctgh);
            //donHang.ChiTietDonHangs = lstResult;
            lstCTDH = lstResult;
            return View(lstResult);
        }

        public ActionResult XacNhanThanhToan(Guid id)
        {
            var res = donHangAdd;
            //tạo đơn hàng mới;
            db.DonHangs.Add(res);
            db.SaveChanges();
            foreach (ChiTietDonHang item in lstCTDH)
            {
                var ctdh = new ChiTietDonHang();
                ctdh.IDChiTietDonHang = Guid.NewGuid();
                ctdh.IDDonHang = donHangAdd.IDDonHang;
                ctdh.IDSanPham = item.IDSanPham;
                ctdh.DonGiaBan = db.SanPhams.SingleOrDefault(x => x.IDSanPham == item.IDSanPham).GiaKhuyenMai;
                ctdh.SoLuong = item.SoLuong;
                ctdh.ThanhTien = item.ThanhTien;
                ctdh.CreatedDate = DateTime.Now;
                ctdh.CreatedBy = (string)Session["USERNAME_SESSION"];
                ctdh.ModifiedDate = DateTime.Now;
                ctdh.ModifiedBy = (string)Session["USERNAME_SESSION"];
                db.ChiTietDonHangs.Add(ctdh);
                db.SaveChanges();
            }
            cdhDao.XacNhanDonHang(id);
            return RedirectToAction("Index", "Home");
            //return RedirectToAction("Details", "HoaDonNhap", new { id = IDcurHoaDonNhap });
        }
    }
}