using System.Collections.Generic;
using System.ComponentModel;

namespace AbstractMotorFactoryServiceDAL.ViewModels
{
    public class EngineViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название двигателя")]
        public string EngineName { get; set; }
        [DisplayName("Стоимость")]
        public decimal Cost { get; set; }

        public List<EngineDetailViewModel> EngineDetails { get; set; }
    }
}
