using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCare.BLL.Services.Interfaces;
using SmartCare.DAL.DTO.Requists;
using System.Security.Claims;

namespace SmartCare.PL.Areas.Doctor.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Doctor")]
    [Authorize(Roles = "Doctor")]
    public class DoctorMedicalRecordsController : ControllerBase
    {
        private readonly IDoctorMedicalRecordservices _doctorMedicalRecordservices;

        public DoctorMedicalRecordsController(IDoctorMedicalRecordservices doctorMedicalRecordservices)
        {
            _doctorMedicalRecordservices = doctorMedicalRecordservices;
        }
        [HttpPost("CreateMedicalRecord/{appointmentId}")]
        public async Task<IActionResult> CreateMedicalRecord([FromRoute]string appointmentId , MedicalRecordRequist medicalRecordRequist)
        {
            var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           await _doctorMedicalRecordservices.CreateMedicalRecord(doctorId, appointmentId , medicalRecordRequist);
            return Ok(new { message = "MedicalRecord Created Successfuly" });
        }
        [HttpGet("GetMedicalRecord/{appointmentId}")]
        public async Task<IActionResult> GetMedicalRecord([FromRoute] string appointmentId)
        {
            var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result =await _doctorMedicalRecordservices.GetMedicalRecordByAppointmentIdAsync(doctorId, appointmentId);
            return Ok(result);
        }
        [HttpGet("GetAllMedicalRecords")]
        public async Task<IActionResult> GetAllMedicalRecords()
        {
            var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _doctorMedicalRecordservices.GetAllMedicalRecordsAsync(doctorId);
            return Ok(result);
        }
    }
}
