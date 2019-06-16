using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractMotorFactoryModel
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string CustomerFIO { get; set; }

        [ForeignKey("CustomerId")]
        public virtual List<Production> Productions { get; set; }
    }
}
