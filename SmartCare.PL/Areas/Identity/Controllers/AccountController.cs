using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SmartCare.BLL.Services.Interfaces;
using SmartCare.DAL.DTO.Requists;
using SmartCare.DAL.DTO.Responses;

namespace SmartCare.PL.Areas.Identity.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Identity")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationUser _authenticationUser;

        public AccountController(IAuthenticationUser authenticationUser)
        {
            _authenticationUser = authenticationUser;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserResponse>> Register(RegisterRequist registerRequest)
        {
            var result = await _authenticationUser.RegisterAsync(registerRequest);
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserResponse>> Login(LoginRequist loginRequest)
        {
            var result = await _authenticationUser.LoginAsync(loginRequest);
            return Ok(result);
        }
        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult<string>> ConfirmEmail([FromQuery]string token,[FromQuery]string userId)
        {
          var result =await _authenticationUser.ConfirmEmail(token, userId);
            return Ok(result);
        }
        [HttpPost("ResetPassword")]
        public async Task<ActionResult<string>> ResetPassword(ResetPassword resetPassword)
        {
            var result = await _authenticationUser.ResetPassword(resetPassword);
            return Ok(result);
        }
        [HttpPost("ChangePassword")]
        public async Task<ActionResult<string>> ChangePassword(ChangePassword changePassword)
        {
            var result = await _authenticationUser.ChangePassword(changePassword);
            return Ok(result);
        }
    }
}
