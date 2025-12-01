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
    public class PatientRepositry : IPatientRepositry
    {
        private readonly ApplicationDbContext _dbContext;

        public PatientRepositry(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Patient> GetPaitentByIdAsync(string UserId)
        {
            var patient = await _dbContext.Patients.FirstOrDefaultAsync(i => i.Id == UserId);
            return patient;
        }
        public async Task UpdatePatientAsync(Patient patient)
        {
            _dbContext.Patients.Update(patient);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<MedicalRecord>> GetMedicalRecordsByIdAsync(string UserId)
        {
            var records = await _dbContext.MedicalRecords.Where(i=>i.Appointment.PatientId == UserId).ToListAsync();
            return records;
        }
        public Task<List<Appointment>> GetAppointmentsAsync(string UserId)
        {
            var appointments = _dbContext.Appointments.Where(a=> a.PatientId == UserId)
                .Include(a => a.Doctor)
                .Include(a => a.MedicalRecord)
                .ToListAsync();
            return appointments;
        }
        
        public async Task<List<Prescription>> GetPrescriptions(string UserId)
        {
            var prescriptions = await _dbContext.Prescriptions.Include(p=>p.Appointment)
                .ThenInclude(a=>a.Doctor)
                .Where(p => p.Appointment != null && p.Appointment.PatientId == UserId)
                .ToListAsync();
             return prescriptions;
        }
    }
}
