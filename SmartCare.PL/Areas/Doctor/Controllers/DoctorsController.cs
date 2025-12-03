using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCare.BLL.Services.Interfaces;
using SmartCare.DAL.DTO.Requists;
using SmartCare.DAL.DTO.Responses;
using SmartCare.DAL.Models;
using System.Security.Claims;

namespace SmartCare.PL.Areas.Doctor.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Doctor")]
    [Authorize(Roles = "Doctor")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        [HttpGet("Profile")]
        public async Task<ActionResult<DoctorProfileResponse>> GetProfile()
        {
            var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profile = await _doctorService.GetProfileAsync(doctorId);
            return Ok(profile);
        }
        [HttpPatch("UpdateProfile")]
        public async Task<ActionResult<string>> UpdateProfile([FromBody]UpdateDoctorRequist requist)
        {
            var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var updateResult = await _doctorService.UpdateProfileAsync(doctorId , requist);
            return Ok(updateResult);
        }
        [HttpGet("GetWorkingHours")]
        public async Task<ActionResult<List<WorkingTimeResponse>>> GetWorkingHours()
        {
            var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var workingHours = await _doctorService.GetWorkingHoursAsync(doctorId);
            return Ok(workingHours);
        }
    }
}
