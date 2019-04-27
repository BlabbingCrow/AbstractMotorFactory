using System.ComponentModel;
using System.Runtime.Serialization;

namespace AbstractMotorFactoryServiceDAL.ViewModels
{
    [DataContract]
    public class CustomerViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("ФИО покупателя")]
        public string CustomerFIO { get; set; }
    }
}
