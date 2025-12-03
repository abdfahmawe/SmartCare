using Microsoft.AspNetCore.Identity.UI.Services;
using SmartCare.BLL.Services.Interfaces;
using SmartCare.DAL.Data;
using SmartCare.DAL.DTO.Requists;
using SmartCare.DAL.DTO.Responses;
using SmartCare.DAL.Models;
using SmartCare.DAL.Repositries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.BLL.Services.Classes
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepositry _doctorRepositry;
        private readonly IEmailSender _emailSender;

        public DoctorService(IDoctorRepositry doctorRepositry  , IEmailSender emailSender )
        {
            _doctorRepositry = doctorRepositry;
            _emailSender = emailSender;
        }
        public async Task<DoctorProfileResponse> GetProfileAsync(string doctorId)
        {
            var doctorProfile = await _doctorRepositry.GetProfileAsync(doctorId);
            var response = new DoctorProfileResponse
            {
                City = doctorProfile.City,
                DepartmentName = doctorProfile.Department.Name,
                Email = doctorProfile.Email,
                EmergencyPhone = doctorProfile.EmergencyPhone,
                FullName = doctorProfile.FullName,
                PhoneNumber = doctorProfile.PhoneNumber,
                Specialization = doctorProfile.Specialization
            };
            return response;
        }

        public async Task<List<WorkingTimeResponse>> GetWorkingHoursAsync(string doctorId)
        {
            var workingHours = await _doctorRepositry.GetWorkingTimeAsync(doctorId);
            var response = new List<WorkingTimeResponse>();
            foreach(var item in workingHours)
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

        public async Task<string> UpdateProfileAsync(string doctorId, UpdateDoctorRequist requist)
        {
           
            var doctor = await _doctorRepositry.GetProfileAsync(doctorId);
            doctor.FullName = requist.FullName;
            doctor.PhoneNumber = requist.PhoneNumber;
            doctor.EmergencyPhone = requist.EmergencyPhone;
            doctor.City = requist.City;
            var result = await _doctorRepositry.UpdateDoctorAsync(doctor);
            await _emailSender.SendEmailAsync(doctor.Email, "Profile Updated", "Your profile has been successfully updated.");

            return result;
        }
    }
}
