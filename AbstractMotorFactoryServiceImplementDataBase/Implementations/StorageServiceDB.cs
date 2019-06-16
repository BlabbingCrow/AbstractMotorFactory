using AbstractMotorFactoryModel;
using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.Interfaces;
using AbstractMotorFactoryServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractMotorFactoryServiceImplementDataBase.Implementations
{
    public class StorageServiceDB : IStorageService
    {
        private AbstractDbContext context;

        public StorageServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }

        public List<StorageViewModel> GetList()
        {
            List<StorageViewModel> result = context.Storages.Select(rec => new StorageViewModel
            {
                Id = rec.Id,
                StorageName = rec.StorageName,
                StorageDetails = context.StorageDetails
                    .Where(recPC => recPC.Id == rec.Id)
                    .Select(recPC => new StorageDetailViewModel
                    {
                        Id = recPC.Id,
                        StorageId = recPC.StorageId,
                        DetailId = recPC.DetailId,
                        DetailName = recPC.Detail.DetailName,
                        Number = recPC.Number
                    })
                   .ToList()
            })
            .ToList();
            return result;
        }

        public StorageViewModel GetElement(int id)
        {
            Storage element = context.Storages.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new StorageViewModel
                {
                    Id = element.Id,
                    StorageName = element.StorageName,
                    StorageDetails = context.StorageDetails
                    .Where(recPC => recPC.Id == element.Id)
                    .Select(recPC => new StorageDetailViewModel
                    {
                        Id = recPC.Id,
                        StorageId = recPC.StorageId,
                        DetailId = recPC.DetailId,
                        DetailName = recPC.Detail.DetailName,
                        Number = recPC.Number
                    })
                    .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(StorageBindingModel model)
        {
            Storage element = context.Storages.FirstOrDefault(rec =>
            rec.StorageName == model.StorageName);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = new Storage
            {
                StorageName = model.StorageName,
            };
            context.Storages.Add(element);
            context.SaveChanges();
        }

        public void UpdElement(StorageBindingModel model)
        {
            Storage element = context.Storages.FirstOrDefault(rec => rec.StorageName == model.StorageName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = context.Storages.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.StorageName = model.StorageName;
            context.SaveChanges();
        }
        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Storage element = context.Storages.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.StorageDetails.RemoveRange(context.StorageDetails.Where(rec =>
                        rec.StorageId == id));
                        context.Storages.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
