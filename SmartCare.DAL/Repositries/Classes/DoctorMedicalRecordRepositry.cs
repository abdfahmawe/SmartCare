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
    }
}
