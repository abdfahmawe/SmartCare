using SmartCare.DAL.DTO.Requists;
using SmartCare.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.BLL.Services.Interfaces
{
    public interface IDoctorMedicalRecordservices
    {
        Task CreateMedicalRecord(string doctorId, string appointmentId , MedicalRecordRequist medicalRecordRequist);
    }
}
