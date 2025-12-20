using SmartCare.DAL.DTO.Requists;
using SmartCare.DAL.DTO.Responses;
using SmartCare.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.BLL.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<InvoiceResponse> GenarateInvoice(string appointmentId, CreateInvoiceRequist createInvoice);
        Task<List<InvoiceResponse>> GetAllAsync();
        Task<InvoiceResponse> GetByAppointmentIdAsync(string appointmentId);
        Task<InvoiceResponse> MarkAsPaidAsync(string invoiceId, MarkAsPaidInvoiceRequist requist);
    }
}
