using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.ViewModels;
using System.Collections.Generic;

namespace AbstractMotorFactoryServiceDAL.Interfaces
{
    public interface ICoreService
    {
        List<ProductionViewModel> GetList();

        void CreateOrder(ProductionBindingModel model);

        void TakeOrderInWork(ProductionBindingModel model);

        void FinishOrder(ProductionBindingModel model);

        void PayOrder(ProductionBindingModel model);

        void PutDetailOnStorage(StorageDetailBindingModel model);
    }
}
