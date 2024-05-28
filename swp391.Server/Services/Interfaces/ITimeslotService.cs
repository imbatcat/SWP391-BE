using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services.Interfaces
{
    public interface ITimeSlotService
    {
        IEnumerable<TimeSlot> GetAllTimeSlots();
        TimeSlot? GetTimeSlotByCondition(Expression<Func<TimeSlot, bool>> expression);
        void CreateTimeSlot(TimeslotDTO TimeSlot);
        void UpdateTimeSlot(int id, TimeslotDTO TimeSlot);
        void DeleteTimeSlot(TimeSlot TimeSlot);
    }
}
