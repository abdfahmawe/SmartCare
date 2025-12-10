using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Models
{
   public enum AppointmentStatus
    {
        Scheduled,
        Completed,
        Canceled,
        NoShow
    }
    public class Appointment 
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public AppointmentStatus Status { get; set; } 
        public DateTime StartAt { get; set; } // want to store in UTC format 
        public DateTime EndAt { get; set; } // want to store in UTC format
        public int DurationMinutes { get; set; } = 30;
        // navigation properties
        public string PatientId { get; set; }
        public Patient Patient { get; set; }
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public string? InvoiceId { get; set; }
        public Invoice? Invoice { get; set; } // هل دفع او لا والطريقة والوقت 
        public string? MedicalRecordId { get; set; }
        public MedicalRecord? MedicalRecord { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; } // وصفة طبية 
        public ICollection<MedicalFile> MedicalFiles { get; set; } // صورة اشعة او اشي زي هيك


    }
}
