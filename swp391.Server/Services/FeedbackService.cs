using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services.Interfaces;
using PetHealthcareSystem.APIs.DTOS;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services
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

        public void deleteFeedback(Feedback Feedback)
        {
           _feedbackService?.Delete(Feedback);
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