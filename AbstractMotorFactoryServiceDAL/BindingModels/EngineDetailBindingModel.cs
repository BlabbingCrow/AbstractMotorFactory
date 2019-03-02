using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractMotorFactoryServiceDAL.BindingModels
{
    public class EngineDetailBindingModel
    {
        public int Id { get; set; }

        public int EngineId { get; set; }

        public int DetailId { get; set; }

        public int Number { get; set; }
    }
}
