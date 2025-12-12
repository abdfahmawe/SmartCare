using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCare.BLL.Services.Classes;
using SmartCare.BLL.Services.Interfaces;
using SmartCare.DAL.DTO.Requists;
using SmartCare.DAL.DTO.Responses;
using System.Security.Claims;

namespace SmartCare.PL.Areas.Doctor.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Doctor")]
    [Authorize(Roles = "Doctor")]
    public class DoctorWorkingTimeController : ControllerBase
    {
        private readonly IDoctorWorkingTimeService _doctorWorkingTimeService;

        public DoctorWorkingTimeController(IDoctorWorkingTimeService doctorWorkingTimeService)
        {
            _doctorWorkingTimeService = doctorWorkingTimeService;
        }
        [HttpGet("GetWorkingHours")]
        public async Task<ActionResult<List<WorkingTimeResponse>>> GetWorkingHours()
        {
            var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var workingHours = await _doctorWorkingTimeService.GetWorkingHoursAsync(doctorId);
            return Ok(workingHours);
        }
        [HttpPost("SetWorkingHours")]
        public async Task<ActionResult> SetWorkingHours([FromBody] DoctorWorkingTimeRequist requist)
        {
            var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _doctorWorkingTimeService.SetWorkingHoursAsync(doctorId , requist);
            return Ok(new {message="working time added successfuly"});
        }
        [HttpPut("UpdateWorkingHours")]
        public async Task<ActionResult<string>> UpdateWorkingHours([FromBody]DoctorWorkingTimeRequist requist)
        {
            var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
             await _doctorWorkingTimeService.UpdateWorkingTimeAsync(doctorId , requist);
            return Ok(new {message = "Updated Successfuly"});
        }
        [HttpDelete("DeleteWorkingTime")]
        public async Task<ActionResult<string>> DeleteWorkingTime([FromBody] DeleteWorkingTimeRequist Day)
        {
            var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           await _doctorWorkingTimeService.DeleteWorkingTimeAsync(doctorId , Day);
            return Ok(new {message = "DeletedSuccessfuly"});
        }
    }
}
