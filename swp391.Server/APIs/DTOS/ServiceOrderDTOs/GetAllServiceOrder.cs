using PetHealthcare.Server.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PetHealthcare.Server.APIs.DTOS.ServiceOrderDTO
{
    public class GetAllServiceOrder
    {
        [Key]
        [Column(TypeName = "char(11)")]
        public string ServiceOrderId { get; set; }
        public double Price { get; set; }
        public DateOnly OrderDate { get; set; }
        public string OrderStatus { get; set; }
    }
}
