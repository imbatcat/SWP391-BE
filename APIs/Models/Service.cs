using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetHealthcareSystem.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }

        [DataType(DataType.Currency)]
        public double ServicePrice { get; set; }

        [StringLength(50)]
        public string ServiceName { get; set; }

        // Reference entities
        public virtual ICollection<ServiceOrder> ServiceOrders { get; set; }
    }
}
