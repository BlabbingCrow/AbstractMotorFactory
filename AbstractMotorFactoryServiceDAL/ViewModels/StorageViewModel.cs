using System.ComponentModel;
using System.Collections.Generic;

namespace AbstractMotorFactoryServiceDAL.ViewModels
{
    public class StorageViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название склада")]
        public string StorageName { get; set; }
        [DisplayName("Детали на складе")]
        public List<StorageDetailViewModel> StorageDetails { get; set; }
    }
}
