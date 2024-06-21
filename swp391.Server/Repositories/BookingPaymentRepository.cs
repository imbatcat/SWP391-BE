using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Repositories
{
    public class BookingPaymentRepository : IBookingPaymentRepository
    {
        private readonly PetHealthcareDbContext context;
        public BookingPaymentRepository(PetHealthcareDbContext context)
        {
            this.context = context;
        }
        public async Task Create(BookingPayment entity)
        {
            context.BookingPayments.Add(entity);
            await SaveChanges();
        }

        public void Delete(BookingPayment entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookingPayment>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<BookingPayment?> GetByCondition(Expression<Func<BookingPayment, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public Task Update(BookingPayment entity)
        {
            throw new NotImplementedException();
        }
    }
}
