using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Models
{
   public class MedicalRecord 
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Diagnosis { get; set; }          // التشخيص
        public string Symptoms { get; set; }           // الأعراض
        public string Notes { get; set; }              // ملاحظات الدكتور
        public string VitalSigns { get; set; }         // العلامات الحيوية
        public string TestsNeeded { get; set; }        // الفحوصات المطلوبة
        public string Allergies { get; set; }          // الحساسية إن وجدت
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        //
        public string AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
    }
}
