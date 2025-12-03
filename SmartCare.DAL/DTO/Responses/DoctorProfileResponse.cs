using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.DTO.Responses
{
   public class DoctorProfileResponse
    {
        public string Specialization { get; set; }
        public string City { get; set; }
        public string EmergencyPhone { get; set; }
        //
        public string FullName {get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //
        public string DepartmentName { get; set; }
    }
}
