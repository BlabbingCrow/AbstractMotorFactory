using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractMotorFactoryServiceDAL.BindingModels
{
    public class ProductionBindingModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int EngineId { get; set; }

        public int Number { get; set; }

        public decimal Amount { get; set; }
    }
}
