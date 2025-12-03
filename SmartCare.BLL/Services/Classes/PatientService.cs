using Mapster;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using SmartCare.BLL.Services.Interfaces;
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
    public class PatientService : IPatientService
    {
        private readonly IPatientRepositry _patientRepositry;
        private readonly IEmailSender _emailSender;

        public PatientService(IPatientRepositry patientRepositry , IEmailSender emailSender)
        {
            _patientRepositry = patientRepositry;
            _emailSender = emailSender;
        }
        public async Task<PatientProfileResponse> GetPatientByIdAsync(string UserId)
        {
            var result = await _patientRepositry.GetPaitentByIdAsync(UserId);
            var resultAfterAdapt = result.Adapt<PatientProfileResponse>();
            return resultAfterAdapt;
        }
        public async Task<Patient> UpdateProfileAsync(string UserId, UpdatePatientRequist dto)
        {
            var existingUser = await _patientRepositry.GetPaitentByIdAsync(UserId);
            var updatedPatient = dto.Adapt(existingUser);
            await _patientRepositry.UpdatePatientAsync(updatedPatient);
            await _emailSender.SendEmailAsync(updatedPatient.Email , "Profile Updated", "Your profile has been successfully updated.");
            return updatedPatient;
        }

        public async Task<List<MedicalRecord>> GetMedicalRecordsAsync(string UserId)
        {
            var records = await _patientRepositry.GetMedicalRecordsByIdAsync(UserId);
            return records;
        }
      
        public async Task<List<PatientPresciptionResponse>> GetPrescriptionsAsync(string UserId)
        {
            var prescriptions = await _patientRepositry.GetPrescriptions(UserId);
            var prescriptionsAfterAdapt = new List<PatientPresciptionResponse>();
            foreach(var pre in prescriptions)
            {
                prescriptionsAfterAdapt.Add(new PatientPresciptionResponse()
                {
                    Dosage = pre.Dosage,
                    Frequency = pre.Frequency,
                    MedicineName = pre.MedicineName,
                    DurationDays = pre.DurationDays,
                    Instructions = pre.Instructions,
                    StartAt = pre.Appointment.StartAt,
                    DoctorName = pre.Appointment.Doctor.FullName
                });
            }
            return prescriptionsAfterAdapt;
        }
    }
}
