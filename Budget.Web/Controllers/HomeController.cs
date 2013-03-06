using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Budget.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Вітаємо Вас в системі бюджетування!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
