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

        public NguoiDung GetByUsername(string username)
        {
            return db.NguoiDungs.SingleOrDefault(x => x.Username == username);
        }

        public NguoiDung GetByID(Guid id)
        {
            return db.NguoiDungs.Find(id);
        }

        public int LoginAdmin(string username, string password)
        {
            var user = db.NguoiDungs.SingleOrDefault(x => x.Username == username);
            if (user == null || !user.IsAdmin)
            {
                return 0;
            }
            else
            {
                if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    GetByUsername(username).LanHoatDongGanNhat = DateTime.Now;
                    db.SaveChanges();
                    return 1;
                }
                else
                    return -1;
            }
        }

        public int LoginClient(string username, string password)
        {
            var user = db.NguoiDungs.SingleOrDefault(x => x.Username == username);
            if (user == null || user.IsAdmin)
            {
                return 0;
            }
            else
            {
                if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    GetByUsername(username).LanHoatDongGanNhat = DateTime.Now;
                    db.SaveChanges();
                    return 1;
                }
                else
                    return -1;
            }
        }

        public void ChangePassword(NguoiDung obj, string newpass)
        {
            NguoiDung nguoidung = GetByID(obj.IDNguoiDung);
            if (nguoidung != null)
            {
                nguoidung.Password = BCrypt.Net.BCrypt.HashPassword(newpass);

                db.SaveChanges();
            }
        }

        public List<NguoiDung> ListAdmin()
        {
            return db.NguoiDungs.Where(x => x.IsAdmin == true).ToList();
        }

        public List<NguoiDung> ListClient()
        {
            return db.NguoiDungs.Where(x => x.IsAdmin == false).ToList();
        }

        public List<NguoiDung> ListClientCapDoGreaterThan(int capdo)
        {
            return db.NguoiDungs.Where(x => x.IsAdmin == false).Where(x => x.PhanLoaiKhachHang.CapDo >= capdo).ToList();
        }

        public void UpdatePhanLoaiKhachHang(Guid id)
        {
            NguoiDung kh = GetByID(id);
            var listPhanLoai = (new PhanLoaiKhachHangDAO()).ListPhanLoaiKhachHang();

            if (kh != null)
            {
                foreach (PhanLoaiKhachHang pl in listPhanLoai)
                {
                    if (kh.SoDonHangDaMua >= pl.SoDonHangToiThieu && kh.TongTienHangDaMua >= pl.TongTienHangToiThieu)
                    {
                        kh.IDLoaiKhachHang = pl.IDLoaiKhachHang;
                    }
                }
            }
        }

        public void Add(NguoiDung obj)
        {
            db.NguoiDungs.Add(obj);
            db.SaveChanges();
        }

        public void Edit(NguoiDung obj)
        {
            NguoiDung nguoidung = GetByID(obj.IDNguoiDung);
            if (nguoidung != null)
            {
                nguoidung.Ten = obj.Ten;
                nguoidung.DienThoai = obj.DienThoai;
                nguoidung.Email = obj.Email;
                nguoidung.DiaChi = obj.DiaChi;
                nguoidung.Avatar = obj.Avatar;

                db.SaveChanges();
            }
        }

        public int Delete(Guid id)
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

        public IEnumerable<flatTaiKhoan> ListAccountSimple(string searching)
        {
            string query = $"SELECT nd.IDNguoiDung AS IDUser, nd.Username, lnd.Ten AS TenLoaiNguoiDung, nd.ModifiedDate " +
                $"FROM dbo.NguoiDung nd LEFT JOIN dbo.LoaiNguoiDung lnd " +
                $"ON lnd.IDLoaiNguoiDung = nd.IDLoaiNguoiDung " +
                $"WHERE nd.Username LIKE '%{searching}%' " +
                $"OR lnd.Ten LIKE N'%{searching}%' " +
                $"OR nd.ModifiedDate LIKE N'%{searching}%' " +
                $"ORDER BY nd.ModifiedDate DESC";
            var list = db.Database.SqlQuery<flatTaiKhoan>(query).ToList();

            return list;
        }

        public IEnumerable<flatTaiKhoan> ListAccountSimpleSearch(int PageNum, int PageSize, string searching)
        {
            string query = $"SELECT nd.IDNguoiDung AS IDUser, nd.Username, lnd.Ten AS TenLoaiNguoiDung, nd.ModifiedDate " +
                $"FROM dbo.NguoiDung nd LEFT JOIN dbo.LoaiNguoiDung lnd " +
                $"ON lnd.IDLoaiNguoiDung = nd.IDLoaiNguoiDung " +
                $"WHERE nd.Username LIKE '%{searching}%' " +
                $"OR lnd.Ten LIKE N'%{searching}%' " +
                $"OR nd.ModifiedDate LIKE N'%{searching}%' " +
                $"ORDER BY nd.ModifiedDate DESC";
            var list = db.Database.SqlQuery<flatTaiKhoan>(query).ToPagedList<flatTaiKhoan>(PageNum, PageSize);

            return list;
        }

        public IEnumerable<NguoiDung> ListAdminSimple(string searching)
        {
            string query = $"SELECT * FROM dbo.NguoiDung nd " +
                $"WHERE nd.IsAdmin = 1 " +
                $"AND (nd.Username LIKE '%{searching}%' " +
                $"OR nd.Ten LIKE N'%{searching}%' " +
                $"OR nd.ModifiedDate LIKE N'%{searching}%') " +
                $"ORDER BY nd.ModifiedDate DESC";
            var list = db.Database.SqlQuery<NguoiDung>(query).ToList();

            return list;
        }

        public IEnumerable<NguoiDung> ListAdminSimpleSearch(int PageNum, int PageSize, string searching)
        {
            string query = $"SELECT * FROM dbo.NguoiDung nd " +
               $"WHERE nd.IsAdmin = 1 " +
               $"AND (nd.Username LIKE '%{searching}%' " +
               $"OR nd.Ten LIKE N'%{searching}%' " +
               $"OR nd.ModifiedDate LIKE N'%{searching}%') " +
               $"ORDER BY nd.ModifiedDate DESC";
            var list = db.Database.SqlQuery<NguoiDung>(query).ToPagedList<NguoiDung>(PageNum, PageSize);

            return list;
        }

        public int TongSoThanhVien()
        {
            var countUser = db.Database.SqlQuery<NguoiDung>($"SELECT * FROM dbo.NguoiDung kh " +
            $"WHERE kh.IsAdmin = 0").ToList().Count;
            return countUser;
        }
        public IEnumerable<flatKhachHang> ListClientSimple(string searching)
        {
            string query = $"SELECT nd.IDNguoiDung, nd.Username, nd.Ten, pl.Ten AS TenLoaiKhachHang, nd.Avatar, nd.SoDonHangDaMua, nd.TongTienHangDaMua, nd.LanHoatDongGanNhat, nd.ModifiedDate " +
                $"FROM dbo.NguoiDung nd LEFT JOIN dbo.PhanLoaiKhachHang pl " +
                $"ON pl.IDLoaiKhachHang = nd.IDLoaiKhachHang " +
                $"WHERE nd.IsAdmin = 0 " +
                $"AND (nd.Username LIKE '%{searching}%' " +
                $"OR nd.Ten LIKE N'%{searching}%' " +
                $"OR pl.Ten LIKE N'%{searching}%' " +
                $"OR nd.SoDonHangDaMua LIKE N'%{searching}%' " +
                $"OR nd.TongTienHangDaMua LIKE N'%{searching}%' " +
                $"OR nd.LanHoatDongGanNhat LIKE N'%{searching}%' " +
                $"OR nd.ModifiedDate LIKE N'%{searching}%') " +
                $"ORDER BY nd.ModifiedDate DESC";
            var list = db.Database.SqlQuery<flatKhachHang>(query).ToList();

            return list;
        }

        public IEnumerable<flatKhachHang> ListClientSimpleSearch(int PageNum, int PageSize, string searching)
        {
            string query = $"SELECT nd.IDNguoiDung, nd.Username, nd.Ten, pl.Ten AS TenLoaiKhachHang, nd.Avatar, nd.SoDonHangDaMua, nd.TongTienHangDaMua, nd.LanHoatDongGanNhat, nd.ModifiedDate " +
                $"FROM dbo.NguoiDung nd LEFT JOIN dbo.PhanLoaiKhachHang pl " +
                $"ON pl.IDLoaiKhachHang = nd.IDLoaiKhachHang " +
                $"WHERE nd.IsAdmin = 0 " +
                $"AND (nd.Username LIKE '%{searching}%' " +
                $"OR nd.Ten LIKE N'%{searching}%' " +
                $"OR pl.Ten LIKE N'%{searching}%' " +
                $"OR nd.SoDonHangDaMua LIKE N'%{searching}%' " +
                $"OR nd.TongTienHangDaMua LIKE N'%{searching}%' " +
                $"OR nd.LanHoatDongGanNhat LIKE N'%{searching}%' " +
                $"OR nd.ModifiedDate LIKE N'%{searching}%') " +
                $"ORDER BY nd.ModifiedDate DESC";
            var list = db.Database.SqlQuery<flatKhachHang>(query).ToPagedList<flatKhachHang>(PageNum, PageSize);

            return list;
        }

    }
}