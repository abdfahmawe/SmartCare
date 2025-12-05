using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartCare.PL.Areas.Doctor.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Doctor")]
    [Authorize(Roles ="Doctor")]
    public class DoctorAppointmentsController : ControllerBase
    {
        public DoctorAppointmentsController()
        {

        }

    }
}
