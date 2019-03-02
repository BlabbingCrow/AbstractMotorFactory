using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractMotorFactoryServiceDAL.BindingModels
{
    public class EngineBindingModel
    {
        public int Id { get; set; }

        public string EngineName { get; set; }

        public decimal Cost { get; set; }

        public List<EngineDetailBindingModel> EngineDetails { get; set; }
    }
}
