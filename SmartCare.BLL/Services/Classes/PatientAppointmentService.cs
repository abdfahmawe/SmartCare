using SmartCare.BLL.Services.Interfaces;
using SmartCare.DAL.DTO.Responses;
using SmartCare.DAL.Repositries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.BLL.Services.Classes
{
   public class PatientAppointmentService : IPatientAppointmentService
    {
        private readonly IPatientAppointmentRepositry _patientAppointmentRepositry;

        public PatientAppointmentService(IPatientAppointmentRepositry patientAppointmentRepositry)
        {
            _patientAppointmentRepositry = patientAppointmentRepositry;
        }

        public async Task<List<PatientAppointmentResponse>> GetAppointmentsAsync(string UserId)
        {
            var records = await _patientAppointmentRepositry.GetAppointmentsAsync(UserId);
            var recordsAfterAdapt = new List<PatientAppointmentResponse>();
            foreach (var appointment in records)
            {
                recordsAfterAdapt.Add(new PatientAppointmentResponse()
                {
                    DurationMinutes = appointment.DurationMinutes,
                    StartAt = appointment.StartAt,
                    EndAt = appointment.EndAt,
                    Status = appointment.Status,
                    Id = appointment.Id,
                    DoctorName = appointment.Doctor.FullName,
                });
            }
            return recordsAfterAdapt;
        }
    }
}
