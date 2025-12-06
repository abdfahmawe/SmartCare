using SmartCare.DAL.DTO.Requists;
using SmartCare.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Repositries.Interfaces
{
   public interface IPatientAppointmentRepositry
    {
        Task<List<Appointment>> GetAppointmentsAsync(string UserId);
        Task<bool> IsSlotAvailableAsync(BookAppointmentRequist requist);
        Task<Appointment> BookAppintmentAsync(string patientId, BookAppointmentRequist requist);
        Task<bool> IsWithinWorkingHours(string doctoId, DateTime appointmentTime);
        //
        Task<WorkingTime?> GetWorkingTimeAsync(string doctorId, DayOfWeek dayOfWeek);
        Task<List<Appointment>> GetDoctorAppointmentsInDayAsync(string doctorId, DateTime date);
        Task<List<Doctor>> GetAllDoctorsAsync();

    }
}
