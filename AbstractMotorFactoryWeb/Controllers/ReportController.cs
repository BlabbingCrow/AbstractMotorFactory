using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AbstractMotorFactoryServiceDAL.Interfaces;
using AbstractMotorFactoryServiceDAL.BindingModels;

namespace AbstractMotorFactoryWeb.Controllers
{
    public class ReportController : Controller
    {
        private IReportService service = Globals.ReportService;

        public ActionResult SaveProductPrice()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveProductPrice(FormCollection collection)
        {
            try
            {
                service.SaveProductPrice(new ReportBindingModel {
                    FileName = Request["FileName"]
                });
                return RedirectToAction("Index", "Production");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult SaveStocksLoad()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveStocksLoad(FormCollection collection)
        {
            try
            {
                service.SaveStocksLoad(new ReportBindingModel
                {
                    FileName = Request["FileName"]
                });
                return RedirectToAction("Index", "Production");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult SaveClientOrders()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveClientOrders(FormCollection collection)
        {
            try
            {
                service.SaveClientOrders(new ReportBindingModel
                {
                    FileName = Request["FileName"]
                });
                return RedirectToAction("Index", "Production");
            }
            catch
            {
                return View();
            }
        }
    }
}
