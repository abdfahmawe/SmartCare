using SmartCare.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Repositries.Interfaces
{
   public interface IDoctorMedicalRecordRepositry
    {
        Task CreateMedicalRecord(string doctorId, MedicalRecord medicalRecord);
    }
}
