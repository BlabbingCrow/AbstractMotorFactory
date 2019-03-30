using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractMotorFactoryModel
{
    public class Engine
    {
        public int Id { get; set; }

        [Required]
        public string EngineName { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [ForeignKey("EngineId")]
        public virtual List<EngineDetail> EngineDetails { get; set; }

        [ForeignKey("EngineId")]
        public virtual List<Production> Productions { get; set; }
    }
}
