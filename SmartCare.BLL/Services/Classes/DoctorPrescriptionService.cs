using Mapster;
using SmartCare.BLL.Exceptions;
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
            //var response = prescriptions.Select(p => new PrescriptionResponse
            //{
            //    Id = p.Id,
            //    AppointmentId = p.AppointmentId,
            //    MedicineName = p.MedicineName,
            //    Dosage = p.Dosage,
            //    Frequency = p.Frequency,
            //    DurationDays = p.DurationDays,
            //    Instructions = p.Instructions
            //}).ToList();
            //return response;
            return prescriptions.Adapt<List<PrescriptionResponse>>();
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
        public async Task<PrescriptionResponse> CreatePrescriptionAsync(string doctorId , CreatePrescriptionRequist createPrescriptionRequist)
        {
            var appintmentExists = await _doctorPrescriptionRepositry.AppointmentAvalible(createPrescriptionRequist.AppointmentId);
            if (!appintmentExists)
            {
                throw new NotFoundException("Appointment does not exist.");
            }
            var belongs = await _doctorPrescriptionRepositry.AppointmentBelongsToDoctor(createPrescriptionRequist.AppointmentId, doctorId);
            if (!belongs)
            {
                throw new ForbiddenException(doctorId + " is not authorized to create prescription for this appointment.");
            }
            var prescription = new Prescription
            {
                AppointmentId = createPrescriptionRequist.AppointmentId,
                Dosage = createPrescriptionRequist.Dosage,
                DurationDays = createPrescriptionRequist.DurationDays,
                Frequency = createPrescriptionRequist.Frequency,
                Instructions = createPrescriptionRequist.Instructions,
                MedicineName = createPrescriptionRequist.MedicineName

            };
            await _doctorPrescriptionRepositry.CreateAsync(prescription);
            var response = prescription.Adapt<PrescriptionResponse>();
            return response;
        }
        public async Task UpdatePrescriptionAsync(string doctorId , string PrescriptionId , UpdatePrescriptionRequist updatePrescriptionRequist)
        {
            var prescription = await _doctorPrescriptionRepositry.GetPrescriptionAsync(PrescriptionId);
            if(prescription is null)
            {
                throw new NotFoundException("Prescription For this Id not found.");
            }
            var belongs = await _doctorPrescriptionRepositry.AppointmentBelongsToDoctor(prescription.AppointmentId, doctorId);
            if(belongs == false)
            {
                throw new ForbiddenException("You are not authorized to update this prescription.");
            }
            var updatedPrescription = updatePrescriptionRequist.Adapt(prescription);
            await _doctorPrescriptionRepositry.UpdateAsync(updatedPrescription);
        }
        public async Task DeletePrescriptionAsync(string doctorId , string PrescriptionId)
        {
            var prescription = await _doctorPrescriptionRepositry.GetPrescriptionAsync(PrescriptionId);
            if(prescription is null)
            {
                throw new NotFoundException("Prescription For this Id not found.");
            }
            var belongs = await _doctorPrescriptionRepositry.PrescriptionBelongsToDoctor(doctorId, PrescriptionId);
            if(belongs==false)
            {
                throw new ForbiddenException("You are not authorized to delete this prescription.");
            }
            await _doctorPrescriptionRepositry.DeleteAsync(prescription);

        }
    }
}
