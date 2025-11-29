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
    }
}
