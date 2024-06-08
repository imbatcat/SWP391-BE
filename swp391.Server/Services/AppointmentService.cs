using NanoidDotNet;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.APIs.DTOS.AppointmentDTOs;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services.Interfaces;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public string GenerateId()
        {
            var prefix = "AP-";
            string id = Nanoid.Generate(size: 8);
            return prefix + id;
        }
        public async Task CreateAppointment(CreateAppointmentDTO appointment)
        {
            Appointment toCreateAppointment = new Appointment
            {
                AppointmentType = appointment.AppointmentType,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentNotes = appointment.AppointmentNotes,
                BookingPrice = appointment.BookingPrice,
                PetId = appointment.PetId,
                VeterinarianAccountId = appointment.VeterinarianAccountId,
                AppointmentId = GenerateId(),
                AccountId = appointment.AccountId,
                TimeSlotId = appointment.TimeSlotId
            };
            await _appointmentRepository.Create(toCreateAppointment);
        }

        public void DeleteAppointment(Appointment appointment)
        {
            _appointmentRepository.Delete(appointment);
        }



        public async Task<IEnumerable<GetAllAppointmentDTOs>> GetAllAppointment()
        {
            IEnumerable<Appointment> appList = await _appointmentRepository.GetAll();
            List<GetAllAppointmentDTOs> CAList = new List<GetAllAppointmentDTOs>();
            foreach(Appointment app in appList)
            {
                GetAllAppointmentDTOs appointmentDTO = new GetAllAppointmentDTOs
                {
                    AppointmentDate = app.AppointmentDate,
                    AppointmentNotes = app.AppointmentNotes,
                    VeterinarianName = app.Veterinarian.FullName,
                    PetName= app.Pet.PetName,
                    BookingPrice= app.BookingPrice,
                    AppointmentType = app.AppointmentType,
                    TimeSlotId= app.TimeSlotId,
                    IsCancel = app.IsCancel,
                    IsCheckIn = app.IsCheckIn,
                //            public string AppointmentId { get; set; }
                //public DateOnly AppointmentDate { get; set; }
                //public string AppointmentType { get; set; }
                //public string? AppointmentNotes { get; set; }
                //public double BookingPrice { get; set; }
                //public string AccountId { get; set; }
                //public string PetName { get; set; }
                //public string VeterinarianName { get; set; }
                //public int TimeSlotId { get; set; }
                //public bool IsCancel { get; set; }
                //public bool IsCheckIn { get; set; }
                };
                CAList.Add(appointmentDTO);
            }
            return CAList;
        }

        public async Task<Appointment?> GetAppointmentByCondition(Expression<Func<Appointment, bool>> expression)
        {
            return await _appointmentRepository.GetByCondition(expression);
        }

        public bool isVetIdValid(string VetId)
        {
            return _appointmentRepository.isInputtedVetIdValid(VetId);
        }

        public async Task UpdateAppointment(string id, CustomerAppointmentDTO appointment)
        {
            Appointment UpdateAppointment = new Appointment
            {
                AppointmentDate = appointment.AppointmentDate,
                AppointmentNotes = appointment.AppointmentNotes,
                VeterinarianAccountId = appointment.VeterinarianAccountId,
                TimeSlotId = appointment.TimeSlotId,
                AppointmentId = id
            };
            await _appointmentRepository.Update(UpdateAppointment);
        }

        public async Task<IEnumerable<ResAppListForCustomer>> getAllCustomerAppList(string id) //get future appointment
        {
            Debug.WriteLine(id);
            IEnumerable<Appointment> appointmentsList = await _appointmentRepository.GetAll();
            List<ResAppListForCustomer> resAppListForCustomers = new List<ResAppListForCustomer>();
            foreach (Appointment appointment in appointmentsList)
            {
                DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);

                if (appointment.AccountId.Equals(id) && appointment.AppointmentDate.CompareTo(currentDate) >= 0)
                {
                    resAppListForCustomers.Add(new ResAppListForCustomer
                    {
                        AppointmentDate = appointment.AppointmentDate,
                        BookingPrice = appointment.BookingPrice,
                        PetName = appointment.Pet.PetName,
                        VeterinarianName = appointment.Veterinarian.FullName,
                        TimeSlot = appointment.TimeSlot.StartTime.ToString("h:mm") + " - " + appointment.TimeSlot.EndTime.ToString("h:mm"),
                    });
                }
            }
            return resAppListForCustomers;
        }

        public async Task<IEnumerable<ResAppListForCustomer>> getAllCustomerAppHistory(string id)
        {
            IEnumerable<Appointment> appointmentsList = await _appointmentRepository.GetAll();
            List<ResAppListForCustomer> resAppListForCustomers = new List<ResAppListForCustomer>();
            foreach (Appointment appointment in appointmentsList)
            {
                DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
                if (appointment.AccountId.Equals(id) && appointment.AppointmentDate.CompareTo(currentDate) < 0)
                {
                    resAppListForCustomers.Add(new ResAppListForCustomer
                    {
                        AppointmentDate = appointment.AppointmentDate,
                        BookingPrice = appointment.BookingPrice,
                        PetName = appointment.Pet.PetName,
                        VeterinarianName = appointment.Veterinarian.FullName,
                        TimeSlot = appointment.TimeSlot.StartTime.ToString("h:mm") + " - " + appointment.TimeSlot.EndTime.ToString("h:mm"),
                    });
                }
            }
            return resAppListForCustomers;
        }

        public async Task<IEnumerable<ResAppListForCustomer>> SortAppointmentByDate(string id, string SortList, string SortOrder)
        {
            IEnumerable<ResAppListForCustomer> SortedList = new List<ResAppListForCustomer>();
            if (SortList.Equals("history", StringComparison.OrdinalIgnoreCase))
            {
                IEnumerable<ResAppListForCustomer> allAppointment = await getAllCustomerAppHistory(id);
                if (SortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase))
                {
                    SortedList = allAppointment.OrderBy(a => a.AppointmentDate);

                }
                else if (SortOrder.Equals("desc", StringComparison.OrdinalIgnoreCase))
                {
                    SortedList = allAppointment.OrderByDescending(a => a.AppointmentDate);
                }
            }
            else if (SortList.Equals("current", StringComparison.OrdinalIgnoreCase))
            {
                IEnumerable<ResAppListForCustomer> allAppointment = await getAllCustomerAppList(id);
                if (SortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase))
                {
                    SortedList = allAppointment.OrderBy(a => a.AppointmentDate);

                }
                else if (SortOrder.Equals("desc", StringComparison.OrdinalIgnoreCase))
                {
                    SortedList = allAppointment.OrderByDescending(a => a.AppointmentDate);
                }
            }
            return SortedList;
        }
    }

}
