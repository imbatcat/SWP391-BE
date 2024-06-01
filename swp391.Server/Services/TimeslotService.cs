using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services
{
    public class TimeslotService : ITimeSlotService
    {
        private readonly ITimeslotRepository _timeSlotService;

        public TimeslotService(ITimeslotRepository timeSlotService)
        {
            _timeSlotService = timeSlotService;
        }
        public async Task CreateTimeSlot(TimeslotDTO timeSlot)
        {
            var _timeSlot = new TimeSlot
            {

                StartTime = timeSlot.StartTime,
                EndTime = timeSlot.EndTime,

            };
            await _timeSlotService.Create(_timeSlot);
        }

        public void DeleteTimeSlot(TimeSlot TimeSlot)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TimeSlot>> GetAllTimeSlots()
        {
            return await _timeSlotService.GetAll();
        }

        public async Task<TimeSlot?> GetTimeSlotByCondition(Expression<Func<TimeSlot, bool>> expression)
        {
            return await _timeSlotService.GetByCondition(expression);
        }

        public async Task UpdateTimeSlot(int id, TimeslotDTO TimeSlot)
        {
            var _timeSlot = new TimeSlot
            {
                TimeSlotId = id,
                StartTime = TimeSlot.StartTime,
                EndTime = TimeSlot.EndTime,
            };
            await _timeSlotService.Update(_timeSlot);
        }

    }
}
