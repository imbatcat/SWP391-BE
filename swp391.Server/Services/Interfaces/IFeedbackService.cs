using PetHealthcare.Server.Models;
using PetHealthcareSystem.APIs.DTOS;
using System.Linq.Expressions;
namespace PetHealthcare.Server.Services.Interfaces
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetAllFeedback();
        Task<Feedback?>? GetFeedbackByCondition(Expression<Func<Feedback, bool>> expression);
        Task CreateFeedback(FeedbackDTO Feedback);
        //void deleteFeedback(Feedback Feedback);

    }
}
