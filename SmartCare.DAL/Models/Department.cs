using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Models
{
   public class Department 
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Description { get; set; }
        
        // Navigation property 
        public ICollection<Doctor> Doctors { get; set; }

    }
}
