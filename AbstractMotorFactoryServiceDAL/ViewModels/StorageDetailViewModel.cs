using System.ComponentModel;
using System.Runtime.Serialization;

namespace AbstractMotorFactoryServiceDAL.ViewModels
{
    [DataContract]
    public class StorageDetailViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int StorageId { get; set; }

        [DataMember]
        public int DetailId { get; set; }

        [DataMember]
        [DisplayName("Название детали")]
        public string DetailName { get; set; }

        [DataMember]
        [DisplayName("Количество")]
        public int Number { get; set; }
    }
}
