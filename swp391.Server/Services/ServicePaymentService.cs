using NanoidDotNet;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services
{
    public class ServicePaymentService
    {
        private readonly IServicePaymentRepository _servicePaymentService;

        public ServicePaymentService(IServicePaymentRepository servicePaymentService)
        {
            _servicePaymentService = servicePaymentService;
        }

        public async Task CreateServicePayment(ServicePaymentDTO entity)
        {

            var obj = new ServicePayment()
            {
                ServicePaymentId = GenerateId(),
                PaymentDate = entity.PaymentDate,
                PaymentMethod = entity.PaymentMethod,
                ServiceOrderId = entity.ServiceOrderId
            };
            await _servicePaymentService.Create(obj);
        }

        public void DeleteServicePayment(ServicePayment entity)
        {
            _servicePaymentService.Delete(entity);
        }

        public async Task<ServicePayment?> GetServicePaymentdByPetCondition(Expression<Func<ServicePayment, bool>> expression)
        {
            return await _servicePaymentService.GetByCondition(expression);
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
                existingRecord.PaymentDate = entity.PaymentDate;
            }
            await _servicePaymentService.Update(existingRecord);
        }

        private string GenerateId()
        {
            var ac = new ServicePayment();
            var born = ac.Prefix;
            string id = Nanoid.Generate(size: 8);
            return born + id;
        }
    }
}
