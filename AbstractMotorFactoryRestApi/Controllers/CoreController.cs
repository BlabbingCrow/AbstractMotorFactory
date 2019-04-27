using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.Interfaces;
using System;
using System.Web.Http;

namespace AbstractMotorFactoryRestApi.Controllers
{
    public class CoreController : ApiController
    {
        private readonly ICoreService _service;

        public CoreController(ICoreService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetList()
        {
            var list = _service.GetList();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public void CreateOrder(ProductionBindingModel model)
        {
            _service.CreateOrder(model);
        }

        [HttpPost]
        public void TakeOrderInWork(ProductionBindingModel model)
        {
            _service.TakeOrderInWork(model);
        }

        [HttpPost]
        public void FinishOrder(ProductionBindingModel model)
        {
            _service.FinishOrder(model);
        }

        [HttpPost]
        public void PayOrder(ProductionBindingModel model)
        {
            _service.PayOrder(model);
        }

        [HttpPost]
        public void PutDetailOnStorage(StorageDetailBindingModel model)
        {
            _service.PutDetailOnStorage(model);
        }
    }
}
