using System.ComponentModel.DataAnnotations;

namespace AbstractMotorFactoryModel
{
    public class EngineDetail
    {
        public int Id { get; set; }

        public int EngineId { get; set; }

        public int DetailId { get; set; }

        [Required]
        public int Number { get; set; }

        public virtual Engine Engine { get; set; }

        public virtual Detail Detail { get; set; }
    }
}
