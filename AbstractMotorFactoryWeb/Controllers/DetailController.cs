using System.Web.Mvc;
using AbstractMotorFactoryServiceDAL.Interfaces;
using AbstractMotorFactoryServiceDAL.BindingModels;

namespace AbstractMotorFactoryWeb.Controllers
{
    public class DetailController : Controller
    {
        private IDetailService service = Globals.DetailService;

        public ActionResult Index()
        {
            return View(service.GetList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePost()
        {
            service.AddElement(new DetailBindingModel
            {
                DetailName = Request["DetailName"]
            });
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var viewModel = service.GetElement(id);
            var bindingModel = new DetailBindingModel
            {
                Id = id,
                DetailName = viewModel.DetailName
            };
            return View(bindingModel);
        }

        [HttpPost]
        public ActionResult EditPost()
        {
            service.UpdElement(new DetailBindingModel
            {
                Id = int.Parse(Request["Id"]),
                DetailName = Request["DetailName"]
            });
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            service.DelElement(id);
            return RedirectToAction("Index");
        }
    }
}