using System.Collections.Generic;

namespace AbstractMotorFactoryServiceDAL.Interfaces
{
    public interface IReportService
    {
        void SaveProductPrice(ReportBindingModel model);

        List<StorageLoadViewModel> GetStocksLoad();

        void SaveStocksLoad(ReportBindingModel model);

        List<CustomerProductionModel> GetClientOrders(ReportBindingModel model);

        void SaveClientOrders(ReportBindingModel model);
    }
}
