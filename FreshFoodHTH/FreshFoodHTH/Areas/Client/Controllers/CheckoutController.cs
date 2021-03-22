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
        static string maGiamGia;
        static Guid idTKTTND;
        static decimal TienTTOL;
        // GET: Client/Checkout
        public ActionResult Index(Guid id, string? idpttt = "af8ad04b-0200-4e0b-ba12-ef94175af1d9")
        {
            NguoiDung nguoidung = ndDao.GetByID(id);
            ViewBag.User = nguoidung;
            var route = RouteData.Values["magiamgia"] + Request.Url.Query;
            string mgg = route.Split('&').First().Split('=').Last().ToString();

            var lisPTTT = ptttDao.ListPhuongThucThanhToan();
            ViewBag.PayWayList = new SelectList(lisPTTT, "IDPhuongThucThanhToan", "TenPhuongThucThanhToan", "IDPhuongThucThanhToan");

            decimal TienGiam = 0;
            if (idpttt == "0cea7cb1-b4d5-496c-a2e8-2e0c7386079e")
            {
                mgg = maGiamGia;
            }
            ViewBag.mgg = mgg;
            // Áp dụng giảm giá
            if (mgg != "" && mgg != null)
            {
                if (cdhDao.KiemTraDoiTuongApDung(mgg, nguoidung))
                {
                    TienGiam = db.MaGiamGias.SingleOrDefault(x => x.MaGiamGia1 == mgg).TienGiam;
                    maGiamGia = mgg;
                }
            }
            //create donHang
            var lstctgh = db.ChiTietGioHangs.Where(x => x.IDKhachHang == id).ToList();

            var donHang = new DonHang();
            donHang.TenNhanHang = nguoidung.Ten;
            donHang.DiaChiNhanHang = nguoidung.DiaChi;
            donHang.SdtNhanHang = nguoidung.DienThoai;
            donHang.IDDonHang = Guid.NewGuid();
            donHang.CreatedDate = DateTime.Now;
            donHang.CreatedBy = (string)Session["USERNAME_SESSION"];
            donHang.ModifiedDate = DateTime.Now;
            donHang.ModifiedBy = (string)Session["USERNAME_SESSION"];
            donHang.TienHang = nguoidung.TongTienGioHang;
            if (donHang.TienHang >= 100000)
            {
                donHang.TienShip = 0;
            }
            else
            {
                donHang.TienShip = 30000;
            }
            donHang.TienGiam = TienGiam;
            donHang.TongTien = nguoidung.TongTienGioHang + donHang.TienShip - donHang.TienGiam;
            donHang.IDKhachHang = id;
            donHang.IDTrangThai = new Guid("5404ec28-c908-48b1-a7e5-e5a366b51d5a"); // chờ xác nhận
            donHang.IDPhuongThucThanhToan = new Guid(idpttt);

            if (idpttt == "0cea7cb1-b4d5-496c-a2e8-2e0c7386079e")
            {
                ViewBag.DaThanhToan = donHang.TongTien;
                TienTTOL = (decimal)donHang.TongTien;
                donHang.TongTien = 0;
            }
            else
            {
                TienTTOL = 0;
                ViewBag.DaThanhToan = 0;
            }
            donHangAdd = donHang;
            ViewBag.donhang = donHang;
            //mapper ChiTietGioHang => ChiTietDonHang
            var lstResult = cdhDao.CapNhatTongTienDonHangSoBo(lstctgh);
            lstCTDH = lstResult;
            return View(lstResult);
        }

        public ActionResult XacNhanThanhToan(Guid id)
        {
            var idnd = (Guid)Session["IDUSER_SESSION"];
            if(idTKTTND != null && idTKTTND != Guid.Empty)
            {
                var TKTTnguoiDung = db.TKThanhToanNguoiDungs.SingleOrDefault(x => x.IDTaiKhoan == idTKTTND & x.IDNguoiDung == idnd);
                TKTTnguoiDung.TongTien = TKTTnguoiDung.TongTien - TienTTOL;
                db.SaveChanges();
            }    
            
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
        }

        public ActionResult LoginTaiKhoanThanhToan(Guid idtk, Guid idnd)
        {
            TKThanhToanNguoiDung tkthanhtoan = db.TKThanhToanNguoiDungs.Where(x => x.IDTaiKhoan == idtk && x.IDNguoiDung == idnd).SingleOrDefault();
            ViewBag.TongTienDonHang = donHangAdd.TongTien;
            return View(tkthanhtoan);
        }

        public ActionResult CheckLoginTaiKhoanThanhToan(Guid idtktt, FormCollection collection)
        {
            // check password
            var idnd = (Guid)Session["IDUSER_SESSION"];
            var taiKhoanTTND = db.TKThanhToanNguoiDungs.SingleOrDefault(x => x.IDNguoiDung == idnd & x.IDTaiKhoan == idtktt);
            if (BCrypt.Net.BCrypt.Verify(collection["inputPassword"].ToString(), taiKhoanTTND.Password))
            {
                idTKTTND = idtktt;
                return RedirectToAction("Index", "Checkout", new { id = idnd, idpttt = "0cea7cb1-b4d5-496c-a2e8-2e0c7386079e" });
            }
            return RedirectToAction("LoginTaiKhoanThanhToan", "Checkout", new { idtk = idtktt, idnd = idnd });
        }
    }
}