using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.BLL.Services.Interfaces
{
   public interface IAppointmentService
    {
        Task MarkNoShowsAsync();

    }
}
