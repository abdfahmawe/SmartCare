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
        
        public Task DeleteWorkingTimeAsync(WorkingTime workingTime);
        public Task<List<WorkingTime>> GetWorkingTimesAsync(string doctorId);
        public Task<WorkingTime?> GetWorkingTimeAsync(string doctorId, DayOfWeek day);
        public  Task<bool> HasFutureAppointmentsAsync(string doctorId, DayOfWeek day);
        public Task AddWorkingTimeAsync(WorkingTime workingTime);
        public Task UpdateWorkingTimeAsync(WorkingTime workingTime);
        public Task<bool> hasFutureConflictingAppointments(string doctorID , DoctorWorkingTimeRequist requist);

    }
}
