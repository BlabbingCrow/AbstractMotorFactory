using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.ViewModels;
using AbstractMotorFactoryServiceDAL.Attributies;
using System.Collections.Generic;

namespace AbstractMotorFactoryServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с заказами")]
    public interface ICoreService
    {
        [CustomMethod("Метод получения списка заказов")]
        List<ProductionViewModel> GetList();

        [CustomMethod("Метод получения свободных заказов")]
        List<ProductionViewModel> GetFreeOrders();

        [CustomMethod("Метод создания заказа")]
        void CreateOrder(ProductionBindingModel model);

        [CustomMethod("Метод передачи заказа в работу")]
        void TakeOrderInWork(ProductionBindingModel model);

        [CustomMethod("Метод окончания работы над заказом")]
        void FinishOrder(ProductionBindingModel model);

        [CustomMethod("Метод оплаты заказа")]
        void PayOrder(ProductionBindingModel model);

        [CustomMethod("Метод помещения детали на склад")]
        void PutDetailOnStorage(StorageDetailBindingModel model);
    }
}
