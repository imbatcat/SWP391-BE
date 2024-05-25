using PetHealthcareSystem.Models;
using PetHealthcareSystem.Repositories.Interfaces;
using System.Linq;
using System.Linq.Expressions;

namespace PetHealthcareSystem.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly PetHealthcareDbContext context;

        public FeedbackRepository(PetHealthcareDbContext context)
        {
            this.context = context;
        }

        public void Create(Feedback entity)
        {
            context.Feedbacks.Add(entity);
            SaveChanges();
        }

        public void Delete(Feedback entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Feedback> GetAll()
        {
            return context.Feedbacks.ToList();
        }

        public Feedback? GetByCondition(Expression<Func<Feedback, bool>> expression)
        {
            return context.Feedbacks.LastOrDefault(expression);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Update(Feedback entity)
        {
            throw new NotImplementedException();
        }
        public Feedback? GetFeedbackById(int id)
        {
            return context.Feedbacks.LastOrDefault(a => a.FeedbackId == id);

        }
    }
}
