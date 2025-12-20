using SmartCare.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Repositries.Interfaces
{
   public interface IInvoiceRepository
    {
        Task<Invoice> AddAsync(Invoice invoice);
        Task<bool> GetInvoiceByAppointmentId(string appointmentId);
        Task<List<Invoice>> GetAllAsync();
        Task<Invoice> MarkAsPaidAsync(Invoice invoice);
        Task<Invoice?> GetByIdAsync(string invoiceId);
    }
}
