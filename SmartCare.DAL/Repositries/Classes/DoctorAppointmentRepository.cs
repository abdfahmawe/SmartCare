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
   public class DoctorAppointmentRepository : IDoctorAppointmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DoctorAppointmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Appointment>> GetAll(string doctorId, bool onlySchedueld = true)
        {
            var appointments =await _dbContext.Appointments.Include(e=>e.Patient).Where(a=>a.DoctorId ==doctorId).ToListAsync();
            if (onlySchedueld == true)
            {
                appointments = appointments.Where(e => e.Status == AppointmentStatus.Scheduled).ToList();
            }
            return appointments;
        }
    }
}
