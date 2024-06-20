using NanoidDotNet;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services
{
    public class ServicePaymentService : IServicePaymentService
    {
        private readonly IServicePaymentRepository _servicePaymentService;

        public ServicePaymentService(IServicePaymentRepository servicePaymentService)
        {
            _servicePaymentService = servicePaymentService;
        }

        public ServicePaymentService()
        {
        }

        private string Prefix { get; } = "SP-";

        public async Task CreateServicePayment(ServicePaymentDTO entity)
        {

            var obj = new ServicePayment()
            {
                ServicePaymentId = GenerateId(),
                PaymentDate = entity.PaymentDate ?? DateOnly.FromDateTime(DateTime.Now),
                PaymentMethod = entity.PaymentMethod,
                ServiceOrderId = entity.ServiceOrderId
            };
            await _servicePaymentService.Create(obj);
        }

        public void DeleteServicePayment(ServicePayment entity)
        {
            _servicePaymentService.Delete(entity);
        }

        public async Task<IEnumerable<ServicePayment>> GetAll()
        {
            return await _servicePaymentService.GetAll();
        }

        public async Task UpdateServicePayment(string id, ServicePaymentDTO entity)
        {
            var existingRecord = await _servicePaymentService.GetByCondition(a => a.ServicePaymentId == id);
            if (existingRecord != null)
            {
                existingRecord.PaymentMethod = entity.PaymentMethod;
                existingRecord.PaymentDate = entity.PaymentDate ?? DateOnly.FromDateTime(DateTime.Now);
                existingRecord.ServiceOrderId = entity.ServiceOrderId;
            }
            await _servicePaymentService.Update(existingRecord);
        }

        private string GenerateId()
        {
            var ac = new ServicePayment();
            var born = new ServicePaymentService();
            string id = Nanoid.Generate(size: 8);
            return born.Prefix + id;
        }

        public async Task<ServicePayment?> GetServicePaymentByCondition(Expression<Func<ServicePayment, bool>> expression)
        {
            return await _servicePaymentService.GetByCondition(expression);
        }
    }
}
