using SmartCare.DAL.DTO.Requists;
using SmartCare.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Repositries.Interfaces
{
   public interface IDoctorWorkingTimeRepositry
    {
        public Task<WorkingTime> SetWorkingTimeAsync(WorkingTime workingTime);
        public Task<string> UpdateWorkingTimeAsync(string doctorId, DoctorWorkingTimeRequist requist);
        public Task<string> DeleteWorkingTimeAsync(string doctorId, DeleteWorkingTimeRequist dayOfWeek);
        Task<List<WorkingTime>> GetWorkingTimeAsync(string doctorId);
    }
}
