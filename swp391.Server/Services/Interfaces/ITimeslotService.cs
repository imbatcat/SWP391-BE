using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services.Interfaces
{
    public interface ITimeSlotService
    {
        IEnumerable<TimeSlot> GetAllTimeSlots();
        TimeSlot? GetTimeSlotByCondition(Expression<Func<TimeSlot, bool>> expression);
<<<<<<< Updated upstream
        void CreateTimeSlot(TimeslotDTO TimeSlot);
        void UpdateTimeSlot(int id, TimeslotDTO TimeSlot);
=======
        Task CreateTimeSlot(TimeslotDTO TimeSlot);
        Task UpdateTimeSlot(int id, TimeslotDTO TimeSlot);
>>>>>>> Stashed changes
        void DeleteTimeSlot(TimeSlot TimeSlot);
    }
}