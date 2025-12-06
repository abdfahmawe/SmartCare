using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.DTO.Responses
{
   public class AvailableSlotsResponse
    {
        public string DoctorId { get; set; }
        public DateTime Date { get; set; }
        public int SlotMinutes { get; set; }
        public List<AvailableSlotDto> Slots { get; set; } = new();

    }
}
