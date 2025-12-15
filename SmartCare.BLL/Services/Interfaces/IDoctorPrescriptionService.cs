using SmartCare.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.BLL.Services.Interfaces
{
   public interface IDoctorPrescriptionService
    {
        Task<List<PrescriptionResponse>> GetPrescriptionsAsync(string doctorID);
        Task<List<PrescriptionResponse>> GetPrescriptionsByAppointmentIdAsync(string doctorId, string appointmentId);
    }
}
