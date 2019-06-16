using AbstractMotorFactoryModel;
using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.Interfaces;
using AbstractMotorFactoryServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractMotorFactoryServiceImplementDataBase.Implementations
{
    public class DetailServiceDB : IDetailService
    {
        private AbstractDbContext context;

        public DetailServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }

        public List<DetailViewModel> GetList()
        {
            List<DetailViewModel> result = context.Details.Select(rec => new DetailViewModel
            {
                Id = rec.Id,
                DetailName = rec.DetailName
            })
                .ToList();
            return result;
        }
        public DetailViewModel GetElement(int id)
        {
            Detail element = context.Details.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new DetailViewModel
                {
                    Id = element.Id,
                    DetailName = element.DetailName
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(DetailBindingModel model)
        {
            Detail element = context.Details.FirstOrDefault(rec => rec.DetailName == model.DetailName);
            if (element != null)
            {
                throw new Exception("Уже есть ингредиент с таким названием");
            }
            context.Details.Add(new Detail
            {
                DetailName = model.DetailName
            });
            context.SaveChanges();
        }
        public void UpdElement(DetailBindingModel model)
        {
            Detail element = context.Details.FirstOrDefault(rec => rec.DetailName ==
                model.DetailName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть ингредиент с таким названием");
            }
            element = context.Details.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.DetailName = model.DetailName;
            context.SaveChanges();
        }
        public void DelElement(int id)
        {
            Detail element = context.Details.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Details.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
