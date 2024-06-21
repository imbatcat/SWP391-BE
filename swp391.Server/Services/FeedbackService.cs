using PetHealthcare.Server.Core.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services.Interfaces;
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
        public async Task CreateFeedback(FeedbackDTO Feedback)
        {
            var newFeedback = new Feedback
            {
                Ratings = Feedback.Rating,
                FeedbackDetails = Feedback.FeedbackDetails,
                AccountId = Feedback.AccountId
            };
            await _feedbackService.Create(newFeedback);

        }

        public void deleteFeedback(Feedback Feedback)
        {
            _feedbackService?.Delete(Feedback);
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedback()
        {
            return await _feedbackService.GetAll();
        }

        public async Task<Feedback?> GetFeedbackByCondition(Expression<Func<Feedback, bool>> expression)
        {
            return await _feedbackService.GetByCondition(expression);
        }

        public async Task<IEnumerable<Feedback>> GetFeedbacksByUserName(string cusName)
        {
            return await _feedbackService.GetFeedbacksByUserName(cusName);
        }
    }
}