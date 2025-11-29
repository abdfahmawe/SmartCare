using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Models
{
   public class Doctor  : ApplicationUser
    {

        public string Specialization { get; set; }
        public string City { get; set; }
        public string EmergencyPhone { get; set; }
        // navigation property
        public string DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<WorkingTime> WorkingTimes { get; set; }
    }
}
