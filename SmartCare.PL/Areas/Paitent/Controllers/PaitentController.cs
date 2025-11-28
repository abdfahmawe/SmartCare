using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCare.BLL.Services.Interfaces;
using SmartCare.DAL.DTO.Responses;

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
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var result = await _pateintService.GetPatientByIdAsync(userId);
            return Ok(result);
        }
    }
}
