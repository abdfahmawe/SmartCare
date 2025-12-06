using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCare.BLL.Services.Interfaces;
using SmartCare.DAL.DTO.Responses;

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
        
      
    }
}
