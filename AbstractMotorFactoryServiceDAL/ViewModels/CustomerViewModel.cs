using System.ComponentModel;

namespace AbstractMotorFactoryServiceDAL.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        [DisplayName("ФИО покупателя")]
        public string CustomerFIO { get; set; }
    }
}
