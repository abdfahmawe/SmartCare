using Microsoft.EntityFrameworkCore;
using SmartCare.DAL.Data;
using SmartCare.DAL.Models;
using SmartCare.DAL.Repositries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Repositries.Classes
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public InvoiceRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Invoice> AddAsync(Invoice invoice)
        {
            await _dbContext.AddAsync(invoice);
            await _dbContext.SaveChangesAsync();
            return invoice;
        }
        public async Task<Invoice?> GetByIdAsync(string invoiceId)
        {
            return await _dbContext.Invoices.FirstOrDefaultAsync(i => i.Id == invoiceId);
        }
        public async Task<bool> GetInvoiceByAppointmentId(string appointmentId)
        {
            return await _dbContext.Invoices.AnyAsync(i => i.AppointmentId == appointmentId);
            
        }
        public async Task<List<Invoice>> GetAllAsync()
        {
            return await _dbContext.Invoices.ToListAsync();
        }
        public async Task<Invoice> MarkAsPaidAsync(Invoice invoice)
        {
            
            await _dbContext.SaveChangesAsync();
            return invoice;
        }
    }
}
