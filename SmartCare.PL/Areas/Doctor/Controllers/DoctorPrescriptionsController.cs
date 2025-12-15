using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCare.BLL.Exceptions;
using SmartCare.BLL.Services.Interfaces;
using System.Security.Claims;

namespace SmartCare.PL.Areas.Doctor.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Doctor")]
    [Authorize(Roles = "Doctor")]
    public class DoctorPrescriptionsController : ControllerBase
    {
        private readonly IDoctorPrescriptionService _doctorPrescriptionService;

        public DoctorPrescriptionsController(IDoctorPrescriptionService doctorPrescriptionService)
        {
            _doctorPrescriptionService = doctorPrescriptionService;
        }
        [HttpGet("GetAllPrescriptionsByDoctorId")]
        public async Task<IActionResult> GetAllPrescriptions()
        {
            var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var prescriptions = await _doctorPrescriptionService.GetPrescriptionsAsync(doctorId);
            return Ok(prescriptions);
        }
        [HttpGet("GetAllPrescriptionsByAppointmentId/{appointmentId}")]
        public async Task<IActionResult> GetAllPrescriptionsByAppointmentId([FromRoute] string appointmentId)
        {
            // adfe1f00-7172-4bca-b948-3a73edb9b545
            // adfe1f00-7172-4bca-b948-3a73edb9b549  correct
            try
            {
                var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var prescriptions = await _doctorPrescriptionService.GetPrescriptionsByAppointmentIdAsync(doctorId, appointmentId);
                return Ok(prescriptions);
            }
            catch (ForbiddenException ex)
            {
                return StatusCode(403,new {message = ex.Message});
            }
            catch (Exception ex)
            {
                return StatusCode(404,new {message = ex.Message });
            }

        }
    }
}
