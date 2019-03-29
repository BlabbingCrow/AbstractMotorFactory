using AbstractMotorFactoryModel;
using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.Interfaces;
using AbstractMotorFactoryServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractMotorFactoryServiceImplementList.Implementations
{
    public class DetailServiceList : IDetailService
    {
        private DataListSingleton source;

        public DetailServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<DetailViewModel> GetList()
        {
            List<DetailViewModel> result = source.Details.Select(rec => new DetailViewModel
            {
                Id = rec.Id,
                DetailName = rec.DetailName
            })
            .ToList();
            return result;
        }

        public DetailViewModel GetElement(int id)
        {
            Detail element = source.Details.FirstOrDefault(rec => rec.Id == id);
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
            Detail element = source.Details.FirstOrDefault(rec => rec.DetailName == model.DetailName);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            int maxId = source.Details.Count > 0 ? source.Details.Max(rec => rec.Id) : 0;
            source.Details.Add(new Detail
            {
                Id = maxId + 1,
                DetailName = model.DetailName
            });
        }

        public void UpdElement(DetailBindingModel model)
        {
            Detail element = source.Details.FirstOrDefault(rec => rec.DetailName == model.DetailName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            element = source.Details.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.DetailName = model.DetailName;
        }

        public void DelElement(int id)
        {
            Detail element = source.Details.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Details.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
