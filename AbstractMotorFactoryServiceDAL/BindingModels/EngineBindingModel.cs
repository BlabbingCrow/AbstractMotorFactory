using System.Collections.Generic;


namespace AbstractMotorFactoryServiceDAL.BindingModels
{
    public class EngineBindingModel
    {
        public int Id { get; set; }

        public string EngineName { get; set; }

        public decimal Cost { get; set; }

        public List<EngineDetailBindingModel> EngineDetails { get; set; }
    }
}
