using PetHealthcare.Server.Models;
using PetHealthcare.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetHealthcare.Server.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VNPayAPIController : ControllerBase
    {
        private readonly PetHealthcareDbContext context;
        public VNPayAPIController(IVnPayService vnPayService, PetHealthcareDbContext context)
        {
            _vnPayService = vnPayService;
            this.context = context;
        }

        private readonly IVnPayService _vnPayService;
        // GET: VNPayController
        [HttpPost]
        public IActionResult CreatePaymentUrl([FromBody] PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Ok(url);
        }

        [HttpPost("PaymentCallback")]
        public IActionResult PaymentCallback([FromForm] IFormCollection form)
        {
            var queury = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(string.Join("&", form.Select(x => $"{x.Key}={x.Value}")));
            var response = _vnPayService.PaymentExecute(new QueryCollection(queury));
            // TODO: kiem tra hash
            try
            {
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
