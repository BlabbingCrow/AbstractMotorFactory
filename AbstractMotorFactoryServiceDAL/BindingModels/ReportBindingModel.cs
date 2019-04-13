using System.Runtime.Serialization;
using System;

namespace AbstractMotorFactoryServiceDAL.BindingModels
{
    [DataContract]
    public class ReportBindingModel
    {
        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public DateTime? TimeFrom { get; set; }

        [DataMember]
        public DateTime? TimeTo { get; set; }
    }
}
