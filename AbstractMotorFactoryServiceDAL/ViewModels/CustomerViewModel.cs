using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AbstractMotorFactoryServiceDAL.ViewModels
{
    [DataContract]
    public class CustomerViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Mail { get; set; }

        [DataMember]
        [DisplayName("ФИО покупателя")]
        public string CustomerFIO { get; set; }

        [DataMember]
        public List<MessageInfoViewModel> Messages { get; set; }
    }
}
