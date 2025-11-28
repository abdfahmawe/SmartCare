using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Models
{
   public class BaseModel
    {
       public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
