using System.ComponentModel;

namespace AbstractMotorFactoryServiceDAL.BindingModels
{
    public class StorageBindingModel
    {
        public int Id { get; set; }

        [DisplayName("Название склада")]
        public string StorageName { get; set; }
    }
}
