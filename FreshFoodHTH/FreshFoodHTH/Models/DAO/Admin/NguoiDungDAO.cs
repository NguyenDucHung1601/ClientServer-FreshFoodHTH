using FreshFoodHTH.Models.EF;
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

        public NguoiDung GetById(string username)
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
    }
}