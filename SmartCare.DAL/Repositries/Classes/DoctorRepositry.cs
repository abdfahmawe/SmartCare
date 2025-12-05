using Microsoft.EntityFrameworkCore;
using SmartCare.DAL.Data;
using SmartCare.DAL.DTO.Responses;
using SmartCare.DAL.Models;
using SmartCare.DAL.Repositries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Repositries.Classes
{
    public class DoctorRepositry : IDoctorRepositry
    {
        private readonly ApplicationDbContext _dbContext;

        public DoctorRepositry(ApplicationDbContext dbContext  )
        {
            _dbContext = dbContext;
        }
        public async Task<Doctor> GetProfileAsync(string doctorId)
        {
            var doctor = await _dbContext.Doctors.Include(de => de.Department).FirstOrDefaultAsync(d => d.Id == doctorId);
            return doctor;
        }

       

        public async Task<string> UpdateDoctorAsync(Doctor doctor)
        {
          
           _dbContext.Doctors.Update(doctor);
            await _dbContext.SaveChangesAsync();
            return "Updated Successfully";
        }
    }
}
