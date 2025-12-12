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

    public class DoctorMedicalRecordRepositry : IDoctorMedicalRecordRepositry
    {
        private readonly ApplicationDbContext _dbContext;

        public DoctorMedicalRecordRepositry(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateMedicalRecord(string doctorId, MedicalRecord medicalRecord)
        {
            var ifTheRightDoctor = await _dbContext.Appointments.FirstOrDefaultAsync(a => a.DoctorId == doctorId && medicalRecord.AppointmentId == a.Id);
            if (ifTheRightDoctor is null)
            {
                throw new Exception("You are not authorized to create a medical record for this appointment.");
            }
            if(ifTheRightDoctor.Status != AppointmentStatus.Completed)
            {
                throw new Exception("Cannot create medical record for an appointment that is not completed.");
            }
            var ifMedicalRecordExists = await _dbContext.MedicalRecords.FirstOrDefaultAsync(mr => mr.AppointmentId == medicalRecord.AppointmentId);
            if (ifMedicalRecordExists is not null)
            {
                throw new Exception("Medical record for this appointment already exists.");
            }
            _dbContext.MedicalRecords.Add(medicalRecord);
            await _dbContext.SaveChangesAsync();
           
        }

        public async Task<bool> DeleteMedicalRecordAsync(string doctorId, string appointmentId)
        {
            var medicalRecord = await _dbContext.MedicalRecords.Include(mr => mr.Appointment).FirstOrDefaultAsync(mr => mr.AppointmentId == appointmentId && mr.Appointment.DoctorId == doctorId);
            if(medicalRecord is null)
            {
                throw new Exception("Medical record not found or you are not authorized to delete it.");
            }
            _dbContext.MedicalRecords.Remove(medicalRecord);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<MedicalRecord>> GetAllMedicalRecordsAsync(string doctorId)
        {
            var medicalRecord = await _dbContext.MedicalRecords
                .Include(mr => mr.Appointment)
                .Where(mr => mr.Appointment.DoctorId == doctorId)
                .ToListAsync();
            ////if (medicalRecord is null)
            ////{
            ////    throw new Exception("there is no medicalRecord for this doctor");
            ////} there is no need because .ToListAsync will return empty list if there is no records
            return medicalRecord;
        }

        public async Task<MedicalRecord> GetMedicalRecordByAppointmentIdAsync(string doctorId, string appointmentId)
        {
            var medicalRecord = await _dbContext.MedicalRecords.Include(a=>a.Appointment).FirstOrDefaultAsync(mr=>mr.AppointmentId == appointmentId);
            if (medicalRecord is null)
            {
                throw new Exception("Medical record not found for the specified appointment.");
            }
            if(medicalRecord.Appointment.DoctorId != doctorId)
            {
                throw new Exception("You are not authorized to access this medical record.");
            }
            return medicalRecord;
        }

        public async Task UpdateMedicalRecordAsync(string doctorId, MedicalRecord medicalRecord)
        {
            
            await _dbContext.SaveChangesAsync();
        }
    }
}
