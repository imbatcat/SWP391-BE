using PetHealthcareSystem.DTOS;
using PetHealthcareSystem.Models;
using System.Linq.Expressions;

namespace PetHealthcareSystem.Services
{
    public interface IHealthService
    {
        IEnumerable<Service> GetAllHealthService();
        Service? GetHealthServiceByCondition(Expression<Func<Service, bool>> expression);
        void CreateHealthService(int id, double ServicePrice, string ServiceName);
        void UpdateHealthService(int id, double ServicePrice, string ServiceName);
        void DeleteHealthService(int id);
    }
}
