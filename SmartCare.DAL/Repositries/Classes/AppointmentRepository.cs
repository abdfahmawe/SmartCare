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
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AppointmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task MarkNoShowsAsync()
        {
            var timeNow = DateTime.Now;
            var appointment = await _dbContext.Appointments.Where(a => a.Status == Models.AppointmentStatus.Scheduled && timeNow > a.EndAt).ToListAsync();
            foreach (var app in appointment)
            {
                app.Status = Models.AppointmentStatus.NoShow;
            }
            if(appointment.Count>0)
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Appointment> GetByIdAsync(string appointmentId)
        {
            return await _dbContext.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.Id == appointmentId);
        }
    }
}
