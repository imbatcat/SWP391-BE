using PetHealthcare.Server.Models;

namespace PetHealthcare.Server.Repositories.Interfaces
{
    public interface IFeedbackRepository : IRepositoryBase<Feedback>
    {
        Task<IEnumerable<Feedback>> GetFeedbacksByUserName(string cusName);
    }
}
