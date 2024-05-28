using Microsoft.EntityFrameworkCore;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly PetHealthcareDbContext context;

        public FeedbackRepository(PetHealthcareDbContext context)
        {
            this.context = context;
        }

        public async Task Create(Feedback entity)
        {
           await context.Feedbacks.AddAsync(entity);
           await SaveChanges();
        }

        public void Delete(Feedback entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Feedback>> GetAll()
        {
            return await context.Feedbacks.ToListAsync();
        }

        public async Task <Feedback?> GetByCondition(Expression<Func<Feedback, bool>> expression)
        {
            return await context.Feedbacks.LastOrDefaultAsync(expression);
        }

        public async Task SaveChanges()
        {
           await context.SaveChangesAsync();
        }
        public async Task< Feedback?> GetFeedbackById(int id)
        {
            return await context.Feedbacks.LastOrDefaultAsync(a => a.FeedbackId == id);

        }

        Task IRepositoryBase<Feedback>.Update(Feedback entity)
        {
            throw new NotImplementedException();
        }
    }
}