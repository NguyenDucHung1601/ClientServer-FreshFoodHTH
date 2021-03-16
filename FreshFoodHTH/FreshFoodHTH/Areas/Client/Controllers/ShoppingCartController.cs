using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodHTH.Areas.Client.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: Client/ShoppingCart
        public ActionResult Index()
        {
            return View();
        }
    }
}