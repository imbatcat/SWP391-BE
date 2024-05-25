using PetHealthcareSystem._3._Services;
using PetHealthcareSystem.APIs.DTOS;
using PetHealthcareSystem.Models;
using PetHealthcareSystem.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcareSystem._3._Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackService;
        public FeedbackService(IFeedbackRepository feedbackService)
        {
            _feedbackService = feedbackService;
        }
        public void CreateFeedback(FeedbackDTO Feedback)
        {
            var newFeedback = new Feedback
            {
                Ratings = Feedback.Rating,
                FeedbackDetails = Feedback.FeedbackDetails,
                AccountId = Feedback.AccountId
            };
            _feedbackService.Create(newFeedback);

        }

        public IEnumerable<Feedback> GetAllFeedback()
        {
            return _feedbackService.GetAll();
        }

        public Feedback? GetFeedbackByCondition(Expression<Func<Feedback, bool>> expression)
        {
            return _feedbackService.GetByCondition(expression);
        }
    }
}
