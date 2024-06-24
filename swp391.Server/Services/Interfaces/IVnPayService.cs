using PetHealthcare.Server.Core.DTOS.AppointmentDTOs;
using PetHealthcare.Server.Models.VNPayModels;

namespace PetHealthcare.Server.Services.Interfaces;
public interface IVnPayService
{
    string CreatePaymentUrl(CreateAppointmentDTO model, HttpContext context);
    PaymentResponseModel PaymentExecute(IQueryCollection collections);
}