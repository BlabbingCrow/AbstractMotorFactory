using System;

namespace AbstractMotorFactoryModel
{
    public class Production
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int EngineId { get; set; }

        public int Number { get; set; }

        public decimal Amount { get; set; }

        public ProductionStatus State { get; set; }

        public DateTime TimeCreate { get; set; }

        public DateTime? TimeImplement { get; set; }
    }
}
