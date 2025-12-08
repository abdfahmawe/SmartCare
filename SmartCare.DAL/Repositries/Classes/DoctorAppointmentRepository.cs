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

        public async Task<Appointment> CompleteAppointmentAsync(string docorId, string AppointmentId)
        {
            var appoitnemnt =await _dbContext.Appointments.FirstOrDefaultAsync(a => a.Id == AppointmentId && a.DoctorId == docorId);
            if (appoitnemnt == null)
            {
                throw new Exception("the docotrId or AppointmentId is not Right Please enter the right data");
            }
            else if(appoitnemnt.Status == AppointmentStatus.Completed)
            {
                throw new Exception("This Appointment is already Completed");
            }
           appoitnemnt.Status = AppointmentStatus.Completed;
            await _dbContext.SaveChangesAsync();
            return appoitnemnt;

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
