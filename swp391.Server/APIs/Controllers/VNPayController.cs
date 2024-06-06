using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetHealthcare.Server.Services;

namespace PetHealthcare.Server.APIs.Controllers
{
    public class VNPayController : Controller
    {
        // GET: VNPayController
        private readonly IVnPayService _vnPayService;
        private readonly PetHealthcareDbContext context;

        public VNPayController(IVnPayService vnPayService, PetHealthcareDbContext context)
        {
            _vnPayService = vnPayService;
            this.context = context;
        }

        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            // TODO: kiem tra hash


            context.paymentResponseModels.Add(response);
            context.SaveChanges();
            return Ok(response);
        }
    }
}
