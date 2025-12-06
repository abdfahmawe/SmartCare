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
   public interface IPatientAppointmentService
    {
        Task<List<PatientAppointmentResponse>> GetAppointmentsAsync(string UserId);
        Task<Appointment> BookAppointmentAsync(string patientId, BookAppointmentRequist requist);
        Task<List<DoctorsWithIdsResponse>> GetAllDoctorsWithIdsAsync();
        Task<AvailableSlotsResponse> GetAvailableSlotsAsync(string doctorId, DateTime date);
    }
}
