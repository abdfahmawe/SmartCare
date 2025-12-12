using Azure.Core;
using Microsoft.EntityFrameworkCore;
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
            var workingHours = await _doctorWorkingTimeRepositry.GetWorkingTimesAsync(doctorId);
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


        public async Task DeleteWorkingTimeAsync(string doctorId, DeleteWorkingTimeRequist day)
        {
            var exist = await _doctorWorkingTimeRepositry.GetWorkingTimeAsync(doctorId ,day.Day);
            if(exist is null)
            {
                throw new Exception("Working time for this doctor and day does not exist.");
            }
            var hasFutureAppointments = await _doctorWorkingTimeRepositry.HasFutureAppointmentsAsync(doctorId, day.Day);
            if(hasFutureAppointments)
            {
                throw new Exception("Cannot delete working time because there are future appointments scheduled on this day.");
            }
          await _doctorWorkingTimeRepositry.DeleteWorkingTimeAsync(exist);
        }

        public async Task SetWorkingHoursAsync(string doctorId, DoctorWorkingTimeRequist workingTimeRequist)
        {
            var exist = await _doctorWorkingTimeRepositry.GetWorkingTimeAsync(doctorId , workingTimeRequist.Day);
            if(exist is not null)
            {
                throw new Exception("Working time for this doctor and day already exists.");
            }
            var workingTime = new WorkingTime
            {
                Day = workingTimeRequist.Day,
                StartTime = workingTimeRequist.StartTime,
                EndTime = workingTimeRequist.EndTime,
                DoctorId = doctorId
            };
           
             await _doctorWorkingTimeRepositry.AddWorkingTimeAsync(workingTime);
       

        }

        public async Task UpdateWorkingTimeAsync(string doctorId, DoctorWorkingTimeRequist requist)
        {
            var exist = await _doctorWorkingTimeRepositry.GetWorkingTimeAsync(doctorId, requist.Day);
            if (exist is null)
            {
                throw new Exception("Working time for this doctor and day does not exist.");
            }
            var hasFutureAppointment = await _doctorWorkingTimeRepositry.hasFutureConflictingAppointments(doctorId, requist);
            if (hasFutureAppointment)
            {
                throw new Exception("Cannot update working time because there are future appointments scheduled on this day.");
            }
            exist.StartTime = requist.StartTime;
            exist.EndTime = requist.EndTime;
            await _doctorWorkingTimeRepositry.UpdateWorkingTimeAsync(exist);
        }

    }
}
