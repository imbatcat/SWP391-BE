using Microsoft.EntityFrameworkCore;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Repositories
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
                // this line ensures efcore to update the table.
                context.Entry(service).State = EntityState.Modified;

                service.ServicePrice = entity.ServicePrice;
                service.ServiceName = entity.ServiceName;
                SaveChanges();
            }
        }
    }
}
