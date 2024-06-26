﻿using PetHealthcare.Server.Core.DTOS;
using PetHealthcare.Server.Models;

namespace PetHealthcare.Server.Repositories.Interfaces
{
    public interface IMedicalRecordRepository : IRepositoryBase<MedicalRecord>
    {
        Task<MedicalRecordVetDTO> GetMedicalRecordsByAppointmentId(string appointmentId);
    }
}
