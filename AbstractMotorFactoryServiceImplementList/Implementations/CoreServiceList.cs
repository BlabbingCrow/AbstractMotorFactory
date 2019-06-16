using AbstractMotorFactoryModel;
using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.Interfaces;
using AbstractMotorFactoryServiceDAL.ViewModels;
using System;
using System.Collections.Generic;

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
            List<ProductionViewModel> result = new List<ProductionViewModel>();
            for (int i = 0; i < source.Productions.Count; ++i)
            {
                string customerFIO = string.Empty;
                for (int j = 0; j < source.Customers.Count; ++j)
                {
                    if (source.Customers[j].Id == source.Productions[i].CustomerId)
                    {
                        customerFIO = source.Customers[j].CustomerFIO;
                        break;
                    }
                }
                string engineName = string.Empty;
                for (int j = 0; j < source.Engines.Count; ++j)
                {
                    if (source.Engines[j].Id == source.Productions[i].EngineId)
                    {
                        engineName = source.Engines[j].EngineName;
                        break;
                    }
                }
                result.Add(new ProductionViewModel
                {
                    Id = source.Productions[i].Id,
                    CustomerId = source.Productions[i].CustomerId,
                    CustomerFIO = customerFIO,
                    EngineId = source.Productions[i].EngineId,
                    EngineName = engineName,
                    Number = source.Productions[i].Number,
                    Amount = source.Productions[i].Amount,
                    TimeCreate = source.Productions[i].TimeCreate.ToLongDateString(),
                    TimeImplement = source.Productions[i].TimeImplement?.ToLongDateString(),
                    State = source.Productions[i].State.ToString()
                });
            }
            return result;
        }

        public void CreateOrder(ProductionBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Productions.Count; ++i)
            {
                if (source.Productions[i].Id > maxId)
                {
                    maxId = source.Customers[i].Id;
                }
            }
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
            int index = -1;
            for (int i = 0; i < source.Productions.Count; ++i)
            {
                if (source.Productions[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.Productions[index].State != ProductionStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            source.Productions[index].TimeImplement = DateTime.Now;
            source.Productions[index].State = ProductionStatus.Выполняется;
        }

        public void FinishOrder(ProductionBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Productions.Count; ++i)
            {
                if (source.Customers[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.Productions[index].State != ProductionStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            source.Productions[index].State = ProductionStatus.Готов;

        }

        public void PayOrder(ProductionBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Productions.Count; ++i)
            {
                if (source.Customers[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.Productions[index].State != ProductionStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            source.Productions[index].State = ProductionStatus.Оплачен;
        }
    }
}
