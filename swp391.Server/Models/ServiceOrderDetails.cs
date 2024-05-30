using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.Server.Models
{
    [PrimaryKey("ServiceId", "ServiceOrderId")]
    public class ServiceOrderDetails
    {
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public string ServiceOrderId { get; set; }
        public ServiceOrder ServiceOrder { get; set; }
    }
}
