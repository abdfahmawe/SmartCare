using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCare.BLL.Services.Interfaces;
using SmartCare.DAL.DTO.Responses;
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
        [HttpGet("Appointments")]
        public async Task<ActionResult<List<PatientAppointmentResponse>>> GetAppointments()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var medicalRecords = await _patientAppointmentService.GetAppointmentsAsync(userId);
            return Ok(medicalRecords);
        }
    }
}
