using SmartCare.DAL.DTO.Responses;
using SmartCare.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Repositries.Interfaces
{
   public interface IDoctorRepositry
    {
        Task<Doctor> GetProfileAsync(string doctorId);
        Task<string> UpdateDoctorAsync(Doctor doctor);
        Task<List<WorkingTime>> GetWorkingTimeAsync(string doctorId);
    }
}
