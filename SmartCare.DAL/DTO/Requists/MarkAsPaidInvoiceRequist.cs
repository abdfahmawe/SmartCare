using SmartCare.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.DTO.Requists
{
    public class MarkAsPaidInvoiceRequist
    {
        public PaymentWay PaymentWay { get; set; } // طريقة الدفع
    
    }
}
