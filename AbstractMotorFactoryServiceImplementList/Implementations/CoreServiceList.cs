using AbstractMotorFactoryModel;
using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.Interfaces;
using AbstractMotorFactoryServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractMotorFactoryServiceImplementList.Implementations
{
    public class CoreServiceList : ICoreService
    {
        private DataListSingleton source;

        public CoreServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<ProductionViewModel> GetList()
        {
            List<ProductionViewModel> result = source.Productions
                .Select(rec => new ProductionViewModel
                {
                    Id = rec.Id,
                    CustomerId = rec.CustomerId,
                    EngineId = rec.EngineId,
                    TimeCreate = rec.TimeCreate.ToLongDateString(),
                    TimeImplement = rec.TimeImplement?.ToLongDateString(),
                    State = rec.State.ToString(),
                    Number = rec.Number,
                    Amount = rec.Amount,
                    CustomerFIO = source.Customers.FirstOrDefault(recC => recC.Id == rec.CustomerId)?.CustomerFIO,
                    EngineName = source.Engines.FirstOrDefault(recP => recP.Id == rec.EngineId)?.EngineName
                })
                .ToList();
            return result;
        }

        public List<ProductionViewModel> GetFreeOrders()
        {
            List<ProductionViewModel> result = source.Productions
            .Where(x => x.State == ProductionStatus.Принят || x.State == ProductionStatus.НедостаточноРесурсов)
            .Select(rec => new ProductionViewModel
            {
                Id = rec.Id
            })
            .ToList();
            return result;
        }

        public void CreateOrder(ProductionBindingModel model)
        {
            int maxId = source.Productions.Count > 0 ? source.Productions.Max(rec => rec.Id) : 0;
            source.Productions.Add(new Production
            {
                Id = maxId + 1,
                CustomerId = model.CustomerId,
                EngineId = model.EngineId,
                TimeCreate = DateTime.Now,
                Number = model.Number,
                Amount = model.Amount,
                State = ProductionStatus.Принят
            });
        }

        public void TakeOrderInWork(ProductionBindingModel model)
        {
            Production element = source.Productions.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.State != ProductionStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }

            var engineDetails = source.EngineDetails.Where(rec => rec.EngineId == element.EngineId);
            foreach (var engineDetail in engineDetails)
            {
                int countOnStocks = source.StorageDetails.Where(rec => rec.DetailId == engineDetail.DetailId).Sum(rec => rec.Number);
                if (countOnStocks < engineDetail.Number * element.Number)
                {
                    var componentName = source.Details.FirstOrDefault(rec => rec.Id == engineDetail.DetailId);
                    throw new Exception("Не достаточно компонента " + componentName?.DetailName + " требуется " + (engineDetail.Number * element.Number) + ", в наличии " + countOnStocks);
                }
            }

            foreach (var engineDetail in engineDetails)
            {
                int numInStorage = engineDetail.Number * element.Number;
                var storageDetails = source.StorageDetails.Where(rec => rec.DetailId == engineDetail.DetailId);
                foreach (var storageDetail in storageDetails)
                {
                    if (storageDetail.Number >= numInStorage)
                    {
                        storageDetail.Number -= numInStorage;
                        break;
                    }
                    else
                    {
                        numInStorage -= storageDetail.Number;
                        storageDetail.Number = 0;
                    }
                }
            }
            element.TimeImplement = DateTime.Now;
            element.State = ProductionStatus.Выполняется;
        }

        public void FinishOrder(ProductionBindingModel model)
        {
            Production element = source.Productions.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.State != ProductionStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.State = ProductionStatus.Готов;
        }

        public void PayOrder(ProductionBindingModel model)
        {
            Production element = source.Productions.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.State != ProductionStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.State = ProductionStatus.Оплачен;
        }

        public void PutDetailOnStorage(StorageDetailBindingModel model)
        {
            StorageDetail element = source.StorageDetails.FirstOrDefault(rec =>
                rec.StorageId == model.StorageId && rec.DetailId == model.DetailId);
            if (element != null)
            {
                element.Number += model.Number;
            }
            else
            {
                int maxId = source.StorageDetails.Count > 0 ?
                source.StorageDetails.Max(rec => rec.Id) : 0;
                source.StorageDetails.Add(new StorageDetail
                {
                    Id = ++maxId,
                    StorageId = model.StorageId,
                    DetailId = model.DetailId,
                    Number = model.Number
                });
            }
        }
    }
}
