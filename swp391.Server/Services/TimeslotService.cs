using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services.Interfaces;
using System.Globalization;
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
                
                StartTime = ParseTime(timeSlot.StartTime),
                EndTime = ParseTime(timeSlot.EndTime),
                
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

        public async Task UpdateTimeSlot(int id, TimeslotDTO timeSlot)
        {
            var _timeSlot = new TimeSlot
            {
                TimeSlotId = id,
                StartTime = ParseTime(timeSlot.StartTime),
                EndTime = ParseTime(timeSlot.EndTime),
            };
            await _timeSlotService.Update(_timeSlot);
        }
         
        private static TimeOnly ParseTime(string input)
        {
            if (TimeOnly.TryParse(input, out TimeOnly time))
            {
                return time;
            }
            else
            {
                throw new FormatException("Invalid time format.");
            }
        }

    }
}
