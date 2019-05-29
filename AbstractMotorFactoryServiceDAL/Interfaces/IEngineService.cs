using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.ViewModels;
using AbstractMotorFactoryServiceDAL.Attributies;
using System.Collections.Generic;

namespace AbstractMotorFactoryServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с двигателями")]
    public interface IEngineService
    {
        [CustomMethod("Метод получения списка двигателей")]
        List<EngineViewModel> GetList();

        [CustomMethod("Метод получения двигателя по id")]
        EngineViewModel GetElement(int id);

        [CustomMethod("Метод добавления двигателя")]
        void AddElement(EngineBindingModel model);

        [CustomMethod("Метод изменения данных по двигателю")]
        void UpdElement(EngineBindingModel model);

        [CustomMethod("Метод удаления двигателя")]
        void DelElement(int id);
    }
}
