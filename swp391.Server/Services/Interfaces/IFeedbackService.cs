using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using System.Linq.Expressions;
namespace PetHealthcare.Server.Services.Interfaces
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetAllFeedback();
        Task<Feedback?>? GetFeedbackByCondition(Expression<Func<Feedback, bool>> expression);
        Task CreateFeedback(FeedbackDTO Feedback);
        //void deleteFeedback(Feedback Feedback);
        Task<IEnumerable<Feedback>> GetFeedbacksByUserName(string cusName);

    }
}
