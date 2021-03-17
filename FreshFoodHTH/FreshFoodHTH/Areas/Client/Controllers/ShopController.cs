using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodHTH.Areas.Client.Controllers
{
    public class ShopController : BaseController
    {
        // GET: Client/Shop
        public ActionResult Index()
        {
            return View();
        }
    }
}