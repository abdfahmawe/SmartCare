using SmartCare.BLL.Services.Interfaces;
using SmartCare.DAL.DTO.Requists;
using SmartCare.DAL.DTO.Responses;
using SmartCare.DAL.Models;
using SmartCare.DAL.Repositries.Classes;
using SmartCare.DAL.Repositries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.BLL.Services.Classes
{
    public class DoctorWorkingTimeService : IDoctorWorkingTimeService
    {
        private readonly IDoctorWorkingTimeRepositry _doctorWorkingTimeRepositry;

        public DoctorWorkingTimeService(IDoctorWorkingTimeRepositry doctorWorkingTimeRepositry)
        {
            _doctorWorkingTimeRepositry = doctorWorkingTimeRepositry;
        }
        public async Task<List<WorkingTimeResponse>> GetWorkingHoursAsync(string doctorId)
        {
            var workingHours = await _doctorWorkingTimeRepositry.GetWorkingTimeAsync(doctorId);
            var response = new List<WorkingTimeResponse>();
            foreach (var item in workingHours)
            {
                response.Add(new WorkingTimeResponse()
                {
                    DoctorName = item.Doctor.FullName,
                    Day = item.Day,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                });
            }
            return response;
        }


        public async Task<string> DeleteWorkingTimeAsync(string doctorId, DeleteWorkingTimeRequist dayOfWeek)
        {
            var result = await _doctorWorkingTimeRepositry.DeleteWorkingTimeAsync(doctorId, dayOfWeek);
            return result;
        }

        public async Task<WorkingTime> SetWorkingHoursAsync(string doctorId, DoctorWorkingTimeRequist workingTimeRequist)
        {
            var workingTime = new WorkingTime
            {
                Day = workingTimeRequist.Day,
                StartTime = workingTimeRequist.StartTime,
                EndTime = workingTimeRequist.EndTime,
                DoctorId = doctorId
            };
            var result = await _doctorWorkingTimeRepositry.SetWorkingTimeAsync(workingTime);
            return result; 
        }

        public async Task<string> UpdateWorkingTimeAsync(string doctorId, DoctorWorkingTimeRequist requist)
        {
          
            var result =  await _doctorWorkingTimeRepositry.UpdateWorkingTimeAsync( doctorId , requist);
            return result;
        }

        
    }
}
