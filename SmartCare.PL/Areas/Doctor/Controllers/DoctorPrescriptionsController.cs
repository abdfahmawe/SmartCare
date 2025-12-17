using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCare.BLL.Exceptions;
using SmartCare.BLL.Services.Interfaces;
using SmartCare.DAL.DTO.Requists;
using System.Security.Claims;
using System.Security.Cryptography;

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
        [HttpGet("AllPrescriptionsByDoctorId")]
        public async Task<IActionResult> GetAllPrescriptions()
        {
            var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var prescriptions = await _doctorPrescriptionService.GetPrescriptionsAsync(doctorId);
            return Ok(prescriptions);
        }
        [HttpGet("AllPrescriptionsByDoctorIdAndAppointmentId/{appointmentId}")]
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
        [HttpPost("CreatePrescription")]
        public async Task<IActionResult> CreatePrescription(CreatePrescriptionRequist createPrescriptionRequist)
        {
            var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var prescription = await _doctorPrescriptionService.CreatePrescriptionAsync(doctorId, createPrescriptionRequist);
                return Ok(new { message = "Prescription created successfully", prescription });
            }
            catch (ForbiddenException ex)
            {
                return StatusCode(403, new { message = ex.Message });
            }
            catch(NotFoundException ex)
            {
                return StatusCode(404, new { message = ex.Message });
            }

            
        }
        [HttpPut("UpdatePrescription/{prescriptionId}")]
        public async Task<IActionResult> UpdatePrescription([FromRoute] string prescriptionId , [FromBody] UpdatePrescriptionRequist requist)
        {
            try
            {
                var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _doctorPrescriptionService.UpdatePrescriptionAsync(doctorId, prescriptionId, requist);
                return Ok(new {message = "Prescription updated successfully" });
            }
            catch(NotFoundException ex)
            {
                return StatusCode(404, new { message = ex.Message });
            }
            catch(ForbiddenException ex)
            {
                return StatusCode(403, new { message = ex.Message });
            }
           
        }
        [HttpDelete("DeletePrescription/{prescriptionId}")]
        public async Task<IActionResult> DeletePrescription([FromRoute] string prescriptionId)
        {
            try
            {
                var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _doctorPrescriptionService.DeletePrescriptionAsync(doctorId , prescriptionId);
                return Ok(new {message = "Prescription deleted successfully" });
            }
            catch (NotFoundException ex)
            {
                return StatusCode(404,new {message = ex.Message });
            }
            catch(ForbiddenException ex)
            {
                return StatusCode(403, new { message = ex.Message });
            }
        }
    }
}
