using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbstractMotorFactoryWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Customers()
        {
            return RedirectToAction("Index", "Customer");
        }

        public ActionResult Details()
        {
            return RedirectToAction("Index", "Detail");
        }

        public ActionResult Engines()
        {
            return RedirectToAction("Index", "Engine");
        }

        public ActionResult Productions()
        {
            return RedirectToAction("Index", "Production");
        }
    }
}