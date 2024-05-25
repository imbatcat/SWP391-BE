using Microsoft.CodeAnalysis.CSharp.Syntax;
using PetHealthcareSystem.APIs.DTOS;
using PetHealthcareSystem.DTOS;
using PetHealthcareSystem.Models;
using System.Linq.Expressions;
namespace PetHealthcareSystem._3._Services
{
    public interface IFeedbackService
    {
        IEnumerable<Feedback> GetAllFeedback();
        Feedback? GetFeedbackByCondition(Expression<Func<Feedback, bool>> expression);
        void CreateFeedback(FeedbackDTO Feedback);
       
    }
}
