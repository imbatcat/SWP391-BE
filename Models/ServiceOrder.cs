using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PetHealthcareSystem.Models
{
    public class ServiceOrder
    {
        [NotMapped]
        public string Prefix { get; } = "SR";

        [Key]
        [Column(TypeName = "char(11)")]
        public string ServiceOrderId { get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [JsonConverter(typeof(DateOnlyConverter))]
        [DataType(DataType.Date)]
        public DateOnly OrderDate { get; set; }

        [StringLength(50)]
        public string OrderStatus { get; set; }

        // Reference entities
        [Required]
        public MedicalRecord MedicalRecord { get; set; }

        //public virtual ICollection<ServiceOrderDetail> ServiceOrderDetails { get; set; }
        public virtual ICollection<Service> Services { get; set; }

        public ServicePayment ServicePayment { get; set; }
    }
}
