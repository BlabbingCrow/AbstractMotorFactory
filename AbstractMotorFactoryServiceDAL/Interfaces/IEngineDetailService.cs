using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.ViewModels;
using System.Collections.Generic;


namespace AbstractMotorFactoryServiceDAL.Interfaces
{
    public interface IEngineDetailService
    {
        List<EngineDetailViewModel> GetList();

        EngineDetailViewModel GetElement(int id);

        void AddElement(EngineDetailBindingModel model);

        void UpdElement(EngineDetailBindingModel model);

        void DelElement(int id);
    }
}
