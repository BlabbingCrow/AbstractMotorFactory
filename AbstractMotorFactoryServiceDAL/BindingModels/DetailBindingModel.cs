using System.Runtime.Serialization;

namespace AbstractMotorFactoryServiceDAL.BindingModels
{
    [DataContract]
    public class DetailBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string DetailName { get; set; }
    }
}
