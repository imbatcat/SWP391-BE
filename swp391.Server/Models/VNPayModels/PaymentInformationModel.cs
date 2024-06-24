using System.ComponentModel.DataAnnotations.Schema;

namespace PetHealthcare.Server.Models.VNPayModels;

[NotMapped]
public class PaymentInformationModel
{
    public string OrderType { get; set; }
    public double Amount { get; set; }
    public string OrderDescription { get; set; }
    public string Name { get; set; }
}