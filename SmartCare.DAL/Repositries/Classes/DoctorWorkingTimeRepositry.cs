using Microsoft.EntityFrameworkCore;
using SmartCare.DAL.Data;
using SmartCare.DAL.DTO.Requists;
using SmartCare.DAL.Models;
using SmartCare.DAL.Repositries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Repositries.Classes
{
    public class DoctorWorkingTimeRepositry : IDoctorWorkingTimeRepositry
    {
        private readonly ApplicationDbContext _dbContext;

        public DoctorWorkingTimeRepositry(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<WorkingTime>> GetWorkingTimeAsync(string doctorId)
        {
            var workingTimes = await _dbContext.WorkingTimes.Include(i => i.Doctor).Where(w => w.DoctorId == doctorId).ToListAsync();
            return workingTimes;
        }
        public async Task<string> DeleteWorkingTimeAsync(string doctorId, DeleteWorkingTimeRequist dayOfWeek)
        {
            var workingTimeExist =await _dbContext.WorkingTimes.FirstOrDefaultAsync(wt => wt.DoctorId == doctorId && wt.Day == dayOfWeek.Day);
            if(workingTimeExist is null)
            {
                throw new Exception("Working time not found for the specified doctor and day.");
            }
            _dbContext.WorkingTimes.Remove(workingTimeExist);
            await _dbContext.SaveChangesAsync();
            return "removed successfully.";
        }

        public async Task<WorkingTime> SetWorkingTimeAsync(WorkingTime workingTime)
        {
            var exist = _dbContext.WorkingTimes.FirstOrDefault(wt => wt.DoctorId == workingTime.DoctorId);
            if (exist is null)
            {
                await _dbContext.WorkingTimes.AddAsync(workingTime);
                await _dbContext.SaveChangesAsync();
                return workingTime;
            }
            else
            {
                throw new Exception("Working time for this doctor already exists.");
            }
        }

        public async Task<string> UpdateWorkingTimeAsync(string doctorId, DoctorWorkingTimeRequist requist)
        {
            var workingTime = _dbContext.WorkingTimes.FirstOrDefault(wt => wt.DoctorId == doctorId && wt.Day == requist.Day);
            if (workingTime is null)
            {
                throw new Exception("Working time not found for the specified doctor and day.");
            }
            workingTime.StartTime = requist.StartTime;
            workingTime.EndTime = requist.EndTime;
            await _dbContext.SaveChangesAsync();
           
                return "Working time updated successfully.";
            
        }
    }
}
