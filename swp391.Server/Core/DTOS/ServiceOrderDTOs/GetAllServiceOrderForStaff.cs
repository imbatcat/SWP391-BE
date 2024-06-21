namespace PetHealthcare.Server.Core.DTOS.ServiceOrderDTOs
{
    public class GetAllServiceOrderForStaff
    {
        public string ServiceOrderId { get; set; }
        public double Price { get; set; }
        public DateOnly OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string customerName { get; set; }
    }
}
