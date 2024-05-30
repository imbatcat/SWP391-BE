using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services.Interfaces
{
    public interface IHealthService
    {
        IEnumerable<Service> GetAllHealthService();
        Service? GetHealthServiceByCondition(Expression<Func<Service, bool>> expression);
        void CreateHealthService(HealthServiceDTO healthService);
        void UpdateHealthService(int id, HealthServiceDTO healthService);
        void DeleteHealthService(Service healthService);
    }
}
