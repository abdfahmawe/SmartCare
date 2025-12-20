using Mapster;
using SmartCare.BLL.Exceptions;
using SmartCare.BLL.Services.Interfaces;
using SmartCare.DAL.DTO.Requists;
using SmartCare.DAL.DTO.Responses;
using SmartCare.DAL.Models;
using SmartCare.DAL.Repositries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.BLL.Services.Classes
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IAppointmentRepository _appointmentRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository , IAppointmentRepository appointmentRepository)
        {
            _invoiceRepository = invoiceRepository;
            _appointmentRepository = appointmentRepository;
        }
        public async Task<InvoiceResponse> GenarateInvoice(string appointmentId , CreateInvoiceRequist createInvoice)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null || appointment.Status != AppointmentStatus.Completed)
                throw new NotFoundException("Appointment not found or not completed.");
            var existingInvoice = await _invoiceRepository.GetInvoiceByAppointmentId(appointmentId);
            if (existingInvoice == true)
                throw new NotFoundException("Invoice already exists for this appointment.");
            var invoice = new Invoice
            {
                AppointmentId = appointmentId,
                Amount = createInvoice.Amount,
                PaymentWay = createInvoice.PaymentWay,
                PaymentStatus = PaymentStatus.Pending,
                IsPaid = false , 
                IssuedAt = DateTime.Now
            };
           var inv =  await _invoiceRepository.AddAsync(invoice);
            var reponse = inv.Adapt<InvoiceResponse>();
            return reponse;
        }
        public async Task<List<InvoiceResponse>> GetAllAsync()
        {
            var invoices = await _invoiceRepository.GetAllAsync();
            var response = invoices.Adapt<List<InvoiceResponse>>();
            return response;
        }
        public async Task<InvoiceResponse> GetByAppointmentIdAsync(string appointmentId)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null)
                throw new NotFoundException("Appointment not found.");
            var invoices = await _invoiceRepository.GetAllAsync();
            var invoice = invoices.FirstOrDefault(i => i.AppointmentId == appointmentId);
            if (invoice == null)
                throw new NotFoundException("Invoice not found for this appointment.");
            var response = invoice.Adapt<InvoiceResponse>();
            return response;
        }
        public async Task<InvoiceResponse> MarkAsPaidAsync(string invoiceId, MarkAsPaidInvoiceRequist requist)
        {
           
            var invoice =await _invoiceRepository.GetByIdAsync(invoiceId);
            if (invoice == null)
                throw new NotFoundException("Invoice not found.");
            if (invoice.IsPaid)
                throw new ConflictException("Invoice already paid.");
            invoice.IsPaid = true;
            invoice.PaidAt = DateTime.Now;
            invoice.PaymentStatus = PaymentStatus.Approved;
            invoice.PaymentWay = requist.PaymentWay;
            var updatedInvoice = await _invoiceRepository.MarkAsPaidAsync(invoice);
            var response = invoice.Adapt<InvoiceResponse>();
            return response;
        }
    }
}
