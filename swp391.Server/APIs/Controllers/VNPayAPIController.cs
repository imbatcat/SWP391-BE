using PetHealthcare.Server.Models;
using PetHealthcare.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetHealthcare.Server.APIs.DTOS.AppointmentDTOs;
using System.Diagnostics;
using Microsoft.Identity.Client;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PetHealthcare.Server.APIs.Constant;
namespace PetHealthcare.Server.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VNPayAPIController : ControllerBase
    {
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;
        private readonly AppointmentService _appointmentService;
        private readonly PetHealthcareDbContext context;
        private readonly BookingPaymentService bookingPaymentService;
        public VNPayAPIController(IVnPayService vnPayService, PetHealthcareDbContext context, AppointmentService appointmentService, 
            BookingPaymentService _bookingPaymentService, ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _vnPayService = vnPayService;
            this.context = context;
            _appointmentService = appointmentService;
            bookingPaymentService = _bookingPaymentService;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
        }
        private ITempDataDictionary TempData => _tempDataDictionaryFactory.GetTempData(HttpContext);
        private readonly IVnPayService _vnPayService;
        // GET: VNPayController
        [HttpPost]
        public IActionResult CreatePaymentUrl([FromBody] CreateAppointmentDTO model)
        {
            CreateAppointmentDTO appointmentDTO = model;
            TempData["AppointmentDTO"] = JsonSerializer.Serialize(appointmentDTO);
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
            
            
            return Ok(url);
        }

        [HttpPost("PaymentCallback")]
        public async Task<IActionResult> PaymentCallback([FromForm] IFormCollection form)
        {
            var queury = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(string.Join("&", form.Select(x => $"{x.Key}={x.Value}")));
            var response = _vnPayService.PaymentExecute(new QueryCollection(queury));
            CreateAppointmentDTO appointmentDTO = new CreateAppointmentDTO();
            try
            {
                if (TempData.ContainsKey("AppointmentDTO"))
                {
                    var appointmentJson = TempData["AppointmentDTO"].ToString();
                    appointmentDTO = JsonSerializer.Deserialize<CreateAppointmentDTO>(appointmentJson);
                }
                else
                {
                    Debug.WriteLine("No appointment data in TempData");
                    return BadRequest("No appointment data");
                }
                if (response.VnPayResponseCode.Equals("00"))
                {
                    string appointmentId = _appointmentService.GenerateId();
                    await _appointmentService.CreateAppointment(appointmentDTO, appointmentId);
                    Debug.WriteLine(appointmentId);
                    if(context.Appointments.Find(appointmentId) != null)
                    {
                        var bookingPayment = new BookingPayment
                        {
                            PaymentId = bookingPaymentService.GenerateBookingPaymentId(),
                            PaymentMethod = "VNPay",
                            PaymentDate = response.PaymentDate,
                            Price = ProjectConstant.DEPOSIT_COST,
                            AppointmentId = appointmentId,
                        };
                        context.BookingPayments.Add(bookingPayment);
                        
                    }
                    else
                    {
                        Debug.WriteLine("Add appointment failed");
                    }
                    
                } else
                {
                    Debug.WriteLine("Transaction failed" + response.VnPayResponseCode);
                }
                
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
