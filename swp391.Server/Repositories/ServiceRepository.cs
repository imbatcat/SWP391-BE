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

        public void Delete(Service entity)
        {

            context.Services.Remove(entity);
            SaveChanges();
        }

        public async Task<IEnumerable<Service>> GetAll()
        {
            return await context.Services.ToListAsync();
        }

        public async Task<Service?> GetByCondition(Expression<Func<Service, bool>> expression)
        {
            return await context.Services.FirstOrDefaultAsync(expression);
        }

        public async Task Create(Service entity)
        {
            await context.Services.AddAsync(entity);
            await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(Service entity)
        {
            var service = await GetByCondition(e => e.ServiceId == entity.ServiceId);
            if (service != null)
            {
                // this line ensures efcore to update the table.
                context.Entry(service).State = EntityState.Modified;

                service.ServicePrice = entity.ServicePrice;
                service.ServiceName = entity.ServiceName;
                await SaveChanges();
            }
        }
    }
}
