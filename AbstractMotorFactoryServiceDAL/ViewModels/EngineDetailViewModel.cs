using System.ComponentModel;

namespace AbstractMotorFactoryServiceDAL.ViewModels
{
    public class EngineDetailViewModel
    {
        public int Id { get; set; }

        public int EngineId { get; set; }

        public int DetailId { get; set; }
        [DisplayName("Название детали")]
        public string DetailName { get; set; }
        [DisplayName("Количество")]
        public int Number { get; set; }
    }
}
