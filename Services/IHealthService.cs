using PetHealthcareSystem.DTOS;
using PetHealthcareSystem.Models;
using System.Linq.Expressions;

namespace PetHealthcareSystem.Services
{
    public interface IHealthService
    {
        IEnumerable<Service> GetAllHealthService();
        Service? GetHealthServiceByCondition(Expression<Func<Service, bool>> expression);
        void CreateHealthService(Service healthService);
        void UpdateHealthService(Service healthService);
        void DeleteHealthService(Service healthService);
    }
}
