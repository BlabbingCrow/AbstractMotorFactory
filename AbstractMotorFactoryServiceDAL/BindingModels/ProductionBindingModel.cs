namespace AbstractMotorFactoryServiceDAL.BindingModels
{
    public class ProductionBindingModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int EngineId { get; set; }

        public int Number { get; set; }

        public decimal Amount { get; set; }
    }
}
