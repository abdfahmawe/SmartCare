using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _pateintService;

        public PatientController(IPatientService pateintService)
        {
            _pateintService = pateintService;
        }
        [HttpGet("Profile")]
        public async Task<ActionResult<PatientProfileResponse>> Profile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _pateintService.GetPatientByIdAsync(userId);
            return Ok(result);
        }
        [HttpPut("UpdateProfile")]
        public async Task<ActionResult<Patient>> UpdateProfile([FromBody]UpdatePatientRequist dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _pateintService.UpdateProfileAsync(userId , dto);
            return result;
        }
        [HttpGet("MedicalRecords")]
        public async Task<ActionResult<List<MedicalRecord>>> GetMedicalRecords()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var medicalRecords = await _pateintService.GetMedicalRecordsAsync(userId);
            return Ok(medicalRecords);
        }
        [HttpGet("Appointments")]
        public async Task<ActionResult<List<PatientAppointmentResponse>>> GetAppointments()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var medicalRecords = await _pateintService.GetAppointmentsAsync(userId);
            return Ok(medicalRecords);
        }
        [HttpGet("Prescriptions")]
        public async Task<List<PatientPresciptionResponse>> GetPrescriptions()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var Prescriptions = await _pateintService.GetPrescriptionsAsync(userId);
            return Prescriptions;
        }
    }
}
