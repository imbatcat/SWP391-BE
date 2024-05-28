using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services
{
    public class HealthService : IHealthService
    {
        private readonly IServiceRepository _healthService;
        public HealthService(IServiceRepository healthService)
        {
            _healthService = healthService;
        }



        public void DeleteHealthService(Service healthService)
        {
            _healthService.Delete(healthService);
        }

        public void SaveChanges()
        {
            _healthService.SaveChanges();
        }


        public async Task<IEnumerable<Service>> GetAllHealthService()
        {
            return await _healthService.GetAll();
        }

        public async Task<Service?> GetHealthServiceByCondition(Expression<Func<Service, bool>> expression)
        {
            return await _healthService.GetByCondition(expression);
        }

        public async Task CreateHealthService(HealthServiceDTO healthService)
        {
            Service toCreateService = new Service
            {
                ServiceName = healthService.ServiceName,
                ServicePrice = healthService.ServicePrice,
            };
            await _healthService.Create(toCreateService);
        }

        public async Task UpdateHealthService(int id, HealthServiceDTO healthService)
        {
            Service UpdateService = new Service
            {
                ServicePrice = healthService.ServicePrice,
                ServiceId = id,
                ServiceName = healthService.ServiceName,
            };
            await _healthService.Update(UpdateService);
        }

    }
}
