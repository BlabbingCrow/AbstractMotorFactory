namespace AbstractMotorFactoryServiceDAL.BindingModels
{
    public class CustomerBindingModel
    {
        public int Id { get; set; }

        [DisplayName("��� ����������")]
        public string CustomerFIO { get; set; }
    }
}
