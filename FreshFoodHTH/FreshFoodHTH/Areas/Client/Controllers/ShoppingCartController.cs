using FreshFoodHTH.Models.DAO.Client;
using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FreshFoodHTH.Common;
using FreshFoodHTH.Models.DAO.Admin;
using System.Data.Entity;
namespace FreshFoodHTH.Areas.Client.Controllers
{
    public class ShoppingCartController : BaseController
    {
        FreshFoodDBContext db = new FreshFoodDBContext();
        GioHangDAO ghDao = new GioHangDAO();
        SanPhamDAO spDao = new SanPhamDAO();
        NguoiDungDAO ndDao = new NguoiDungDAO();
        // GET: Client/ShoppingCart
        public ActionResult Index(Guid id)
        {
            var list = db.ChiTietGioHangs.Where(x => x.IDKhachHang == id).OrderByDescending(x => x.ModifiedDate).ToList();
            return View(list);
        }

        [HttpPost]
        public ActionResult Index(FormCollection c)
        {
            Guid idkhachhang = Guid.NewGuid();
            int i = 0;
            if (ModelState.IsValid)
            {
                var userIDCTGioHang = c.GetValues("item.IDChiTietGioHang");
                var userSoLuong = c.GetValues("item.SoLuong");

                for (i = 0; i < userIDCTGioHang.Count(); i++)
                {
                    string idCTGioHang = userIDCTGioHang[i];
                    ChiTietGioHang obj = db.ChiTietGioHangs.Where(x => x.IDChiTietGioHang.ToString() == idCTGioHang).SingleOrDefault();

                    if (obj.SoLuong != Convert.ToInt32(userSoLuong[i]))
                    {
                        obj.ModifiedDate = DateTime.Now;
                        obj.ModifiedBy = (string)Session["USERNAME_SESSION"];
                    }

                    obj.SoLuong = Convert.ToInt32(userSoLuong[i]);
                    obj.ThanhTien = obj.SoLuong * obj.SanPham.GiaKhuyenMai;

                    idkhachhang = obj.IDKhachHang;

                    ghDao.Edit(obj);
                    db.Entry(obj).State = EntityState.Modified;


                    if (obj.SoLuong == 0)
                    {
                        NguoiDung nguoidung = db.NguoiDungs.Find(obj.IDKhachHang);
                        nguoidung.TongTienGioHang -= obj.ThanhTien;
                        db.ChiTietGioHangs.Remove(obj);
                    }
                }
                db.SaveChanges();
            }
            var list = db.ChiTietGioHangs.Where(x => x.IDKhachHang == idkhachhang).OrderByDescending(x => x.ModifiedDate).ToList();
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

            List<ChiTietGioHang> listCTGioHang = ghDao.GetListChiTietGioHangByIDKhachHang((Guid)Session["IDUSER_SESSION"]);
            foreach (var item in listCTGioHang)
            {
                if (obj.IDSanPham == item.IDSanPham)
                {
                    obj.IDChiTietGioHang = item.IDChiTietGioHang;
                    obj.SoLuong = item.SoLuong + obj.SoLuong;
                    obj.ThanhTien = obj.SoLuong * (db.SanPhams.Find(productId)).GiaKhuyenMai;
                    if (ghDao.KTGIOHANG(obj))
                    {
                        ghDao.Edit(obj);
                        return RedirectToAction("Index", "ShoppingCart", new { id = Session["IDUSER_SESSION"] });
                    }
                    else
                    {
                        // fail ...
                    }
                }
            }

            if (ghDao.KTGIOHANG(obj))
            {
                ghDao.Add(obj);
            }
            else
            {
                // fail ...
            }
            return RedirectToAction("Index", "ShoppingCart", new { id = Session["IDUSER_SESSION"] });
        }
        public ActionResult DeleteItem(Guid id)
        {
            ChiTietGioHang obj = db.ChiTietGioHangs.Find(id);
            NguoiDung nguoidung = db.NguoiDungs.Find(obj.IDKhachHang);
            nguoidung.TongTienGioHang -= obj.ThanhTien;
            db.ChiTietGioHangs.Remove(obj);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = Session["IDUSER_SESSION"] });
        }
    }
}