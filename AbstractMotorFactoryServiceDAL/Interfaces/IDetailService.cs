using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.ViewModels;
using System.Collections.Generic;

namespace AbstractMotorFactoryServiceDAL.Interfaces
{
    public interface IDetailService
    {
        List<DetailViewModel> GetList();

        DetailViewModel GetElement(int id);

        void AddElement(DetailBindingModel model);

        void UpdElement(DetailBindingModel model);

        void DelElement(int id);
    }
}
