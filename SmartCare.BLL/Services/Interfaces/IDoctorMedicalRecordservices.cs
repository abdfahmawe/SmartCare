using SmartCare.DAL.DTO.Requists;
using SmartCare.DAL.DTO.Responses;
using SmartCare.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.BLL.Services.Interfaces
{
    public interface IDoctorMedicalRecordservices
    {
        Task CreateMedicalRecord(string doctorId, string appointmentId , MedicalRecordRequist medicalRecordRequist);
        Task UpdateMedicalRecordAsync(string doctorId, string appointmentId, MedicalRecordRequist medicalRecordRequist);
        Task<MedicalRecordResponse> GetMedicalRecordByAppointmentIdAsync(string doctorId, string appointmentId);
        Task<List<MedicalRecordResponse>> GetAllMedicalRecordsAsync(string doctorId);
       Task<bool> DeleteMedicalRecordAsync(string doctorId, string appointmentId);
    }
}
