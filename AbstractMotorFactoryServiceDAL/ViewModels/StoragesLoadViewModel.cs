using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AbstractMotorFactoryServiceDAL.ViewModels
{
    [DataContract]
    public class StoragesLoadViewModel
    {
        [DataMember]
        public string StorageName { get; set; }

        [DataMember]
        public int TotalNumber { get; set; }

        [DataMember]
        public IEnumerable<Tuple<string, int>> Details { get; set; }
    }
}
