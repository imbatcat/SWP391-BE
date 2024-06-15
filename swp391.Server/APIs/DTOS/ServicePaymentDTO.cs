using PetHealthcare.Server.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PetHealthcare.Server.APIs.DTOS
{
    public class ServicePaymentDTO
    {
        //public required string ServicePaymentId { get; set; }
        public double ServicePrice { get; set; }
        public DateOnly PaymentDate { get; set; }
        public required string? PaymentMethod { get; set; }
        public required string? ServiceOrderId { get; set; }
    }
}
