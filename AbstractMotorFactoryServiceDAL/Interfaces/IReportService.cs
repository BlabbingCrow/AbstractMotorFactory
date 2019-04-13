using System.Collections.Generic;
using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.ViewModels;

namespace AbstractMotorFactoryServiceDAL.Interfaces
{
    public interface IReportService
    {
        void SaveProductPrice(ReportBindingModel model);

        List<StoragesLoadViewModel> GetStocksLoad();

        void SaveStocksLoad(ReportBindingModel model);

        List<CustomerProductionsViewModel> GetClientOrders(ReportBindingModel model);

        void SaveClientOrders(ReportBindingModel model);
    }
}
