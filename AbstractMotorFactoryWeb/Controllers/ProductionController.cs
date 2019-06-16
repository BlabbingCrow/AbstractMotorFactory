using System;
using System.Web.Mvc;
using AbstractMotorFactoryServiceDAL.ViewModels;
using AbstractMotorFactoryServiceDAL.Interfaces;
using AbstractMotorFactoryServiceDAL.BindingModels;

namespace AbstractMotorFactoryWeb.Controllers
{
    public class ProductionController : Controller
    {
        private IEngineService fabricService = Globals.EngineService;

        private ICoreService mainService = Globals.CoreService;

        private ICustomerService customerService = Globals.CustomerService;

        public ActionResult Index()
        {
            return View(mainService.GetList());
        }

        public ActionResult Create()
        {
            var engines = new SelectList(fabricService.GetList(), "Id", "EngineName");
            var customers = new SelectList(customerService.GetList(), "Id", "CustomerFIO");
            ViewBag.Engine = engines;
            ViewBag.Customer = customers;
            return View();
        }

        [HttpPost]
        public ActionResult CreatePost()
        {
            var engineId = int.Parse(Request["EngineId"]);
            var customerId = int.Parse(Request["CustomerId"]);
            var number = int.Parse(Request["Number"]);
            var amount = CalcSum(engineId, number);

            mainService.CreateOrder(new ProductionBindingModel
            {
                EngineId = engineId,
                CustomerId = customerId,
                Number = number,
                Amount = amount
            });
            return RedirectToAction("Index");
        }

        private Decimal CalcSum(int engineId, int engineCount)
        {
            EngineViewModel engine = fabricService.GetElement(engineId);
            return engineCount * engine.Cost;
        }

        public ActionResult SetStatus(int id, string status)
        {
            try
            {
                switch (status)
                {
                    case "Processing":
                        mainService.TakeOrderInWork(new ProductionBindingModel { Id = id });
                        break;
                    case "Ready":
                        mainService.FinishOrder(new ProductionBindingModel { Id = id });
                        break;
                    case "Paid":
                        mainService.PayOrder(new ProductionBindingModel { Id = id });
                        break;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }


            return RedirectToAction("Index");
        }
    }
}