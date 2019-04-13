using System;
using System.Collections.Generic;

namespace AbstractMotorFactoryServiceDAL.ViewModels
{
    public class StoragesLoadViewModel
    {
        public string StorageName { get; set; }

        public int TotalNumber { get; set; }

        public IEnumerable<Tuple<string, int>> Details { get; set; }
    }
}
