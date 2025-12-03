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
   public interface IPatientService
    {
        Task<PatientProfileResponse> GetPatientByIdAsync(string UserId);
        Task<Patient> UpdateProfileAsync(string UserId, UpdatePatientRequist dto);
      
        Task<List<MedicalRecord>> GetMedicalRecordsAsync(string UserId);
        Task<List<PatientPresciptionResponse>> GetPrescriptionsAsync(string UserId);
    }
}
