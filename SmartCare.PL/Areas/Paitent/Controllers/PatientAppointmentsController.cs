using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCare.BLL.Services.Classes;
using SmartCare.BLL.Services.Interfaces;
using SmartCare.DAL.DTO.Requists;
using SmartCare.DAL.DTO.Responses;
using SmartCare.DAL.Models;
using System.Security.Claims;

namespace SmartCare.PL.Areas.Paitent.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Patient")]
    [Authorize(Roles = "Patient")]
    public class PatientAppointmentsController : ControllerBase
    {
        private readonly IPatientAppointmentService _patientAppointmentService;

        public PatientAppointmentsController(IPatientAppointmentService patientAppointmentService)
        {
            _patientAppointmentService = patientAppointmentService;
        }
        [HttpGet("AvailableSlots")]
        public async Task<ActionResult<AvailableSlotsResponse>> GetSlots([FromQuery] string doctorId, [FromQuery] DateTime date)
        {
            var result = await _patientAppointmentService.GetAvailableSlotsAsync(doctorId, date);
            return Ok(result);
        }
        [HttpGet("GetAllDoctorsWithIds")]

        public async Task<ActionResult<List<DoctorsWithIdsResponse>>> GetAllDoctorsWithIds()
        {
            return await _patientAppointmentService.GetAllDoctorsWithIdsAsync();
        }
        [HttpGet("Appointments")]
        public async Task<ActionResult<List<PatientAppointmentResponse>>> GetAppointments()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var medicalRecords = await _patientAppointmentService.GetAppointmentsAsync(userId);
            return Ok(medicalRecords);
        }
        [HttpPost("BookAppointment")]
        public async Task<ActionResult<Appointment>> BookAppointment([FromBody] BookAppointmentRequist requist)
        {
            
            var patientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appointmentBooking = await _patientAppointmentService.BookAppointmentAsync(patientId, requist);
            return Ok(appointmentBooking);
        }
    }
}
