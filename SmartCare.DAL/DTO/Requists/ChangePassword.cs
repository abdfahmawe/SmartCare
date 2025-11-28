using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.DTO.Requists
{
   public class ChangePassword
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string Token{get; set; }
    }
}
