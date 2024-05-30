using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services.Interfaces
{
    public interface ITimeSlotService
    {
        Task<IEnumerable<TimeSlot>> GetAllTimeSlots();
        Task<TimeSlot?> GetTimeSlotByCondition(Expression<Func<TimeSlot, bool>> expression);
        Task CreateTimeSlot(TimeslotDTO TimeSlot);
        Task UpdateTimeSlot(int id, TimeslotDTO TimeSlot);
        void DeleteTimeSlot(TimeSlot TimeSlot);
    }
}
