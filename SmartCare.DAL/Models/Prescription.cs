using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Models
{
  public  class Prescription // روشتة
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string MedicineName { get; set; }     // اسم الدواء
        public string Dosage { get; set; }           // الجرعة
        public string Frequency { get; set; }        // عدد المرات يوميًا
        public int DurationDays { get; set; }        // عدد الأيام
        public string Instructions { get; set; }      // تعليمات

        //   ==============================================
        public string AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
    }
}
