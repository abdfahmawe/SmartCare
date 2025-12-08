using SmartCare.BLL.Services.Interfaces;
using SmartCare.DAL.DTO.Requists;
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
   public class PatientAppointmentService : IPatientAppointmentService
    {
        private readonly IPatientAppointmentRepositry _patientAppointmentRepositry;
        private const int SlotMinutes = 30;
        public PatientAppointmentService(IPatientAppointmentRepositry patientAppointmentRepositry)
        {
            _patientAppointmentRepositry = patientAppointmentRepositry;
        }

        public async Task<Appointment> BookAppointmentAsync(string patientId, BookAppointmentRequist requist)
        {
            if (requist.StartAt < DateTime.Now)
            {
                throw new Exception("The appointment time must be in the future. Please select a valid time.");
            }
            else if(requist.StartAt > DateTime.Today.AddDays(30))
            {
                throw new Exception("Appointments can only be booked up to 30 days in advance. Please select a closer date.");
            }
            bool withinWorkingHours = await _patientAppointmentRepositry.IsWithinWorkingHours(requist.DoctorId, requist.StartAt);
            if (!withinWorkingHours)
            {
                throw new Exception("The selected time is outside the doctor's working hours. Please choose a different time.");
            }
            var isSlotAvailable = await _patientAppointmentRepositry.IsSlotAvailableAsync(requist);
            if (!isSlotAvailable)
            {
                throw new Exception("The selected time slot is not available. Please choose a different time.");
            }
            return await _patientAppointmentRepositry.BookAppintmentAsync(patientId, requist);
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
        public async Task<AvailableSlotsResponse> GetAvailableSlotsAsync(string doctorId, DateTime date)
        {
            var response = new AvailableSlotsResponse
            {
                DoctorId = doctorId,
                Date = date,
                SlotMinutes = SlotMinutes
            };

            // 1) نجيب دوام اليوم بغض النظر محجوز او لا 
            var workingTime = await _patientAppointmentRepositry.GetWorkingTimeAsync(doctorId, date.DayOfWeek);

            if (workingTime is null)
                return response; // الدكتور ما بشتغل اليوم

            // 2) نجيب مواعيد اليوم
            var appointments = await _patientAppointmentRepositry.GetDoctorAppointmentsInDayAsync(doctorId, date);

            // 3) نجهز الـ slots
            var slots = new List<AvailableSlotDto>();
            var duration = TimeSpan.FromMinutes(SlotMinutes);

            var start = date.Date + workingTime.StartTime; // DateTime + TimeSpan
            var end = date.Date + workingTime.EndTime;     // DateTime + TimeSpan

            for (var slotStart = start; slotStart + duration <= end; slotStart += duration)
            {
                var slotEnd = slotStart + duration;
                /*
                    Appointment: 10:00 → 10:30
                    Slot: 09:30 → 10:00  ===>>>>>>>>>>>>>>> No Confilt 

                    Slot: 10:00 → 10:30  ===>>>>>>>>>>>>>>> Conflict
                 */

                bool conflict = appointments.Any(a =>
                    slotStart < a.EndAt && slotEnd > a.StartAt
                );

                if (!conflict)
                {
                    slots.Add(new AvailableSlotDto
                    {
                        StartAt = slotStart,
                        EndAt = slotEnd
                    });
                }
            }

            response.Slots = slots;
            return response;
        }

        public async Task<List<DoctorsWithIdsResponse>> GetAllDoctorsWithIdsAsync()
        {
            var doctors = await _patientAppointmentRepositry.GetAllDoctorsAsync();
            var response = doctors.Select(d => new DoctorsWithIdsResponse
            {
                DoctorId = d.Id,
                FullName = d.FullName
            }).ToList();
            return response;
        }
    }
}
