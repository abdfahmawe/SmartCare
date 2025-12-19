using SmartCare.DAL.DTO.Responses;
using SmartCare.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.BLL.Services.Interfaces
{
    public interface IDoctorAppointmentService
    {
        Task<List<DoctorAppointmentResponseDTO>> GetAllAppointmentsAsync(string doctorId , bool onlySchedueld=true);
        Task<Appointment> CompleteAppointmentAsync(string doctorId , string AppointmentID);
        Task<List<DoctorAppointmentResponseDTO>> GetTodayAppointmentsAsync(string doctorId);
    }
}
