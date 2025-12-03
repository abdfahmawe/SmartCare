using SmartCare.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Repositries.Interfaces
{
    public interface IPatientRepositry
    {
        Task<Patient> GetPaitentByIdAsync(string UserId);
        Task UpdatePatientAsync(Patient patient);
        Task<List<MedicalRecord>> GetMedicalRecordsByIdAsync(string UserId);
        Task<List<Prescription>> GetPrescriptions(string UserId);
    }
}
