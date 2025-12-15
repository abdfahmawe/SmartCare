using SmartCare.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Repositries.Interfaces
{
   public interface IDoctorPrescriptionRepositry
    {
        public Task<List<Prescription>> GetAllPrescriptions(string doctorID);
        Task<bool> AppointmentBelongsToDoctor(string AppointmentId, string DoctorID);
        Task<bool> AppointmentAvalible(string AppointmentId);
        public Task<List<Prescription>> GetAllPrescriptionsByAppointmentId(string doctorId, string appointmentId);
    }
}
