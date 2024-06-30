using NanoidDotNet;
using PetHealthcare.Server.Core.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        public readonly IMedicalRecordRepository medRecService;
        public readonly IAppointmentRepository appointmentService;
        public MedicalRecordService(IMedicalRecordRepository medicalRecordService, IAppointmentRepository appointmentRepository)
        {
            medRecService = medicalRecordService;
            appointmentService = appointmentRepository;
        }
        public async Task CreateMedicalRecord(MedicalRecordResDTO medicalRecord)
        {
            var medicalRec = new MedicalRecord
            {
                MedicalRecordId = GenerateID(),
                DateCreated = DateOnly.FromDateTime(DateTime.Now),
                PetWeight = medicalRecord.PetWeight,
                Symptoms = medicalRecord.Symptoms,
                Allergies = medicalRecord.Allergies,
                Diagnosis = medicalRecord.Diagnosis,
                AdditionalNotes = medicalRecord.AdditionalNotes,
                FollowUpAppointmentDate = medicalRecord.FollowUpAppointmentDate,
                FollowUpAppointmentNotes = medicalRecord.FollowUpAppointmentNotes,
                DrugPrescriptions = medicalRecord.DrugPrescriptions,
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

        public async Task<MedicalRecordVetDTO> GetMedicalRecordsByAppointmentId(string appointmentId)
        {
            return await medRecService.GetMedicalRecordsByAppointmentId(appointmentId);
        }
        public async Task<IEnumerable<MedicalRecordVetDTO>> GetMedicalRecordsByVetId(string vetId)
        {
            var joinList = from s in await medRecService.GetAll()
                           join r in await appointmentService.GetAll()
                           on s.AppointmentId equals r.AppointmentId
                           where r.VeterinarianAccountId == vetId
                           select s;
            //var listAppointment= await appointmentService.GetAll();
            //listAppointment.Where(a=>a.VeterinarianAccountId==vetId).ToList();
            //var list =await medRecService.GetAll();

            List<MedicalRecordVetDTO> recordVetDTOs = new List<MedicalRecordVetDTO>();
            foreach (var record in joinList)
            {
                var medRecord = new MedicalRecordVetDTO
                {
                    MedicalRecordId = record.MedicalRecordId,
                    AdditionalNotes = record.AdditionalNotes,
                    Allergies = record.Allergies,
                    Diagnosis = record.Diagnosis,
                    DrugPrescriptions = record.DrugPrescriptions,
                    FollowUpAppointmentDate = record.FollowUpAppointmentDate,
                    FollowUpAppointmentNotes = record.FollowUpAppointmentNotes,
                    PetWeight = record.PetWeight,
                    Symptoms = record.Symptoms,
                };
                recordVetDTOs.Add(medRecord);
            }
            return recordVetDTOs;
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
