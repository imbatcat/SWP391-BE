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

        public void CreateHealthService(int id, double ServicePrice, string ServiceName)
        {
            _healthService.Create(new Service(id, ServicePrice, ServiceName));
            SaveChanges();
        }

        public void DeleteHealthService(int id)
        {
            Service? toDeleteService = _healthService.GetByCondition(d => d.Equals(id));
            if (toDeleteService != null)
            {
                _healthService.Delete(toDeleteService);
            }
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

        public void UpdateHealthService(int id, double ServicePrice, String ServiceName)
        {
            _healthService.Update(new Service(id, ServicePrice, ServiceName));
        }
    }
}
