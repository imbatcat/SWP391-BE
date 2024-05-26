using PetHealthcareSystem.APIs.DTOS;
using PetHealthcareSystem.DTOS;
using PetHealthcareSystem.Models;
using System.Linq.Expressions;

namespace PetHealthcareSystem.Services
{
    public interface IHealthService
    {
        IEnumerable<Service> GetAllHealthService();
        Service? GetHealthServiceByCondition(Expression<Func<Service, bool>> expression);
        void CreateHealthService(HealthServiceDTO healthService);
        void UpdateHealthService(int id,HealthServiceDTO healthService);
        void DeleteHealthService(Service healthService);
    }
}
