using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Services.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services
{
    public class Timeslot : ITimeSlotService
    {
        public void CreateTimeSlot(TimeslotDTO TimeSlot)
        {
            throw new NotImplementedException();
        }

        public void DeleteTimeSlot(TimeSlot TimeSlot)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TimeSlot> GetAllTimeSlots()
        {
            throw new NotImplementedException();
        }

        public TimeSlot? GetTimeSlotByCondition(Expression<Func<TimeSlot, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void UpdateTimeSlot(int id, TimeslotDTO TimeSlot)
        {
            throw new NotImplementedException();
        }
    }
}
