using FreshFoodHTH.Models.DAO.Admin;
using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace FreshFoodHTH.Models.DAO.Client
{
    public class ClientDonHangDAO
    {
        FreshFoodDBContext db = null;
        public ClientDonHangDAO()
        {
            db = new FreshFoodDBContext();
        }

        /// <summary>
        ///  Tính tổng tiền đơn hàng sơ bộ
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="dh"></param>
        public void CapNhatTongTienGioHangSoBo(Guid idND)
        {
            var lstctgh = db.ChiTietGioHangs.Where(x => x.IDKhachHang == idND).ToList();

            // cập nhật thành tiền
            foreach (ChiTietGioHang item in lstctgh)
            {
                item.ThanhTien = item.SanPham.GiaTien * item.SoLuong;
            }
            // cập nhật tổng tiền
            var khachHang = db.NguoiDungs.SingleOrDefault(x => x.IDNguoiDung == idND);
            khachHang.TongTienGioHang = lstctgh.Sum(x => x.ThanhTien);

            db.SaveChanges();
        }


        public List<ChiTietDonHang> CapNhatTongTienDonHangSoBo(List<ChiTietGioHang> lstctgh)
        {
            // Chuyển các sản phẩm trong giỏ hàng => đơn hàng
            //Initialize the mapper
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<ChiTietGioHang, ChiTietDonHang>()
                    .ForMember(dest => dest.IDSanPham, act => act.MapFrom(src => src.IDSanPham))
                    .ForMember(dest => dest.SoLuong, act => act.MapFrom(src => src.SoLuong))
                    .ForMember(dest => dest.ThanhTien, act => act.MapFrom(src => src.ThanhTien))
                );
            var mapper = config.CreateMapper();
            var lstctdh = lstctgh.Select
                          (
                            item => mapper.Map<ChiTietGioHang, ChiTietDonHang>(item)
                          ).ToList();
            return lstctdh;
        }
       
        /// <summary>
        /// Kiểm tra mã giảm giá áp dụng
        /// </summary>
        /// <param name="mgg"> mã giảm giá</param>
        /// <returns></returns>
        public bool KiemTraMaGiamGia(string mgg)
        {
            var result = db.MaGiamGias.Any(x => x.MaGiamGia1 == mgg);
            if (result)
            {
                var res = db.MaGiamGias.SingleOrDefault(x => x.MaGiamGia1 == mgg);
                if (res.HanSuDung >= DateTime.Now)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        
        public bool KiemTraDoiTuongApDung(string mgg, NguoiDung kh)
        {
            if (KiemTraMaGiamGia(mgg))
            {
                var mggObj = db.MaGiamGias.SingleOrDefault(x => x.MaGiamGia1 == mgg);
                var res = db.PhanLoaiKhachHangs.SingleOrDefault(x => x.IDLoaiKhachHang == kh.IDLoaiKhachHang);
                if (mggObj.PhanLoaiKhachHang.CapDo == res.CapDo)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        /// <summary>
        /// Xác nhận thanh toán đơn hàng
        /// </summary>
        /// <param name="dh"></param>
        public void XacNhanThanhToan(DonHang dh)
        {
            var donHang = db.DonHangs.SingleOrDefault(x => x.IDDonHang == dh.IDDonHang);
            // trạng thái chờ xác nhận
            donHang.IDTrangThai = new Guid("5404ec28-c908-48b1-a7e5-e5a366b51d5a");
            db.SaveChanges();
        }


        // Xác nhận đã nhận hàng
        public void XacNhanDaNhanHang(DonHang dh)
        {
            var donHang = db.DonHangs.SingleOrDefault(x => x.IDDonHang == dh.IDDonHang);
            // trạng thái đã giao hàng
            donHang.IDTrangThai = new Guid("e970c371-b124-49a4-ae54-781f8f848797");
            db.SaveChanges();
        }

        public void XacNhanDonHang(Guid id)
        {   
            var donHang = db.DonHangs.Find(id);

            // cập nhật số lượng & số lượt mua
            var lstctDonHang = db.ChiTietDonHangs.Where(x => x.IDDonHang == donHang.IDDonHang).ToList();

            foreach (ChiTietDonHang item in lstctDonHang)
            {
                item.SanPham.SoLuong = item.SanPham.SoLuong - item.SoLuong;
                item.SanPham.SoLuotMua = item.SanPham.SoLuotMua + 1;
                CapNhatGioHang(donHang.IDKhachHang);
            }
            // cật nhật số đơn hàng và tổng tiền hàng đã mua
            var khachHang = db.NguoiDungs.SingleOrDefault(x => x.IDNguoiDung == donHang.IDKhachHang);
            khachHang.SoDonHangDaMua = khachHang.SoDonHangDaMua + 1;
            khachHang.TongTienHangDaMua = khachHang.TongTienHangDaMua + donHang.TongTien;

            CapNhatTongTienGioHang(khachHang);

            db.SaveChanges();
        }

        // Xóa các sản phẩm đã mua trong giỏ hàng và cập nhật tổng tiền giỏ hàng
        public void CapNhatGioHang(Guid id)
        {
            var res =  db.NguoiDungs.SingleOrDefault(x => x.IDNguoiDung == id).ChiTietGioHangs;
            db.ChiTietGioHangs.RemoveRange(res);
            db.SaveChanges();
        }

        public void CapNhatTongTienGioHang(NguoiDung kh)
        {
            var lstctgh = db.ChiTietGioHangs.Where(x => x.IDKhachHang == kh.IDNguoiDung).ToList();

            // cập nhật thành tiền
            foreach (ChiTietGioHang item in lstctgh)
            {
                item.ThanhTien = item.SanPham.GiaTien * item.SoLuong;
            }
            // cập nhật tổng tiền
            var khachHang = db.NguoiDungs.SingleOrDefault(x => x.IDNguoiDung == kh.IDNguoiDung);
            khachHang.TongTienGioHang = lstctgh.Sum(x => x.ThanhTien);

            db.SaveChanges();
        }
    }
}