using SmartCare.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.DTO.Requists
{
   public class UpdatePatientRequist
    {
        //patient info
        public string EmergencyPhone { get; set; }
        public DateTime BirthDate { get; set; }
        public Gendar Gendar { get; set; }
        public string Address { get; set; }
        public BloodType BloodType { get; set; }
        // user info
        public string FullName { get; set; }
        public string userName { get; set; }
        public string phoneNumber { get; set; }
        public string city { get; set; }
    }
}
