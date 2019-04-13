using System.ComponentModel;

namespace AbstractMotorFactoryServiceDAL.ViewModels
{
    public class CustomerProductionsViewModel
    {
        public string CustomerName { get; set; }

        public string TimeCreate { get; set; }

        public string EngineName { get; set; }

        public int Number { get; set; }

        public decimal Amount { get; set; }

        public string State { get; set; }
    }
}
