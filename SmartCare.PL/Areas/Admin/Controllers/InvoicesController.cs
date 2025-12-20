using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCare.BLL.Exceptions;
using SmartCare.BLL.Services.Interfaces;
using SmartCare.DAL.DTO.Requists;

namespace SmartCare.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        [HttpPost("GenerateInvoice/{appointmentId}")]
        public async Task<IActionResult> GenerateInvoice([FromRoute] string appointmentId, [FromBody] CreateInvoiceRequist createInvoice)
        {
            try
            {
                var invoice = await _invoiceService.GenarateInvoice(appointmentId, createInvoice);
                return Ok(new { message = "Invoice Created Successfuly", invoice });
            }
            catch(NotFoundException ex)
            {
                return StatusCode(404 , ex.Message);
            }
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllInvoices()
        {
            var invoices =  await _invoiceService.GetAllAsync();
            return Ok(invoices);
        }
        [HttpGet("GetInvoiceByAppointmentId/{appointmentId}")]
        public async Task<IActionResult> GetInvoiceByAppointmentId([FromRoute]string appointmentId)
        {
             var invoice = await _invoiceService.GetByAppointmentIdAsync(appointmentId);
             return Ok(invoice);
        }
        [HttpPatch("MarkAsPaid/{invoiceId}")]
        public async Task<IActionResult> MarkAsPaid([FromRoute]string invoiceId , [FromBody] MarkAsPaidInvoiceRequist requist)
        {
            try
            {
                var invoice = await _invoiceService.MarkAsPaidAsync(invoiceId, requist);
                return Ok(new { message = "Invoice Marked as Paid Successfuly", invoice });
            }
            catch(NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch(ConflictException ex)
            {
                return StatusCode(409, ex.Message);
            }
        }
    }
}
