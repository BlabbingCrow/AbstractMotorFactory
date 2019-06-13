using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.Interfaces;
using AbstractMotorFactoryServiceDAL.ViewModels;

namespace AbstractMotorFactoryWeb.Controllers
{
    public class EngineController : Controller
    {
        private IEngineService engineService = Globals.EngineService;
        private IDetailService detailService = Globals.DetailService;
        private bool state = true;

        public ActionResult Index()
        {
            Session.Remove("Engine");
            return View(engineService.GetList());
        }

        public ActionResult Create()
        {
            state = true;
            if (Session["Engine"] == null)
            {
                var engine = new EngineViewModel();
                engine.EngineDetails = new List<EngineDetailViewModel>();
                Session["Engine"] = engine;
            }
            return View((EngineViewModel)Session["Engine"]);
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
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
                engineService.AddElement(new EngineBindingModel
                {
                    EngineName = Request["EngineName"],
                    Cost = Convert.ToDecimal(Request["Cost"]),
                    EngineDetails = engineDetails
                });
                Session.Remove("Engine");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            state = false;
            if (Session["Engine"] == null)
            {
                var engine = engineService.GetElement(id);
                Session["Engine"] = engine;
            }
            return View((EngineViewModel)Session["Engine"]);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
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
                engineService.UpdElement(new EngineBindingModel
                {
                    Id = id,
                    EngineName = Request["EngineName"],
                    Cost = Convert.ToDecimal(Request["Cost"]),
                    EngineDetails = engineDetails
                });
                Session.Remove("Engine");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                engineService.DelElement(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AddDetail()
        {
            var details = new SelectList(detailService.GetList(), "Id", "DetailName");
            ViewBag.Detail = details;
            return View();
        }

        [HttpPost]
        public ActionResult AddDetail(FormCollection collection)
        {
            try
            {
                var engine = (EngineViewModel)Session["Engine"];
                var detail = new EngineDetailViewModel
                {
                    DetailId = int.Parse(Request["Id"]),
                    DetailName = detailService.GetElement(int.Parse(Request["Id"])).DetailName,
                    Number = int.Parse(Request["Number"])
                };
                engine.EngineDetails.Add(detail);
                Session["Engine"] = engine;

                if (state)
                {
                    return RedirectToAction("Create");
                }
                else
                {
                    return RedirectToAction("Edit");
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteDetail(int id)
        {
            try
            {
                var engine = (EngineViewModel)Session["Engine"];
                engine.EngineDetails.RemoveAll((x) => x.Id == id);
                Session["Engine"] = engine;

                if (state)
                {
                    return RedirectToAction("Create");
                }
                else
                {
                    return RedirectToAction("Edit");
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
