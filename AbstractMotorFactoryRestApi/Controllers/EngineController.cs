using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.Interfaces;
using System;
using System.Web.Http;

namespace AbstractMotorFactoryRestApi.Controllers
{
    public class EngineController : ApiController
    {
        private readonly IEngineService _service;
        public EngineController(IEngineService service)
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

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var element = _service.GetElement(id);
            if (element == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(element);
        }

        [HttpPost]
        public void AddElement(EngineBindingModel model)
        {
            _service.AddElement(model);
        }

        [HttpPost]
        public void UpdElement(EngineBindingModel model)
        {
            _service.UpdElement(model);
        }

        [HttpPost]
        public void DelElement(EngineBindingModel model)
        {
            _service.DelElement(model.Id);
        }
    }
}
