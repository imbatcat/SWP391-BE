using NanoidDotNet;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        public readonly IMedicalRecordRepository medRecService;
        public MedicalRecordService(IMedicalRecordRepository medicalRecordService)
        {
            medRecService = medicalRecordService;
        }
        public async Task CreateMedicalRecord(MedicalRecordDTO medicalRecord)
        {
            var medicalRec = new MedicalRecord
            {
                MedicalRecordId = GenerateID(),
                DateCreated = medicalRecord.DataCreated,
                PetWeight = medicalRecord.PetWeight,
                Symptoms = medicalRecord.Symptoms,
                Allergies = medicalRecord.Allergies,
                Diagnosis = medicalRecord.Diagnosis,
                AdditionalNotes = medicalRecord.AdditionallNotes,
                FollowUpAppointmentDate = DateOnly.FromDateTime(medicalRecord.FollowUpAppointmentDate ?? DateTime.Now),
                FollowUpAppointmentNotes = medicalRecord.FollowUpAppointmentNotes,
                DrugPrescriptions = medicalRecord.DrugPrescription,
                AppointmentId = medicalRecord.AppointmentId,
                PetId = medicalRecord.PetId
            };
            await medRecService.Create(medicalRec);
        }

        public void DeleteMedicalRecord(MedicalRecord medicalRecord)
        {
            medRecService.Delete(medicalRecord);
        }

        public string GenerateID()
        {
            var prefix = "ME-";
            string id = Nanoid.Generate(size: 8);
            return prefix + id;
        }

        public async Task<IEnumerable<MedicalRecord>> GetAllMedicalRecord()
        {
            return await medRecService.GetAll();
        }

        public async Task<MedicalRecord?> GetMedicalRecordByCondition(Expression<Func<MedicalRecord, bool>> expression)
        {
            return await medRecService.GetByCondition(expression);
        }

        public async Task UpdateMedicalRecord(string id, MedicalRecordDTO medicalRecord)
        {
            var medicalRec = new MedicalRecord
            {
                MedicalRecordId = id,
                Symptoms = medicalRecord.Symptoms,
                Allergies = medicalRecord.Allergies,
                Diagnosis = medicalRecord.Diagnosis,
                AdditionalNotes = medicalRecord.AdditionallNotes,
                DrugPrescriptions = medicalRecord.DrugPrescription,
            };
            await medRecService.Update(medicalRec);
        }
    }
}
