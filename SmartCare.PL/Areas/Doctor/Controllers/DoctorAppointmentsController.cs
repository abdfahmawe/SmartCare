using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCare.BLL.Services.Interfaces;
using SmartCare.DAL.DTO.Responses;
using System.Security.Claims;

namespace SmartCare.PL.Areas.Doctor.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Doctor")]
    [Authorize(Roles ="Doctor")]
    public class DoctorAppointmentsController : ControllerBase
    {
        private readonly IDoctorAppointmentService _doctorAppointmentService;

        public DoctorAppointmentsController(IDoctorAppointmentService doctorAppointmentService)
        {
            _doctorAppointmentService = doctorAppointmentService;
        }
        [HttpGet("GetAllAppointmentOnlyScheduled")]
        public async Task<ActionResult<List<DoctorAppointmentResponseDTO>>> GetAllAppointment([FromQuery]bool onlySchedueld = true)
        {
            var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _doctorAppointmentService.GetAllAppointmentsAsync(doctorId , onlySchedueld);
            return Ok(result);
        }
        [HttpPatch("CompleteAppointment/{appointmentId}")]
        public async Task<IActionResult> CompleteAppointment([FromRoute] string appointmentId)
        {
            var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _doctorAppointmentService.CompleteAppointmentAsync(doctorId, appointmentId);
            // Implementation for completing the appointment goes here
            return Ok(new { message = "Appointment completed Successfuly"});
        }
    }
}
