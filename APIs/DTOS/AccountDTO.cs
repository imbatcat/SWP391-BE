namespace PetHealthcareSystem.DTOS
{
    public class AccountDTO
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsMale { get; set; }

        public string PhoneNumber { get; set; }

        public DateOnly DateOfBirth { get; set; }

    }
}
