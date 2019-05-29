using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.ViewModels;
using AbstractMotorFactoryServiceDAL.Attributies;
using System.Collections.Generic;

namespace AbstractMotorFactoryServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с деталями")]
    public interface IDetailService
    {
        [CustomMethod("Метод получения списка деталей")]
        List<DetailViewModel> GetList();

        [CustomMethod("Метод получения детали по id")]
        DetailViewModel GetElement(int id);

        [CustomMethod("Метод добавления детали")]
        void AddElement(DetailBindingModel model);

        [CustomMethod("Метод изменения данных по детали")]
        void UpdElement(DetailBindingModel model);

        [CustomMethod("Метод удаления детали")]
        void DelElement(int id);
    }
}
