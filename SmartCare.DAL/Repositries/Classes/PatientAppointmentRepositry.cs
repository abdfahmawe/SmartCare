using Microsoft.EntityFrameworkCore;
using SmartCare.DAL.Data;
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
        public Task<List<Appointment>> GetAppointmentsAsync(string UserId)
        {
            var appointments = _dbContext.Appointments.Where(a => a.PatientId == UserId)
                .Include(a => a.Doctor)
                .Include(a => a.MedicalRecord)
                .ToListAsync();
            return appointments;
        }
    }
}
