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
   public interface IDoctorService
    {
        Task<DoctorProfileResponse> GetProfileAsync(string doctorId);
        Task<string> UpdateProfileAsync(string doctorId , UpdateDoctorRequist requist);
        
    }
}
