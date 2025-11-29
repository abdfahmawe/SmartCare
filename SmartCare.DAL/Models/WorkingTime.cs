using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Models
{
   public class WorkingTime 
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        //
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
