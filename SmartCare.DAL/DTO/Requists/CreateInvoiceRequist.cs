using SmartCare.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.DTO.Requists
{
    public class CreateInvoiceRequist
    {
        public decimal Amount { get; set; }
        public PaymentWay PaymentWay { get; set; } // Cash أو Visa

    }
}
