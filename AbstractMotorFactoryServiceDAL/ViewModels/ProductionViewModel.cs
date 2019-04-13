using System.ComponentModel;
using System.Runtime.Serialization;

namespace AbstractMotorFactoryServiceDAL.ViewModels
{
    [DataContract]
    public class ProductionViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int CustomerId { get; set; }

        [DataMember]
        [DisplayName("ФИО покупателя")]
        public string CustomerFIO { get; set; }

        [DataMember]
        public int EngineId { get; set; }

        [DataMember]
        [DisplayName("Название двигателя")]
        public string EngineName { get; set; }

        [DataMember]
        [DisplayName("Количество")]
        public int Number { get; set; }

        [DataMember]
        [DisplayName("Стоимость")]
        public decimal Amount { get; set; }

        [DataMember]
        [DisplayName("Статус заказа")]
        public string State { get; set; }

        [DataMember]
        [DisplayName("Время создания заказа")]
        public string TimeCreate { get; set; }

        [DataMember]
        [DisplayName("Время начала выполнения")]
        public string TimeImplement { get; set; }
    }
}
