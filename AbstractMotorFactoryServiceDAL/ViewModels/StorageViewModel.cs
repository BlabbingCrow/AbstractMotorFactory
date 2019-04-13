using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AbstractMotorFactoryServiceDAL.ViewModels
{
    [DataContract]
    public class StorageViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Название склада")]
        public string StorageName { get; set; }

        [DataMember]
        [DisplayName("Детали на складе")]
        public List<StorageDetailViewModel> StorageDetails { get; set; }
    }
}
