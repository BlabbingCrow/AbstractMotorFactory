using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.ViewModels;
using System.Collections.Generic;

namespace AbstractMotorFactoryServiceDAL.Interfaces
{
    public interface IMessageInfoService
    {
        List<MessageInfoViewModel> GetList();

        MessageInfoViewModel GetElement(int id);

        void AddElement(MessageInfoBindingModel model);
    }
}
