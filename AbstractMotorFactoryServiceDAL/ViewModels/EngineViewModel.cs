using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace AbstractMotorFactoryServiceDAL.ViewModels
{
    [DataContract]
    public class EngineViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Название двигателя")]
        public string EngineName { get; set; }

        [DataMember]
        [DisplayName("Стоимость")]
        public decimal Cost { get; set; }

        [DataMember]
        public List<EngineDetailViewModel> EngineDetails { get; set; }
    }
}
