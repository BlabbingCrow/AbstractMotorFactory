using System.ComponentModel;

namespace AbstractMotorFactoryServiceDAL.ViewModels
{
    public class ProductionViewModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        [DisplayName("ФИО покупателя")]
        public string CustomerFIO { get; set; }

        public int EngineId { get; set; }
        [DisplayName("Название двигателя")]
        public string EngineName { get; set; }
        [DisplayName("Количество")]
        public int Number { get; set; }
        [DisplayName("Стоимость")]
        public decimal Amount { get; set; }
        [DisplayName("Статус заказа")]
        public string State { get; set; }
        [DisplayName("Время создания заказа")]
        public string TimeCreate { get; set; }
        [DisplayName("Время начала выполнения")]
        public string TimeImplement { get; set; }
    }
}
