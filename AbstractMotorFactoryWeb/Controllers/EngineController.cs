using System.Web.Mvc;
using AbstractMotorFactoryServiceDAL.Interfaces;
using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.ViewModels;
using System;
using System.Collections.Generic;

namespace AbstractMotorFactoryWeb.Controllers
{
    public class EngineController : Controller
    {
        private IEngineService service = Globals.EngineService;
        private IDetailService ingredientService = Globals.DetailService;

        public ActionResult List()
        {
            if (Session["Engine"] == null)
            {
                var engine = new EngineViewModel();
                engine.EngineDetails = new List<EngineDetailViewModel>();
                Session["Engine"] = engine;
            }
            return View((EngineViewModel)Session["Engine"]);
        }

        public ActionResult Index()
        {
            return View(service.GetList());
        }

        public ActionResult Create()
        {
            var ingredients = new SelectList(ingredientService.GetList(), "Id", "DetailName");
            ViewBag.Detail = ingredients;
            return View();
        }

        public ActionResult CreateEngine()
        {
            if (Session["Engine"] == null)
            {
                var engine = new EngineViewModel();
                engine.EngineDetails = new List<EngineDetailViewModel>();
                Session["Engine"] = engine;
            }
            return View((EngineViewModel)Session["Engine"]);
        }

        [HttpPost]
        public ActionResult CreateDetailPost()
        {
            var engine = (EngineViewModel)Session["Engine"];
            var ingredient = new EngineDetailViewModel
            {
                DetailId = int.Parse(Request["Id"]),
                DetailName = ingredientService.GetElement(int.Parse(Request["Id"])).DetailName,
                Number = int.Parse(Request["Number"])
            };
            engine.EngineDetails.Add(ingredient);
            Session["Engine"] = engine;
            return RedirectToAction("CreateEngine");
        }

        [HttpPost]
        public ActionResult CreateEnginePost()
        {
            var engine = (EngineViewModel)Session["Engine"];
            var engineDetails = new List<EngineDetailBindingModel>();
            for (int i = 0; i < engine.EngineDetails.Count; ++i)
            {
                engineDetails.Add(new EngineDetailBindingModel
                {
                    Id = engine.EngineDetails[i].Id,
                    EngineId = engine.EngineDetails[i].EngineId,
                    DetailId = engine.EngineDetails[i].DetailId,
                    Number = engine.EngineDetails[i].Number
                });
            }
            service.AddElement(new EngineBindingModel
            {
                EngineName = Request["EngineName"],
                Cost = Convert.ToDecimal(Request["Cost"]),
                EngineDetails = engineDetails
            });
            Session.Remove("Engine");
            return RedirectToAction("Index");
        }
    }
}