using SmartCare.BLL.Exceptions;
using SmartCare.BLL.Services.Interfaces;
using SmartCare.DAL.DTO.Responses;
using SmartCare.DAL.Repositries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.BLL.Services.Classes
{
   public class DoctorPrescriptionService : IDoctorPrescriptionService
    {
        private readonly IDoctorPrescriptionRepositry _doctorPrescriptionRepositry;

        public DoctorPrescriptionService(IDoctorPrescriptionRepositry doctorPrescriptionRepositry)
        {
            _doctorPrescriptionRepositry = doctorPrescriptionRepositry;
        }
        public async Task<List<PrescriptionResponse>> GetPrescriptionsAsync(string doctorID)
        {
            var prescriptions = await _doctorPrescriptionRepositry.GetAllPrescriptions(doctorID);
            var response = prescriptions.Select(p => new PrescriptionResponse
            {
                Id = p.Id,
                AppointmentId = p.AppointmentId,
                MedicineName = p.MedicineName,
                Dosage = p.Dosage,
                Frequency = p.Frequency,
                DurationDays = p.DurationDays,
                Instructions = p.Instructions
            }).ToList();
            return response;
        }
        public async Task<List<PrescriptionResponse>> GetPrescriptionsByAppointmentIdAsync(string doctorId, string appointmentId)
        {
            var appointment = await _doctorPrescriptionRepositry.AppointmentAvalible(appointmentId);
            if(appointment == false)
            {
                throw new NotFoundException("Appointment does not exist.");
            }
            var belongs = await _doctorPrescriptionRepositry.AppointmentBelongsToDoctor(appointmentId, doctorId);
            if(belongs ==false)
            {
                throw new ForbiddenException("Appointment does not belong to the doctor.");
            }
            var prescriptions = await _doctorPrescriptionRepositry.GetAllPrescriptionsByAppointmentId(doctorId, appointmentId);
            var response = prescriptions.Select(p => new PrescriptionResponse
            {
                Id = p.Id,
                AppointmentId = p.AppointmentId,
                MedicineName = p.MedicineName,
                Dosage = p.Dosage,
                Frequency = p.Frequency,
                DurationDays = p.DurationDays,
                Instructions = p.Instructions
            }).ToList();
            return response;
        }
    }
}
