using FreshFoodHTH.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodHTH.Areas.Client.Controllers
{
    public class ContactController : Controller
    {
        FreshFoodDBContext db = new FreshFoodDBContext();
        // GET: Client/Contact
        public ActionResult Index()
        {
            return View();
        }
    }
}