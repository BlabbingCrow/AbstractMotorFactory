using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.ViewModels;
using System.Collections.Generic;

namespace AbstractMotorFactoryServiceDAL.Interfaces
{
    public interface IEngineService
    {
        List<EngineViewModel> GetList();

        EngineViewModel GetElement(int id);

        void AddElement(EngineBindingModel model);

        void UpdElement(EngineBindingModel model);

        void DelElement(int id);
    }
}
