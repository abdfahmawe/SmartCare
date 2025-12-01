using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.DTO.Responses
{
    public class PatientPresciptionResponse
    {
        public string MedicineName { get; set; }     // اسم الدواء
        public string Dosage { get; set; }           // الجرعة
        public string Frequency { get; set; }        // عدد المرات يوميًا
        public int DurationDays { get; set; }        // عدد الأيام
        public string Instructions { get; set; }      // تعليمات
        public DateTime StartAt { get; set; }
        public string DoctorName { get; set; }
    }
}
