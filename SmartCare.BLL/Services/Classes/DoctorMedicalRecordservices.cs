using Mapster;
using SmartCare.BLL.Services.Interfaces;
using SmartCare.DAL.DTO.Requists;
using SmartCare.DAL.DTO.Responses;
using SmartCare.DAL.Models;
using SmartCare.DAL.Repositries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.BLL.Services.Classes
{
    public class DoctorMedicalRecordservices : IDoctorMedicalRecordservices
    {
        private readonly IDoctorMedicalRecordRepositry _doctorMedicalRecordRepositry;

        public DoctorMedicalRecordservices(IDoctorMedicalRecordRepositry doctorMedicalRecordRepositry)
        {
            _doctorMedicalRecordRepositry = doctorMedicalRecordRepositry;
        }
        public async Task CreateMedicalRecord(string doctorId, string appointmentId , MedicalRecordRequist medicalRecordRequist)
        {
            var medicalrecord = new MedicalRecord
            {
                Allergies = medicalRecordRequist.Allergies,
                Diagnosis = medicalRecordRequist.Diagnosis,
                Symptoms = medicalRecordRequist.Symptoms,
                AppointmentId = appointmentId,
                Notes = medicalRecordRequist.Notes,
                TestsNeeded = medicalRecordRequist.TestsNeeded,
                VitalSigns = medicalRecordRequist.VitalSigns,
            };
         await _doctorMedicalRecordRepositry.CreateMedicalRecord(doctorId, medicalrecord);
          
        }

        public async Task<List<MedicalRecordResponse>> GetAllMedicalRecordsAsync(string doctorId)
        {
            var medicalRecords =await _doctorMedicalRecordRepositry.GetAllMedicalRecordsAsync(doctorId);
            var medicalRecordResponses = medicalRecords.Adapt<List<MedicalRecordResponse>>();
            return medicalRecordResponses;
        }

        public async Task<MedicalRecordResponse> GetMedicalRecordByAppointmentIdAsync(string doctorId, string appointmentId)
        {
            var medicalRecord = await _doctorMedicalRecordRepositry.GetMedicalRecordByAppointmentIdAsync(doctorId, appointmentId);
            var medicalRecordResponse = medicalRecord.Adapt<MedicalRecordResponse>();
            return medicalRecordResponse;
        }
    }
}
