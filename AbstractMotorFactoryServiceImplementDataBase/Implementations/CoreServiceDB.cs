using AbstractMotorFactoryModel;
using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.Interfaces;
using AbstractMotorFactoryServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Data.Entity;
using System.Linq;

namespace AbstractMotorFactoryServiceImplementDataBase.Implementations
{
    public class CoreServiceDB : ICoreService
    {
        private AbstractDbContext context;
        public CoreServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }
        public List<ProductionViewModel> GetList()
        {
            List<ProductionViewModel> result = context.Productions.Select(rec => new ProductionViewModel
            {
                Id = rec.Id,
                CustomerId = rec.CustomerId,
                EngineId = rec.EngineId,
                TimeCreate = SqlFunctions.DateName("dd", rec.TimeCreate) + " " +
                SqlFunctions.DateName("mm", rec.TimeCreate) + " " +
                SqlFunctions.DateName("yyyy", rec.TimeCreate),
                TimeImplement = rec.TimeImplement == null ? "" :
                SqlFunctions.DateName("dd", rec.TimeImplement.Value) + " " +
                SqlFunctions.DateName("mm", rec.TimeImplement.Value) + " " +
                SqlFunctions.DateName("yyyy", rec.TimeImplement.Value),
                State = rec.State.ToString(),
                Number = rec.Number,
                Amount = rec.Amount,
                CustomerFIO = rec.Customer.CustomerFIO,
                EngineName = rec.Engine.EngineName
            })
            .ToList();
            return result;
        }
        public void CreateOrder(ProductionBindingModel model)
        {
            context.Productions.Add(new Production
            {
                CustomerId = model.CustomerId,
                EngineId = model.EngineId,
                TimeCreate = DateTime.Now,
                Number = model.Number,
                Amount = model.Amount,
                State = ProductionStatus.Принят
            });
            context.SaveChanges();
        }
        public void TakeOrderInWork(ProductionBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Production element = context.Productions.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    if (element.State != ProductionStatus.Принят)
                    {
                        throw new Exception("Заказ не в статусе \"Принят\"");
                    }
                    var engineDetails = context.EngineDetails.Include(rec => rec.Detail).Where(rec => rec.EngineId == element.EngineId);

                    foreach (var productComponent in engineDetails)
                    {
                        int NumberOnStocks = productComponent.Number * element.Number;
                        var storageComponents = context.StorageDetails.Where(rec =>
                        rec.DetailId == productComponent.DetailId);
                        foreach (var stockComponent in storageComponents)
                        {
                            if (stockComponent.Number >= NumberOnStocks)
                            {
                                stockComponent.Number -= NumberOnStocks;
                                NumberOnStocks = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                NumberOnStocks -= stockComponent.Number;
                                stockComponent.Number = 0;
                                context.SaveChanges();
                            }
                        }
                        if (NumberOnStocks > 0)
                        {
                            throw new Exception("Не достаточно компонента " +
                           productComponent.Detail.DetailName + " требуется " + productComponent.Number + ", нехватает " + NumberOnStocks);
                         }
                    }
                    element.TimeImplement = DateTime.Now;
                    element.State = ProductionStatus.Выполняется;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void FinishOrder(ProductionBindingModel model)
        {
            Production element = context.Productions.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.State != ProductionStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.State = ProductionStatus.Готов;
            context.SaveChanges();
        }
        public void PayOrder(ProductionBindingModel model)
        {
            Production element = context.Productions.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.State != ProductionStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.State = ProductionStatus.Оплачен;
            context.SaveChanges();
        }
        public void PutDetailOnStorage(StorageDetailBindingModel model)
        {
            StorageDetail element = context.StorageDetails.FirstOrDefault(rec => rec.StorageId == model.StorageId && rec.DetailId == model.DetailId);
            if (element != null)
            {
                element.Number += model.Number;
            }
            else
            {
                context.StorageDetails.Add(new StorageDetail
                {
                    StorageId = model.StorageId,
                    DetailId = model.DetailId,
                    Number = model.Number
                });
            }
            context.SaveChanges();
        }
    }
}
