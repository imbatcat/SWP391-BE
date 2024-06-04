namespace PetHealthcare.Server.APIs.DTOS.Auth
{
    public class RequestResetPassword
    {
        public string UserId {  get; set; }
        public string Token {  get; set; }
        public string NewPassword {  get; set; }
    }
}
