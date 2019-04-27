using System.Runtime.Serialization;

namespace AbstractMotorFactoryServiceDAL.BindingModels
{
    [DataContract]
    public class EngineDetailBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int EngineId { get; set; }

        [DataMember]
        public int DetailId { get; set; }

        [DataMember]
        public int Number { get; set; }
    }
}
