using PetHealthcare.Server.Models;

namespace PetHealthcare.Server.Repositories.Interfaces
{
    public interface ITimeslotRepository : IRepositoryBase<TimeSlot>
    {
        Task<IEnumerable<TimeSlot>> GetSlots();
        Task<TimeSlot?> GetSlotById(int timeId);

    }
}
