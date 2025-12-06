using Microsoft.EntityFrameworkCore;
using SmartCare.DAL.Data;
using SmartCare.DAL.DTO.Requists;
using SmartCare.DAL.Models;
using SmartCare.DAL.Repositries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Repositries.Classes
{
   public class PatientAppointmentRepositry : IPatientAppointmentRepositry
    {
        private readonly ApplicationDbContext _dbContext;

        public PatientAppointmentRepositry(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Appointment> BookAppintmentAsync(string patientId, BookAppointmentRequist requist)
        {
            var appointment = new Appointment()
            {
                DoctorId = requist.DoctorId,
                PatientId = patientId,
                StartAt = requist.StartAt,
                EndAt = requist.StartAt.AddMinutes(30),
                Status = AppointmentStatus.Scheduled,

            };
           await _dbContext.Appointments.AddAsync(appointment);
           await _dbContext.SaveChangesAsync();
            return appointment;
        }

        public Task<List<Appointment>> GetAppointmentsAsync(string UserId)
        {
            var appointments = _dbContext.Appointments.Where(a => a.PatientId == UserId)
                .Include(a => a.Doctor)
                .Include(a => a.MedicalRecord)
                .ToListAsync();
            return appointments;
        }

        public async Task<bool> IsSlotAvailableAsync(BookAppointmentRequist requist)
        {
            var docotrID = requist.DoctorId;
            var startAt = requist.StartAt; //DateTime 
            var result = !await _dbContext.Appointments.AnyAsync(a => a.DoctorId == docotrID && a.StartAt == startAt);
            return result;
        }

        public async Task<bool> IsWithinWorkingHours(string doctoId, DateTime appointmentTime)
        {
            var DoctorWorkingHoures = await _dbContext.WorkingTimes
                .FirstOrDefaultAsync(w => w.DoctorId == doctoId && w.Day == appointmentTime.DayOfWeek);
            // إذا ما فيه دوام لذلك اليوم
            if (DoctorWorkingHoures == null)
            {
                return false;
            }
            return appointmentTime.TimeOfDay >= DoctorWorkingHoures.StartTime &&
       appointmentTime.TimeOfDay + TimeSpan.FromMinutes(30) <= DoctorWorkingHoures.EndTime;



        }

        public async Task<WorkingTime?> GetWorkingTimeAsync(string doctorId, DayOfWeek dayOfWeek)
        {
            return await _dbContext.WorkingTimes
                .FirstOrDefaultAsync(w => w.DoctorId == doctorId && w.Day == dayOfWeek);
        }

        public async Task<List<Appointment>> GetDoctorAppointmentsInDayAsync(string doctorId, DateTime date)
        {
            return await _dbContext.Appointments
                .Where(a => a.DoctorId == doctorId &&
                            a.StartAt.Date == date.Date &&
                            a.Status == AppointmentStatus.Scheduled)
                .ToListAsync();
        }

        public async Task<List<Doctor>> GetAllDoctorsAsync()
        {
            var doctors = await _dbContext.Doctors.ToListAsync();
            return doctors;
        }
    }
}
