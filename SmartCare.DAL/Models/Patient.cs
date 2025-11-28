using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Models
{
    public enum Gendar
    {
        Boy,
        Girl
    }
    public enum BloodType
    {
        A_Positive,
        A_Negative,
        B_Positive,
        B_Negative,
        AB_Positive,
        AB_Negative,
        O_Positive,
        O_Negative
    }
    public class Patient : BaseModel
    {
      
        public string PhoneNumber { get; set; }
        public string EmergencyPhone { get; set; }
        public int Age { get; set; }
        public Gendar Gendar { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public BloodType BloodType { get; set; }

        // ربط مع Identity user
        public string UserId { get; set; }               // <-- NEW
        public ApplicationUser User { get; set; }        // <-- NEW (navigation)

        // relationships
        public ICollection<Appointment> Appointments { get; set; }
    }
}
