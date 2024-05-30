using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PetHealthcare.Server.Models
{
    [Index(nameof(Username), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(PhoneNumber), IsUnique = true)]
    public class Account
    {
        [Key]
        [Column(TypeName = "char(11)")]
        public string AccountId { get; set; }

        [StringLength(20)]
        [Required]
        public string Username { get; set; }

        [StringLength(50)]
        [Required]
        public string FullName { get; set; }

        [StringLength(16)]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Required]
        public bool IsMale { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [Required]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateOnly DateOfBirth { get; set; }

        [JsonConverter(typeof(DateOnlyConverter))]
        [DataType(DataType.Date)]
        [Required]
        public DateOnly JoinDate { get; set; }

        [DefaultValue(false)]
        public bool IsDisabled { get; set; }

        // Reference entities 
        [DefaultValue(1)]
        public int RoleId { get; set; } = 1;
        public Role AccountRole { get; set; }

        public virtual ICollection<Appointment>? Appointments { get; set; }

        public virtual ICollection<Feedback>? Feedbacks { get; set; }

        public virtual ICollection<Pet>? Pets { get; set; }
    }
}
