using System.ComponentModel;

namespace AbstractMotorFactoryServiceDAL.BindingModels
{
    public class CustomerBindingModel
    {
        public int Id { get; set; }

        [DisplayName("ФИО покупателя")]
        public string CustomerFIO { get; set; }
    }
}
