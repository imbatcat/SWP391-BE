using PetHealthcare.Server.Models;
using PetHealthcareSystem.APIs.DTOS;
using System.Linq.Expressions;
namespace PetHealthcare.Server.Services.Interfaces
{
    public interface IFeedbackService
    {
        IEnumerable<Feedback> GetAllFeedback();
        Feedback? GetFeedbackByCondition(Expression<Func<Feedback, bool>> expression);
        void CreateFeedback(FeedbackDTO Feedback);

    }
}
