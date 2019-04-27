using AbstractMotorFactoryRestApi.Services;
using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.Interfaces;
using AbstractMotorFactoryServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace AbstractMotorFactoryRestApi.Controllers
{
    public class CoreController : ApiController
    {
        private readonly ICoreService _service;

        private readonly IImplementerService _serviceImplementer;

        public CoreController(ICoreService service, IImplementerService serviceImplementer)
        {
            _service = service;
            _serviceImplementer = serviceImplementer;
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
        public void PayOrder(ProductionBindingModel model)
        {
            _service.PayOrder(model);
        }

        [HttpPost]
        public void PutDetailOnStorage(StorageDetailBindingModel model)
        {
            _service.PutDetailOnStorage(model);
        }

        [HttpPost]
        public void StartWork()
        {
            List<ProductionViewModel> orders = _service.GetFreeOrders();
            foreach (var order in orders)
            {
                ImplementerViewModel impl = _serviceImplementer.GetFreeWorker();
                if (impl == null)
                {
                    throw new Exception("Нет сотрудников");

                }
                new WorkImplementer(_service, _serviceImplementer, impl.Id, order.Id);
            }
        }
    }
}
