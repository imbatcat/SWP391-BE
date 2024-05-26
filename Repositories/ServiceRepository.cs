using Microsoft.EntityFrameworkCore;
using PetHealthcareSystem.Models;
using PetHealthcareSystem.Repositories.Interfaces;
using System.Linq;
using System.Linq.Expressions;

namespace PetHealthcareSystem.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly PetHealthcareDbContext context;
        public ServiceRepository(PetHealthcareDbContext context)
        {
            this.context = context;
        }
        public void Create(Service entity)
        {
            context.Services.Add(entity);
            SaveChanges();
        }

        public void Delete(Service entity)
        {

            context.Services.Remove(entity);
            SaveChanges();
        }

        public IEnumerable<Service> GetAll()
        {
            return context.Services.ToList();
        }

        public Service? GetByCondition(Expression<Func<Service, bool>> expression)
        {
            return context.Services.FirstOrDefault(expression);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Update(Service entity)
        {
            var service = GetByCondition(e => e.ServiceId == entity.ServiceId);
            if (service != null)
            {
                
                service.ServicePrice = entity.ServicePrice;
                service.ServiceName = entity.ServiceName;
                SaveChanges();
            }
        }
    }
}
