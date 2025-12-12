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
    public interface IDoctorWorkingTimeService
    {
        public Task SetWorkingHoursAsync(string doctorId, DoctorWorkingTimeRequist workingTimeRequist);
        public Task UpdateWorkingTimeAsync(string doctorId, DoctorWorkingTimeRequist requist);
        public Task DeleteWorkingTimeAsync(string doctorId, DeleteWorkingTimeRequist day);
        public Task<List<WorkingTimeResponse>> GetWorkingHoursAsync(string doctorId);



    }
}
