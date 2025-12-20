using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Models
{
    public enum PaymentWay
    {
        Cash,//0
        Visa//1
    }
    public enum PaymentStatus
    {
        Pending,//0
        Approved//1
    }
    public class Invoice // فاتورة
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime IssuedAt { get; set; } = DateTime.Now; // تاريخ اصدار الفاتورة
        public DateTime? PaidAt { get; set; } // تاريخ الدفع
        public PaymentWay PaymentWay { get; set; } // طريقة الدفع
        public decimal Amount { get; set; } // المبلغ
        public bool IsPaid { get; set; } // هل تم الدفع ام لا
        public PaymentStatus PaymentStatus { get; set; } // حالة الدفع
        // navigation properties
        public string AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
    }
}
