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
    public class DoctorWorkingTimeRepositry : IDoctorWorkingTimeRepositry
    {
        private readonly ApplicationDbContext _dbContext;

        public DoctorWorkingTimeRepositry(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<WorkingTime>> GetWorkingTimesAsync(string doctorId)
        {
            return  await _dbContext.WorkingTimes.Include(i => i.Doctor).Where(w => w.DoctorId == doctorId).ToListAsync();
        }
        public async Task<WorkingTime?> GetWorkingTimeAsync (string doctorId , DayOfWeek day)
        {
            return await _dbContext.WorkingTimes.FirstOrDefaultAsync(wt=>wt.DoctorId == doctorId && wt.Day == day);
        }
        public async Task<bool> HasFutureAppointmentsAsync(string doctorId , DayOfWeek day)
        {
            return (await _dbContext.Appointments.ToListAsync())
        .Any(a => a.DoctorId == doctorId &&
                  a.StartAt.DayOfWeek == day &&
                  a.Status == AppointmentStatus.Scheduled);
        }
        public async Task AddWorkingTimeAsync(WorkingTime workingTime)
        {
            await _dbContext.AddAsync(workingTime);
            await _dbContext.SaveChangesAsync();
         
        }

        public async Task DeleteWorkingTimeAsync(WorkingTime workingTime)
        {
            _dbContext.WorkingTimes.Remove(workingTime);
            await _dbContext.SaveChangesAsync();
           
        }
      
        public async Task UpdateWorkingTimeAsync(WorkingTime workingTime)
        {
            _dbContext.Update(workingTime);
            await _dbContext.SaveChangesAsync();
            
        }

        public async Task<bool> hasFutureConflictingAppointments(string doctorID, DoctorWorkingTimeRequist requist)
        {
            // 1) Get all future appointments for that doctor
            var appointments = _dbContext.Appointments
                .Where(a => a.DoctorId == doctorID && a.Status == AppointmentStatus.Scheduled)
                .AsEnumerable() // ينقل للذاكرة حتى نقدر نستخدم DayOfWeek
                .Where(a => a.StartAt.DayOfWeek == requist.Day)
                .ToList();      // استخدم ToList() العادي بعد AsEnumerable

            // 2) تحقق إذا أي موعد خارج الدوام الجديد
            bool hasOutsideAppointment = appointments.Any(a =>
                a.StartAt.TimeOfDay < requist.StartTime || // قبل بداية الدوام الجديد
                a.EndAt.TimeOfDay > requist.EndTime       // بعد نهاية الدوام الجديد
            );

            return hasOutsideAppointment;

        }
        // appointment => 9:00 - 9:30
        //requist => 8:00 - 13:00 => no conflict
        // requist => 10:00 - 12:00 => conflict
    }
}
