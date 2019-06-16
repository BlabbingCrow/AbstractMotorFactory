using System.ComponentModel.DataAnnotations;

namespace AbstractMotorFactoryModel
{
    public class StorageDetail
    {
        public int Id { get; set; }

        public int StorageId { get; set; }

        public int DetailId { get; set; }

        [Required]
        public int Number { get; set; }

        public virtual Storage Storage { get; set; }

        public virtual Detail Detail { get; set; }
    }
}
