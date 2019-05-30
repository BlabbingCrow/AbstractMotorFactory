using System.Collections.Generic;
using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.ViewModels;
using AbstractMotorFactoryServiceDAL.Attributies;

namespace AbstractMotorFactoryServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с отчетами")]
    public interface IReportService
    {
        [CustomMethod("Метод сохранения цены на продукт")]
        void SaveProductPrice(ReportBindingModel model);

        [CustomMethod("Метод получения списка загруженности складов")]
        List<StoragesLoadViewModel> GetStocksLoad();

        [CustomMethod("Метод сохранения загрузки складов")]
        void SaveStocksLoad(ReportBindingModel model);

        [CustomMethod("Метод получения списка заказов клиентов")]
        List<CustomerProductionsViewModel> GetClientOrders(ReportBindingModel model);

        [CustomMethod("Метод сохранения заказов клиентов")]
        void SaveClientOrders(ReportBindingModel model);
    }
}
