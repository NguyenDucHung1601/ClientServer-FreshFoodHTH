using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodHTH.Models.DAO.Client
{
    public class ThanhToanDAO
    {
        //TKThanhToanNguoiDungDAO tkttndDao = new TKThanhToanNguoiDungDAO();
        FreshFoodDBContext db;

        public ThanhToanDAO()
        {
            db = new FreshFoodDBContext();
        }

        public List<TKThanhToanNguoiDung> ListTKThanhToanNguoiDung()
        {
            return db.TKThanhToanNguoiDungs.ToList();
        }

        public TKThanhToanNguoiDung GetByID(Guid idnd, Guid idtk)
        {
            return db.TKThanhToanNguoiDungs.Where(x => x.IDNguoiDung == idnd).Where(x => x.IDTaiKhoan == idtk).ToList().ElementAt(0);
        }

        public string GetIdentity()
        {
            int idOrder = Convert.ToInt32(db.Database.SqlQuery<decimal>("SELECT IDENT_CURRENT('TKThanhToanNguoiDung')").FirstOrDefault());
            return idOrder.ToString("D4");
        }

        public void Add(TKThanhToanNguoiDung obj)
        {
            db.TKThanhToanNguoiDungs.Add(obj);
            db.SaveChanges();
        }
        public int KTTKHOPLE(TKThanhToanNguoiDung obj, string pass)
        {
            TKThanhToanNguoiDung taikhoanthanhtoannguoidung = GetByID(obj.IDNguoiDung, obj.IDTaiKhoan);
            DonHang donhang = db.DonHangs.Where(x => x.IDKhachHang == obj.IDNguoiDung).ElementAt(0);
            if (BCrypt.Net.BCrypt.Verify(pass, taikhoanthanhtoannguoidung.Password))
            {


                if (taikhoanthanhtoannguoidung.TongTien >= donhang.TongTien)

                    // view thanh toan thanh cong// cap nhat lai tien trong gio hang
                    taikhoanthanhtoannguoidung.TongTien = taikhoanthanhtoannguoidung.TongTien - donhang.TongTien;
                return 1;
                //else return tai khoan k du tien
            }
            return -1;// view sai mat khau
        }
        public int Delete(Guid idnd, Guid idtk)
        {
            TKThanhToanNguoiDung tkthanhtoannguoidung = db.TKThanhToanNguoiDungs.Where(x => x.IDNguoiDung == idnd).Where(x => x.IDTaiKhoan == idtk).ToList().ElementAt(0);
            if (tkthanhtoannguoidung != null)
            {
                db.TKThanhToanNguoiDungs.Remove(tkthanhtoannguoidung);
                return db.SaveChanges();
            }
            else
                return -1;
        }
    }
}