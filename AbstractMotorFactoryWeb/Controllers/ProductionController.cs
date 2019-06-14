using System;
using System.Web.Mvc;
using AbstractMotorFactoryServiceDAL.ViewModels;
using AbstractMotorFactoryServiceDAL.Interfaces;
using AbstractMotorFactoryServiceDAL.BindingModels;

namespace AbstractMotorFactoryWeb.Controllers
{
    public class ProductionController : Controller
    {
        private IEngineService engineService = Globals.EngineService;
        private ICoreService coreService = Globals.CoreService;
        private ICustomerService customerService = Globals.CustomerService;

        public ActionResult Index()
        {
            return View(coreService.GetList());
        }

        public ActionResult Create()
        {
            var engines = new SelectList(engineService.GetList(), "Id", "EngineName");
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

            coreService.CreateOrder(new ProductionBindingModel
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
            EngineViewModel engine = engineService.GetElement(engineId);
            return engineCount * engine.Cost;
        }

        public ActionResult SetStatus(int id, string status)
        {
            try
            {
                switch (status)
                {
                    case "Processing":
                        coreService.TakeOrderInWork(new ProductionBindingModel { Id = id });
                        break;
                    case "Ready":
                        coreService.FinishOrder(new ProductionBindingModel { Id = id });
                        break;
                    case "Paid":
                        coreService.PayOrder(new ProductionBindingModel { Id = id });
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