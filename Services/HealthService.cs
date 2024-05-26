using Ardalis.Specification;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        public void CreateHealthService(Service healthService)
        {
           _healthService.Create(healthService);
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

        public void UpdateHealthService(Service healthService)
        {
            _healthService.Update(healthService);
        }
    }
}
