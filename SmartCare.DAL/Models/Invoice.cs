using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Models
{
    public enum PaymentWay
    {
        Cash,
        Visa
    }
    public enum PaymentStatus
    {
        Pending,
        Approved
    }
    public class Invoice : BaseModel
    {
        public decimal Amount { get; set; }
        public DateTime IssuedAt { get; set; } = DateTime.Now;
        public bool IsPaid { get; set; }
        public PaymentWay PaymentWay { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        //
        public string AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
    }
}
