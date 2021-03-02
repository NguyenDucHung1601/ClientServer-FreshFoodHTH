using FreshFoodHTH.Models.EF;
using FreshFoodHTH.Models.EFplus;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodHTH.Models.DAO.Admin
{
    public class NguoiDungDAO
    {
        FreshFoodDBContext db = null;
        public NguoiDungDAO()
        {
            db = new FreshFoodDBContext();
        }

        // Login
        public NguoiDung GetByID(Guid id)
        {
            return db.NguoiDungs.Find(id);
        }

        public NguoiDung GetByUsername(string username)
        {
            return db.NguoiDungs.SingleOrDefault(x => x.Username == username);
        }

        public int Login(string username, string password)
        {
            var result = db.NguoiDungs.SingleOrDefault(x => x.Username == username);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (BCrypt.Net.BCrypt.Verify(password, result.Password))
                    return 1;
                else
                    return -1;
            }
        }

        public int LoginAdmin(string username, string password)
        {
            var result = db.NguoiDungs.SingleOrDefault(x => x.Username == username);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.IsAdmin && BCrypt.Net.BCrypt.Verify(password, result.Password))
                    return 1;
                else
                    return -1;
            }
        }

        public int LoginClient(string username, string password)
        {
            var result = db.NguoiDungs.SingleOrDefault(x => x.Username == username);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (!result.IsAdmin && BCrypt.Net.BCrypt.Verify(password, result.Password))
                    return 1;
                else
                    return -1;
            }
        }

        // NguoiDungAdmin
        public List<NguoiDung> ListNguoiDungAdmin()
        {
            return db.NguoiDungs.Where(x => x.IsAdmin == true).ToList();
        }

        public void AddAdmin(NguoiDung obj)
        {
            db.NguoiDungs.Add(obj);
            db.SaveChanges();
        }

        public void EditAdmin(NguoiDung obj)
        {
            NguoiDung nguoidung = GetByID(obj.IDNguoiDung);
            if (nguoidung != null)
            {
                nguoidung.Ten = obj.Ten;
                nguoidung.DienThoai = obj.DienThoai;
                nguoidung.DiaChi = obj.DiaChi;
                nguoidung.Username = obj.Username;
                nguoidung.Password = obj.Password;
                nguoidung.Avatar = obj.Avatar;
                db.SaveChanges();
            }
        }

        public int DeleteAdmin(Guid id)
        {
            NguoiDung nguoidung= db.NguoiDungs.Find(id);
            if (nguoidung != null)
            {
                db.NguoiDungs.Remove(nguoidung);
                return db.SaveChanges();
            }
            else
                return -1;
        }

        public IEnumerable<NguoiDung> ListAdminSimple(string searching)
        {
            var list = db.Database.SqlQuery<NguoiDung>($"SELECT * FROM dbo.NguoiDung nd " +
                $"WHERE nd.IsAdmin = 1 " +
                $"AND (nd.Username LIKE '%{searching}%' " +
                $"OR nd.Ten LIKE N'%{searching}%' " +
                $"OR nd.DienThoai LIKE '%{searching}%' " +
                $"OR nd.DiaChi LIKE N'%{searching}%')").ToList();

            return list;
        }

        public IEnumerable<NguoiDung> ListAdminSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = db.Database.SqlQuery<NguoiDung>($"SELECT * FROM dbo.NguoiDung nd " +
               $"WHERE nd.IsAdmin = 1 " +
               $"AND (nd.Username LIKE '%{searching}%' " +
               $"OR nd.Ten LIKE N'%{searching}%' " +
               $"OR nd.DienThoai LIKE '%{searching}%' " +
               $"OR nd.DiaChi LIKE N'%{searching}%')").ToPagedList<NguoiDung>(PageNum, PageSize);

            return list;
        }


        // NguoiDungClient
        public List<NguoiDung> ListNguoiDungClient()
        {
            return db.NguoiDungs.Where(x => x.IsAdmin == false).ToList();
        }

        public void AddClient(NguoiDung obj)
        {
            db.NguoiDungs.Add(obj);
            db.SaveChanges();
        }

        public void EditClient(NguoiDung obj)
        {
            NguoiDung nguoidung = GetByID(obj.IDNguoiDung);
            if (nguoidung != null)
            {
                nguoidung.Ten = obj.Ten;
                nguoidung.DienThoai = obj.DienThoai;
                nguoidung.DiaChi = obj.DiaChi;
                nguoidung.Username = obj.Username;
                nguoidung.Password = obj.Password;
                nguoidung.Avatar = obj.Avatar;
                db.SaveChanges();
            }
        }

        public int DeleteClient(Guid id)
        {
            NguoiDung nguoidung = db.NguoiDungs.Find(id);
            if (nguoidung != null)
            {
                db.NguoiDungs.Remove(nguoidung);
                return db.SaveChanges();
            }
            else
                return -1;
        }

        public IEnumerable<flatKhachHang> ListClientSimple(string searching)
        {
            //string querry = $"SELECT nd.IDNguoiDung, nd.IDLoaiNguoiDung, nd.IDLoaiKhachHang, pl.Ten AS TenLoaiKhachHang, nd.Ten, " +
            //    $"nd.DienThoai, nd.DiaChi, nd.Username, nd.Avatar, nd.TongTienGioHang, nd.SoDonHangDaMua, nd.TongTienHangDaMua, nd.TrangThai, " +
            //    $"nd.LanHoatDongGanNhat, nd.IsAdmin, nd.CreatedDate, nd.CreatedBy, nd.ModifiedDate, nd.ModifiedBy " +
            //    $"FROM dbo.NguoiDung nd LEFT JOIN dbo.PhanLoaiKhachHang pl " +
            //    $"ON pl.IDLoaiKhachHang = nd.IDLoaiKhachHang WHERE nd.IsAdmin = 0";
            var list = db.Database.SqlQuery<flatKhachHang>($"SELECT * FROM dbo.NguoiDung nd " +
                $"WHERE nd.IsAdmin = 0 " +
                $"AND (nd.Username LIKE '%{searching}%' " +
                $"OR nd.Ten LIKE N'%{searching}%' " +
                $"OR nd.DienThoai LIKE '%{searching}%' " +
                $"OR nd.DiaChi LIKE N'%{searching}%')").ToList();

            return list;
        }

        public IEnumerable<NguoiDung> ListClientSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = db.Database.SqlQuery<NguoiDung>($"SELECT * FROM dbo.NguoiDung nd " +
               $"WHERE nd.IsAdmin = 0 " +
               $"AND (nd.Username LIKE '%{searching}%' " +
               $"OR nd.Ten LIKE N'%{searching}%' " +
               $"OR nd.DienThoai LIKE '%{searching}%' " +
               $"OR nd.DiaChi LIKE N'%{searching}%')").ToPagedList<NguoiDung>(PageNum, PageSize);

            return list;
        }
    }
}