using PetHealthcare.Server.Models;

namespace PetHealthcare.Server.Repositories.Interfaces
{
    public interface ITimeslotRepository : IRepositoryBase<TimeSlot>
    {
        IEnumerable<TimeSlot> GetSlots();
        TimeSlot GetSlotById(int timeId);

    }
}
