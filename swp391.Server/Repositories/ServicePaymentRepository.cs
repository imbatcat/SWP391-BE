using Microsoft.EntityFrameworkCore;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Repositories
{
    public class ServicePaymentRepository : IServicePaymentRepository
    {
        private readonly PetHealthcareDbContext _context;

        public async Task Create(ServicePayment entity)
        {
            await _context.ServicePayments.AddAsync(entity);
            await SaveChanges();
        }

        public void Delete(ServicePayment entity)
        {
            _context.ServicePayments.Remove(entity);
        }

        public async Task<IEnumerable<ServicePayment>> GetAll()
        {
            return await _context.ServicePayments.OrderBy(p => p.ServicePaymentId).ToListAsync();
        }

        public async Task<ServicePayment?> GetByCondition(Expression<Func<ServicePayment, bool>> expression)
        {
            return await _context.ServicePayments.FirstOrDefaultAsync(expression);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(ServicePayment entity)
        {
            var shii = await GetByCondition(e => e.ServicePaymentId == entity.ServicePaymentId);
            if (shii != null)
            {
                _context.Entry(shii).State = EntityState.Modified;
                shii.ServicePaymentId = entity.ServicePaymentId;
                shii.PaymentDate = entity.PaymentDate;
                shii.PaymentMethod = entity.PaymentMethod;
                await SaveChanges();
            }
        }
    }
}
