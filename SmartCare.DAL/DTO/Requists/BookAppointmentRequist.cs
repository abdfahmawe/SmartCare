using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.DTO.Requists
{
   public class BookAppointmentRequist
    {
        public string DoctorId { get; set; }
        public DateTime StartAt { get; set; }
    }
}
