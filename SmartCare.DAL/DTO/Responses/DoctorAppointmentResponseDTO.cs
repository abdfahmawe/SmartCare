using SmartCare.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.DTO.Responses
{
   public class DoctorAppointmentResponseDTO
    {
        public string Id { get; set; }
        public AppointmentStatus Status { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public int DurationMinutes { get; set; } = 30;
        //
        public string PatientName { get; set; }
        public string PatientId { get; set; }
    }
}
