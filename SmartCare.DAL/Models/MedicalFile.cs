using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Models
{
   public class MedicalFile 
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FileUrl { get; set; }
        public string FileType { get; set; }      // image/jpeg - application/pdf
        public string Description { get; set; }

        // Relationships
        public string AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
    }
}
