using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.DTO.Requists
{
   public class CreatePrescriptionRequist
    {
        public string MedicineName { get; set; }     // اسم الدواء => Acamol
        public string Dosage { get; set; }           // الجرعة => for example "500mg"
        public string Frequency { get; set; }        // عدد المرات يوميًا => for example "3 times a day"
        public int DurationDays { get; set; }        // عدد الأيام => for example 7
        public string? Instructions { get; set; }      // تعليمات => for example "Take after meals"
        public string AppointmentId { get; set; }
    }
}
