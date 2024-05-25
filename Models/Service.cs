using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetHealthcareSystem.Models
{
    public class Service
    {
        public Service(int serviceId, double servicePrice, string serviceName)
        {
            ServiceId = serviceId;
            ServicePrice = servicePrice;
            ServiceName = serviceName;
            ServiceOrders = new HashSet<ServiceOrder>();
        }
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
