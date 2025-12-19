using Mapster;
using Microsoft.EntityFrameworkCore;
using SmartCare.BLL.Services.Interfaces;
using SmartCare.DAL.Data;
using SmartCare.DAL.DTO.Responses;
using SmartCare.DAL.Models;
using SmartCare.DAL.Repositries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.BLL.Services.Classes
{
   public class DoctorAppointmentService : IDoctorAppointmentService
    {
        private readonly IDoctorAppointmentRepository _doctorAppointmentRepository;

        public DoctorAppointmentService(IDoctorAppointmentRepository doctorAppointmentRepository)
        {
            _doctorAppointmentRepository = doctorAppointmentRepository;
        }

        public async Task<Appointment> CompleteAppointmentAsync(string doctorId, string AppointmentID)
        {
           return await _doctorAppointmentRepository.CompleteAppointmentAsync(doctorId, AppointmentID);
        }

        public async Task<List<DoctorAppointmentResponseDTO>> GetAllAppointmentsAsync(string doctorId , bool onlySchedueld=true)
        {
            var appointments =await _doctorAppointmentRepository.GetAll(doctorId , onlySchedueld);
            var response = appointments.Select(a => new DoctorAppointmentResponseDTO
            {
                Id = a.Id,
                PatientId = a.PatientId,
                PatientName = a.Patient.FullName,
                DurationMinutes = a.DurationMinutes,
                Status = a.Status,
                StartAt = a.StartAt,
                EndAt = a.EndAt,
            }).ToList();
            return response;
        }
        public async Task<List<DoctorAppointmentResponseDTO>> GetTodayAppointmentsAsync(string doctorId)
        {
            var allAppoitnemts = await _doctorAppointmentRepository.GetAll(doctorId,false);
            var todayDate = DateTime.Now.Date;
            var todayAppointments = allAppoitnemts.Where(a => a.StartAt.Date == todayDate).ToList();
            var response = todayAppointments.Adapt<List<DoctorAppointmentResponseDTO>>();
            return response;
        }
    }
}
