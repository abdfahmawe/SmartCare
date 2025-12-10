using SmartCare.DAL.DTO.Responses;
using SmartCare.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Repositries.Interfaces
{
   public interface IDoctorMedicalRecordRepositry
    {
        Task CreateMedicalRecord(string doctorId, MedicalRecord medicalRecord);
        Task<MedicalRecord> GetMedicalRecordByAppointmentIdAsync(string doctorId, string appointmentId);
        Task<List<MedicalRecord>> GetAllMedicalRecordsAsync(string doctorId);
    }
}
