using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractMotorFactoryServiceDAL.BindingModels
{
    public class CustomerBindingModel
    {
        public int Id { get; set; }

        [DisplayName("ФИО покупателя")]
        public string CustomerFIO { get; set; }
    }
}
