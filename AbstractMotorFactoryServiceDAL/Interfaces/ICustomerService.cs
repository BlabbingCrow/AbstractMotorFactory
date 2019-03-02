using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.ViewModels;
using System.Collections.Generic;

namespace AbstractMotorFactoryServiceDAL.Interfaces
{
    public interface ICustomerService
    {
        List<CustomerViewModel> GetList();

        CustomerViewModel GetElement(int id);

        void AddElement(CustomerBindingModel model);

        void UpdElement(CustomerBindingModel model);

        void DelElement(int id);
    }
}
