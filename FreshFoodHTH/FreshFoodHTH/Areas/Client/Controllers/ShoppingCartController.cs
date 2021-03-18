using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FreshFoodHTH.Common;
using FreshFoodHTH.Models.DAO.Client;
using FreshFoodHTH.Models.DAO.Admin;

namespace FreshFoodHTH.Areas.Client.Controllers
{
    public class ShoppingCartController : BaseController
    {
        FreshFoodDBContext db = new FreshFoodDBContext();
        GioHangDAO giohangDao = new GioHangDAO();
        SanPhamDAO spDao = new SanPhamDAO();
        NguoiDungDAO ndDao = new NguoiDungDAO();
        // GET: Client/ShoppingCart
        public ActionResult Index(Guid id)
        {
            var list = db.ChiTietGioHangs.Where(x => x.IDKhachHang == id).OrderByDescending(x=>x.CreatedDate).ToList();
            return View(list);
        }
        
        public ActionResult AddItem(Guid productId, int quantity)
        {
            ChiTietGioHang obj = new ChiTietGioHang();
            obj.IDChiTietGioHang = Guid.NewGuid();
            obj.IDSanPham = productId;
            obj.IDKhachHang = (Guid)Session["IDUSER_SESSION"];
            obj.SoLuong = quantity;
            obj.DuocChon = false;
            obj.ThanhTien = obj.SoLuong * (db.SanPhams.Find(productId)).GiaKhuyenMai;
            obj.CreatedDate = DateTime.Now;
            obj.CreatedBy = (string)Session["USERNAME_SESSION"];
            obj.ModifiedDate = DateTime.Now;
            obj.ModifiedBy = (string)Session["USERNAME_SESSION"];

            if (giohangDao.KTGIOHANG(obj))
                giohangDao.Add(obj);
            return RedirectToAction("Index", "ShoppingCart", new { id = Session["IDUSER_SESSION"] }) ;
        }
        public ActionResult DeleteCartDetail(Guid id)
        {
            ChiTietGioHang obj = db.ChiTietGioHangs.Find(id);
            NguoiDung nguoidung = db.NguoiDungs.Where(x => x.IDNguoiDung == obj.IDKhachHang).SingleOrDefault();
            nguoidung.TongTienGioHang -= obj.ThanhTien;
            db.ChiTietGioHangs.Remove(obj);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = Session["IDUSER_SESSION"] });
        }

        public ActionResult UpdateCart()
        {
            giohangDao.CNGioHang((Guid)Session["IDUSER_SESSION"]);
            return RedirectToAction("Index", "ShoppingCart", new { id = Session["IDUSER_SESSION"] });
        }
    }
}