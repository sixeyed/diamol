using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NerdDinner.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Organizing the world's nerds and helping them eat in packs.";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
}
}
