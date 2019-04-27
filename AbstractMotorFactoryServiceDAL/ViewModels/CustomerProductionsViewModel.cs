using System.Runtime.Serialization;
using System.ComponentModel;

namespace AbstractMotorFactoryServiceDAL.ViewModels
{
    [DataContract]
    public class CustomerProductionsViewModel
    {
        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string TimeCreate { get; set; }

        [DataMember]
        public string EngineName { get; set; }

        [DataMember]
        public int Number { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public string State { get; set; }
    }
}
