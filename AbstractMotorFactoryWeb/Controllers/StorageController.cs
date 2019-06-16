using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbstractMotorFactoryWeb.Controllers
{
    public class StorageController : Controller
    {
        private IDetailService detailService = Globals.DetailService;
        private IStorageService storageService = Globals.StorageService;
        private ICoreService mainService = Globals.CoreService;

        public ActionResult Index()
        {
            return View(storageService.GetList());
        }

        public ActionResult Details(int id)
        {
            return View(storageService.GetElement(id).StorageDetails);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                storageService.AddElement(new StorageBindingModel
                {
                    StorageName = Request["StorageName"]
                });
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View(new StorageBindingModel
            {
                Id = id,
                StorageName = storageService.GetElement(id).StorageName
            });
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                storageService.UpdElement(new StorageBindingModel
                {
                    Id = id,
                    StorageName = Request["StorageName"]
                });
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            storageService.DelElement(id);
            return RedirectToAction("Index");
        }
        
        public ActionResult PutDetailOnStorage()
        {
            ViewBag.Detail = new SelectList(detailService.GetList(), "Id", "DetailName");
            ViewBag.Storage = new SelectList(storageService.GetList(), "Id", "StorageName");
            return View();
        }

        [HttpPost]
        public ActionResult PutDetailOnStorage(FormCollection collection)
        {
            try
            {
                mainService.PutDetailOnStorage(new StorageDetailBindingModel
                {
                    DetailId = int.Parse(Request["DetailId"]),
                    StorageId = int.Parse(Request["StorageId"]),
                    Number = int.Parse(Request["Number"])
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
