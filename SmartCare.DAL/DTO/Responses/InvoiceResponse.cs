using SmartCare.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.DTO.Responses
{
   public class InvoiceResponse
    {
        public string Id { get; set; } 
        public DateTime IssuedAt { get; set; }  // تاريخ اصدار الفاتورة
        public DateTime? PaidAt { get; set; } // تاريخ الدفع
        public PaymentWay PaymentWay { get; set; } // طريقة الدفع
        public decimal Amount { get; set; } // المبلغ
        public bool IsPaid { get; set; } // هل تم الدفع ام لا
        public PaymentStatus PaymentStatus { get; set; } // حالة الدفع
        // navigation properties
        public string AppointmentId { get; set; }
    }
}
