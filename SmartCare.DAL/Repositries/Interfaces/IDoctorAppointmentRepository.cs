using SmartCare.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Repositries.Interfaces
{
   public interface IDoctorAppointmentRepository
    {
       Task<List<Appointment>> GetAll(string doctorId , bool onlySchedueld=true);
       Task<Appointment> CompleteAppointmentAsync(string docorId , string AppointmentId);
    }
}
