using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PetHealthcare.Server.Models
{
    public class ServicePayment
    {
        public string Prefix { get; } = "SP";

        [Key]
        [Column(TypeName = "char(11)")]
        public string ServicePaymentId { get; set; }

        [JsonConverter(typeof(DateOnlyConverter))]
        [DataType(DataType.Date)]
        public DateOnly PaymentDate { get; set; }

        [StringLength(20)]
        public string PaymentMethod { get; set; }

        // Reference entities
        [ForeignKey("ServiceOrderId")]
        public string ServiceOrderId { get; set; }
        public ServiceOrder ServiceOrder { get; set; }
    }
}
