using Microsoft.EntityFrameworkCore;
using SmartCare.DAL.Data;
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
            var appointment = await _dbContext.Appointments.Where(a => a.Status == Models.AppointmentStatus.Scheduled && DateTime.Now > a.EndAt).ToListAsync();
            foreach (var app in appointment)
            {
                app.Status = Models.AppointmentStatus.NoShow;
            }
            if(appointment.Count>0)
            await _dbContext.SaveChangesAsync();
        }
    }
}
