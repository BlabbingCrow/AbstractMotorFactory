using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace AbstractMotorFactoryModel
{
    public class Detail
    {
        public int Id { get; set; }

        [Required]
        public string DetailName { get; set; }

        [ForeignKey("DetailId")]
        public virtual List<EngineDetail> EngineDetails { get; set; }

        [ForeignKey("DetailId")]
        public virtual List<StorageDetail> StorageDetails { get; set; }
    }
}
