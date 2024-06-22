using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.Server.Core.DTOS
{
    public class InternalAccountDTO
    {
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string UserName { get; set; }

        public int RoleId { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public bool IsMale { get; set; }

        public DateOnly? JoinDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [Phone]
        public string PhoneNumber { get; set; }

        public string? ImgUrl { get; set; }

        public string? Description { get; set; }

        public string? Department { get; set; }

        public string? Position { get; set; }

        public int Experience { get; set; } = 0;
    }
}
