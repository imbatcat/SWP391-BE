
﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
﻿using Microsoft.AspNetCore.Http.HttpResults;
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
        private readonly ITimeslotRepository _timeSlotRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IPetRepository _petRepository;
        private readonly IBookingPaymentRepository _bookPaymentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository, ITimeslotRepository timeSlotRepository, IAccountRepository accountRepository, IPetRepository petRepository, IBookingPaymentRepository bookPaymentRepository)
        {
            _appointmentRepository = appointmentRepository;
            _timeSlotRepository = timeSlotRepository;
            _accountRepository = accountRepository;
            _petRepository = petRepository;
            _bookPaymentRepository = bookPaymentRepository;
        }
        public string GenerateId()
        {
            var prefix = "AP-";
            string id = Nanoid.Generate(size: 8);
            return prefix + id;
        }
        public async Task CreateAppointment(CreateAppointmentDTO appointment, string id)
        {
            Appointment toCreateAppointment = new Appointment
            {
                AppointmentType = appointment.AppointmentType,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentNotes = appointment.AppointmentNotes,
                BookingPrice = appointment.BookingPrice,
                PetId = appointment.PetId,
                VeterinarianAccountId = appointment.VeterinarianAccountId,
                AppointmentId = id,
                AccountId = appointment.AccountId,
                TimeSlotId = appointment.TimeSlotId,
                IsCancel = false,
                IsCheckIn = false,
                IsCheckUp = false,
                
            };
            await _appointmentRepository.Create(toCreateAppointment);
        }

        public void DeleteAppointment(Appointment appointment)
        {
            _appointmentRepository.Delete(appointment);
        }



        public async Task<IEnumerable<GetAllAppointmentForAdminDTO>> GetAllAppointment()
        {
            IEnumerable<Appointment> appList = await _appointmentRepository.GetAll();
            List<GetAllAppointmentForAdminDTO> CAList = new List<GetAllAppointmentForAdminDTO>();
            foreach(Appointment app in appList)
            {
                GetAllAppointmentForAdminDTO appointmentDTO = new GetAllAppointmentForAdminDTO
                {
                    AppointmentDate = app.AppointmentDate,
                    AppointmentNotes = app.AppointmentNotes,
                    VeterinarianName = app.Veterinarian.FullName,
                    PetName= app.Pet.PetName,
                    BookingPrice= app.BookingPrice,
                    AppointmentType = app.AppointmentType,
                    TimeSlot= app.TimeSlot.StartTime.ToString("h:mm") + " - " + app.TimeSlot.EndTime.ToString("h:mm"),
                    IsCancel = app.IsCancel,
                    IsCheckIn = app.IsCheckIn,
                    IsCheckUp = app.IsCheckUp,
                    CheckinTime = app.CheckinTime,
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

        public string GetAppointmentStatus(Appointment appointment)
        {
            string status = "Ongoing";
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            if(appointment.IsCancel == true)
            {
                status = "Cancel";
            } else if(appointment.IsCheckUp == true || appointment.AppointmentDate.CompareTo(currentDate) < 0)
            {
                status = "Finish";
            }
            return status;
        }
        public async Task<IEnumerable<ResAppListForCustomer>> getAllCustomerAppointment(string id, string listType)
        {
            var AccountCheck = await GetAccountById(id);
            if(AccountCheck == null)
            {
                throw new Exception("Can't find that Account");
            }
            IEnumerable<Appointment> appointmentsList = await _appointmentRepository.GetAll();
            List<ResAppListForCustomer> resAppListForCustomers = new List<ResAppListForCustomer>();
            foreach (Appointment appointment in appointmentsList)
            {
                DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
                if(listType.Equals("history",StringComparison.OrdinalIgnoreCase))
                {
                    if (appointment.AccountId.Equals(id) && appointment.AppointmentDate.CompareTo(currentDate) < 0)
                    {
                        resAppListForCustomers.Add(new ResAppListForCustomer
                        {
                            AppointmentDate = appointment.AppointmentDate,
                            BookingPrice = appointment.BookingPrice,
                            PetName = appointment.Pet.PetName,
                            VeterinarianName = appointment.Veterinarian.FullName,
                            TimeSlot = appointment.TimeSlot.StartTime.ToString("h:mm") + " - " + appointment.TimeSlot.EndTime.ToString("h:mm"),
                            AppointmentStatus = GetAppointmentStatus(appointment)
                        });
                    }
                } else if (listType.Equals("current",StringComparison.OrdinalIgnoreCase))
                {
                    if (appointment.AccountId.Equals(id) && appointment.AppointmentDate.CompareTo(currentDate) > 0)
                    {
                        resAppListForCustomers.Add(new ResAppListForCustomer
                        {
                            AppointmentDate = appointment.AppointmentDate,
                            BookingPrice = appointment.BookingPrice,
                            PetName = appointment.Pet.PetName,
                            VeterinarianName = appointment.Veterinarian.FullName,
                            TimeSlot = appointment.TimeSlot.StartTime.ToString("h:mm") + " - " + appointment.TimeSlot.EndTime.ToString("h:mm"),
                            AppointmentStatus = GetAppointmentStatus(appointment)
                        });
                    }
                }
                
            }

            //catch error
            if(resAppListForCustomers.Count() == 0 && listType.Equals("current", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("The current list is empty");
            } else if (resAppListForCustomers.Count() == 0 && listType.Equals("history", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("The history list is empty");
            }
            return resAppListForCustomers;
        }

        public async Task<IEnumerable<ResAppListForCustomer>> SortAppointmentByDate(string id, string SortList, string SortOrder)
        {
            IEnumerable<ResAppListForCustomer> SortedList = new List<ResAppListForCustomer>();
            IEnumerable<ResAppListForCustomer> allAppointment = await getAllCustomerAppointment(id,SortList);
            if (SortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase))
            {
                SortedList = allAppointment.OrderBy(a => a.AppointmentDate);

            }
            else if (SortOrder.Equals("desc", StringComparison.OrdinalIgnoreCase))
            {
                SortedList = allAppointment.OrderByDescending(a => a.AppointmentDate);
            }
            return SortedList;
        }

        public async Task<IEnumerable<GetAllAppointmentForAdminDTO>> GetAllAppointmentByAccountId(string acId)
        {
            IEnumerable<Appointment> AppList = await _appointmentRepository.GetAll();
            List<GetAllAppointmentForAdminDTO> appointmentList = new List<GetAllAppointmentForAdminDTO>();
            if(AppList != null)
            {
                foreach (Appointment app in AppList)
                {
                    if (app.AccountId.Equals(acId))
                    {
                        appointmentList.Add(new GetAllAppointmentForAdminDTO
                        {
                            AccountId = app.AccountId,
                            AppointmentDate = app.AppointmentDate,
                            AppointmentId = app.AppointmentId,
                            AppointmentNotes = app.AppointmentNotes,
                            AppointmentType = app.AppointmentType,
                            BookingPrice = app.BookingPrice,
                            TimeSlot = app.TimeSlot.StartTime.ToString("h:mm") + " - " + app.TimeSlot.EndTime.ToString("h:mm"),
                            IsCancel = app.IsCancel,
                            IsCheckIn = app.IsCheckIn,
                            IsCheckUp = app.IsCheckUp,
                            CheckinTime = app.CheckinTime,
                        });
                    }
                }
            }
            if(appointmentList.Count == 0)
            {
                return null;
            }
            return appointmentList;
        }

        public async Task<Account?> GetAccountById(string id)
        {
            return await _appointmentRepository.GetAccountById(id);
        }

        public async Task<IEnumerable<AppointmentListForVetDTO?>> ViewAppointmentListForVet(string id, DateOnly date)
        {
            IEnumerable<Appointment> appointmentList = await _appointmentRepository.GetAllAppointmentListForVet(id, date);
            
            List<AppointmentListForVetDTO> appointmentListForVetDTO = new List<AppointmentListForVetDTO>();
            foreach(Appointment app in appointmentList)
            {
                appointmentListForVetDTO.Add(new AppointmentListForVetDTO
                {
                    AppointmentDate = app.AppointmentDate,
                    AppointmentNotes = app.AppointmentNotes,
                    CustomerName = app.Account.FullName,
                    CustomerPhone = app.Account.PhoneNumber,
                    PetName = app.Pet.PetName,
                });
            }
            return appointmentListForVetDTO;
        }

        public async Task<IEnumerable<VetAppointment?>> ViewVetAppointmentList(string id, int timeSlot, DateOnly date)
        {
            IEnumerable<Appointment> appointmentList = await _appointmentRepository.GetVetAppointmentList(id, timeSlot, date);
            if (appointmentList.Count() > 0)
            {
                appointmentList = appointmentList.OrderByDescending(a => a.IsCheckIn).ThenBy(a => a.CheckinTime);
            }
            List<VetAppointment> vetAppointmentList = new List<VetAppointment>();
            foreach(Appointment appointment in appointmentList)
            {
                if(appointment.IsCancel != true && appointment.IsCheckUp != true )
                {
                    string _status = "Waiting";
                    string _petType = "Cat";
                    if(appointment.Pet.IsCat == false)
                    {
                        _petType = "Dog";
                    }
                    if(appointment.IsCheckIn == false)
                    {
                        _status = "Haven't come";
                    }
                    vetAppointmentList.Add(new VetAppointment
                    {
                        AppointmentId = appointment.AppointmentId,
                        OwnerName = appointment.Account.FullName,
                        PetName=appointment.Pet.PetName,
                        PetBreed = appointment.Pet.PetBreed,
                        TimeSlot = appointment.TimeSlot.StartTime.ToString("h:mm") + " - " + appointment.TimeSlot.EndTime.ToString("h:mm"),
                        status = _status,
                        PetType = _petType,
                    });
                }
            }
            return vetAppointmentList;
        }

        public async Task<bool> UpdateCheckinStatus(string appointmentId, string paymentMethod)
        {
            Appointment? toCheckInAppointment = await _appointmentRepository.GetByCondition(a => a.AppointmentId == appointmentId);
            if(toCheckInAppointment == null)
            {
                return false;
            } else
            {
                await _bookPaymentRepository.AddFullPaidRecord(appointmentId, paymentMethod);
                toCheckInAppointment.IsCheckIn = true;
                toCheckInAppointment.CheckinTime = TimeOnly.FromDateTime(DateTime.Now);
                
                await _appointmentRepository.SaveChanges();
            }
            return true;
        }

        public async Task<IEnumerable<AppointmentForStaffDTO>> GetAllAppointmentForStaff(DateOnly date, int timeslot)
        {
            IEnumerable<Appointment> listAppointments = new List<Appointment>();
            listAppointments = await _appointmentRepository.GetAllAppointmentForStaff(date, timeslot);
            List<AppointmentForStaffDTO> appointmentForStaffDTOs = new List<AppointmentForStaffDTO>();
            foreach(Appointment app in listAppointments)
            {
                if (!app.IsCancel && !app.IsCheckIn && !app.IsCheckUp)
                {
                    appointmentForStaffDTOs.Add(new AppointmentForStaffDTO
                    {

                        //        string appointmentId { get; set;
                        //}
                        //string customerName { get; set; }
                        //string phoneNumber { get; set; }
                        //string petName { get; set; }
                        //string status { get; set; }
                        appointmentId = app.AppointmentId,
                        customerName = app.Account.FullName,
                        phoneNumber = app.Account.PhoneNumber,
                        petName = app.Pet.PetName,
                        status = "Uncheck in",
                        VetName = app.Veterinarian.FullName,
                    });
                }
                
            }
            return appointmentForStaffDTOs;
        }

}
