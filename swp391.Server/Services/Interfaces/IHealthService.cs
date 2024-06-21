using PetHealthcare.Server.Core.DTOS;
using PetHealthcare.Server.Models;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services.Interfaces
{
    public interface IHealthService
    {
        Task<IEnumerable<Service>> GetAllHealthService();
        Task<Service?> GetHealthServiceByCondition(Expression<Func<Service, bool>> expression);
        Task CreateHealthService(HealthServiceDTO healthService);
        Task UpdateHealthService(int id, HealthServiceDTO healthService);
        void DeleteHealthService(Service healthService);
    }
}
