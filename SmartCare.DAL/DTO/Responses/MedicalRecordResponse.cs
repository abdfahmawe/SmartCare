using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.DTO.Responses
{
   public class MedicalRecordResponse
    {
        public string Id { get; set; }
        public string Diagnosis { get; set; }          // التشخيص
        public string Symptoms { get; set; }           // الأعراض
        public string Notes { get; set; }              // ملاحظات الدكتور
        public string VitalSigns { get; set; }         // العلامات الحيوية
        public string TestsNeeded { get; set; }        // الفحوصات المطلوبة
        public string Allergies { get; set; }          // الحساسية إن وجدت
        public DateTime CreatedAt { get; set; }
        public string AppointmentId { get; set; }
    }
}
