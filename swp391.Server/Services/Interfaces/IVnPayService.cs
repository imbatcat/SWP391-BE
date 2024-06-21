using PetHealthcare.Server.Core.DTOS.AppointmentDTOs;
using PetHealthcare.Server.Models;

namespace PetHealthcare.Server.Services;
public interface IVnPayService
{
    string CreatePaymentUrl(CreateAppointmentDTO model, HttpContext context);
    PaymentResponseModel PaymentExecute(IQueryCollection collections);
}