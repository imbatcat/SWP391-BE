using PetHealthcare.Server.Models;
using PetHealthcare.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetHealthcare.Server.APIs.DTOS.AppointmentDTOs;
using System.Diagnostics;
using Microsoft.Identity.Client;

namespace PetHealthcare.Server.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VNPayAPIController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;
        private AppointmentDTO appointmentDTO = new AppointmentDTO();
        private readonly PetHealthcareDbContext context;
        private readonly BookingPaymentService bookingPaymentService;
        public VNPayAPIController(IVnPayService vnPayService, PetHealthcareDbContext context, AppointmentService appointmentService)
        {
            _vnPayService = vnPayService;
            this.context = context;
            _appointmentService = appointmentService;
            appointmentDTO = new AppointmentDTO();
        }

        private readonly IVnPayService _vnPayService;
        // GET: VNPayController
        [HttpPost]
        public IActionResult CreatePaymentUrl([FromBody] AppointmentDTO model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
            appointmentDTO = model;
            
            return Ok(url);
        }

        [HttpPost("PaymentCallback")]
        public IActionResult PaymentCallback([FromForm] IFormCollection form)
        {
            var queury = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(string.Join("&", form.Select(x => $"{x.Key}={x.Value}")));
            var response = _vnPayService.PaymentExecute(new QueryCollection(queury));
            try
            {
                if (response.VnPayResponseCode.Equals("00"))
                {
                    string appointmentId = _appointmentService.GenerateId();
                    _appointmentService.CreateAppointment(appointmentDTO, appointmentId);
                    if(context.Appointments.Find(appointmentId) != null)
                    {
                        var bookingPayment = new BookingPayment
                        {
                            PaymentId = bookingPaymentService.GenerateBookingPaymentId(),
                            PaymentMethod = "VNPay",
                            PaymentDate = response.PaymentDate,
                            Price = appointmentDTO.BookingPrice,
                            AppointmentId = appointmentId,
                        };
                        context.BookingPayments.Add(bookingPayment);
                    }else
                    {
                        Debug.WriteLine("Add appointment failed");
                    }
                    
                }
            
                context.paymentResponseModels.Add(response);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return Ok(response);
        }
    }
}
