using System.ComponentModel;

namespace AbstractMotorFactoryServiceDAL.BindingModels
{
    public class DetailBindingModel
    {
        public int Id { get; set; }

        [DisplayName("�������� ������")]
        public string DetailName { get; set; }
    }
}
