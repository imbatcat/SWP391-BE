using Ardalis.Specification;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PetHealthcareSystem.APIs.DTOS;
using PetHealthcareSystem.Models;
using PetHealthcareSystem.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcareSystem.Services
{
    public class HealthService : IHealthService
    {
        private readonly IServiceRepository _healthService;
        public HealthService(IServiceRepository healthService) {
            _healthService = healthService;
        }

        public void CreateHealthService(HealthServiceDTO healthService)
        {
            Service toCreateService = new Service { 
                ServiceName = healthService.ServiceName,
                ServicePrice = healthService.ServicePrice,
            };
            _healthService.Create(toCreateService);
        }


        public void DeleteHealthService(Service healthService)
        {
            _healthService.Delete(healthService);
        }

        public IEnumerable<Service> GetAllHealthService()
        {
            return _healthService.GetAll();
        }

        public Service? GetHealthServiceByCondition(Expression<Func<Service, bool>> expression)
        {
                return _healthService.GetByCondition(expression); 
        }

        public void SaveChanges()
        {
            _healthService.SaveChanges();
        }

        public void UpdateHealthService(int id,HealthServiceDTO healthService)
        {
            Service UpdateService = new Service
            {
                ServicePrice = healthService.ServicePrice,
                ServiceId = id,
                ServiceName = healthService.ServiceName,
            };
            _healthService.Update(UpdateService);
        }
    }
}
