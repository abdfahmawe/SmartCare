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
    public class DoctorPrescriptionRepositry : IDoctorPrescriptionRepositry
    {
        private readonly ApplicationDbContext _dbContext;

        public DoctorPrescriptionRepositry(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Prescription>> GetAllPrescriptions(string doctorID)
        {
            return await _dbContext.Prescriptions
                .Where(p=>p.Appointment.DoctorId == doctorID).ToListAsync();
        }
        public async Task<List<Prescription>> GetAllPrescriptionsByAppointmentId(string doctorId , string appointmentId)
        {
            return await _dbContext.Prescriptions
                .Where(p => p.AppointmentId == appointmentId && doctorId == p.Appointment.DoctorId).ToListAsync();
        }
        public async Task<bool> AppointmentBelongsToDoctor(string AppointmentId , string DoctorID)
        {
            return await _dbContext.Appointments.AnyAsync(a=>a.DoctorId == DoctorID && a.Id == AppointmentId);
           
        }
        public async Task<bool> AppointmentAvalible (string AppointmentId)
        {
            return await _dbContext.Appointments.AnyAsync(a=>a.Id == AppointmentId);
        }
    }
}
