using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.Server.Models.VNPayModels;

public class PaymentResponseModel
{
    public string OrderDescription { get; set; }

    [Key]
    public string TransactionId { get; set; }
    public string OrderId { get; set; }
    public string PaymentMethod { get; set; }
    public string PaymentId { get; set; }
    public bool Success { get; set; }
    public string Token { get; set; }
    public string VnPayResponseCode { get; set; }

    public DateOnly PaymentDate { get; set; }
}