using PetHealthcare.Server.APIs.DTOS.AppointmentDTOs;
using PetHealthcare.Server.Models;

namespace PetHealthcare.Server.Services;
public interface IVnPayService
{
    string CreatePaymentUrl(AppointmentDTO model, HttpContext context);
    PaymentResponseModel PaymentExecute(IQueryCollection collections);
}