using System.ComponentModel;

namespace AbstractMotorFactoryServiceDAL.ViewModels
{
    public class DetailViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название детали")]
        public string DetailName { get; set; }
    }
}
