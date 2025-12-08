using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartCare.BLL.Services.Interfaces;
using SmartCare.DAL.Data;
using SmartCare.DAL.DTO.Requists;
using SmartCare.DAL.DTO.Responses;
using SmartCare.DAL.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace SmartCare.BLL.Services.Classes
{
    public class AuthenticationUser : IAuthenticationUser
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        public AuthenticationUser(ApplicationDbContext dbContext , UserManager<ApplicationUser> userManager ,   IConfiguration configuration  , IEmailSender emailSender)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;  
        }
        public async Task<UserResponse> LoginAsync(LoginRequist loginRequist)
        {
            var user =await _userManager.FindByEmailAsync(loginRequist.Email);
                 if(user is null)
                    {
                        throw new Exception("Invalid Email or Password");
                     }
              var isPasswordValid =await _userManager.CheckPasswordAsync(user, loginRequist.Password);
            if(!isPasswordValid)
            {
                throw new Exception("Invalid Email or Password");
            }
            if(!user.EmailConfirmed)
            {
                throw new Exception("Email is not confirmed yet , please check your email for confirmation link");
            }
            var token = await GenerateTokenAsync(user);
            return new UserResponse()
            {
                Token = token,
            };


        }
        private async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtOptions")["SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email , user.Email),
                new Claim(ClaimTypes.NameIdentifier , user.Id),
                new Claim(ClaimTypes.Name , user.FullName)

            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var token = new JwtSecurityToken(
               
                claims: Claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<UserResponse> RegisterAsync(RegisterRequist registerRequist)
        {
            var user = new ApplicationUser()
            {
                Email = registerRequist.Email,
                UserName = registerRequist.UserName,
                PhoneNumber = registerRequist.PhoneNumber,
                FullName = registerRequist.FullName,

            };
            var result =await _userManager.CreateAsync(user, registerRequist.Password);
            await _userManager.AddToRoleAsync(user, "Patient");
            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var escapeToken = Uri.EscapeDataString(token);
                var urlEmail = $"https://localhost:7216/api/Identity/Account/ConfirmEmail?token={escapeToken}&userId={user.Id}";
                await _emailSender.SendEmailAsync(user.Email , "Confarim Email", $"<a href={urlEmail}>click here</a>");
                return new UserResponse()
                {
                    Token = "Check Your Email to Confirm",
                };
            }
            else
            {
                throw new Exception($"erorrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr");
            }
        }

        public async Task<string> ConfirmEmail(string token ,  string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if(user is null)
            {
                throw new Exception("Invalid User Id");
            }
            var result =await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return "email confirmed successfuly" ;
            }
            else
            {
                throw new Exception("email confirmation failed");
            }
        }
        public async Task<string> ResetPassword(ResetPassword resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user is null)
            {
                throw new Exception("Invalid Email");
            }
            var random = new Random();
            var token = random.Next(1000, 4000).ToString();
            user.ResetPassword = token;
            user.ResetPasswordExpiry = DateTime.UtcNow.AddMinutes(15);
            await _userManager.UpdateAsync(user);
            await _emailSender.SendEmailAsync(resetPassword.Email, "ResetPassword", $"your token is {token}");
            return "Reset password token sent to your email";
        }

        public async Task<string> ChangePassword (ChangePassword changePassword)
        {
            var user =await _userManager.FindByEmailAsync(changePassword.Email);
            if(user is null)
            {
                throw new Exception("Invalid Email");
            }
            if(user.ResetPassword != changePassword.Token)
            {
                throw new Exception("Invalid Token");
            }
            if(user.ResetPasswordExpiry < DateTime.UtcNow)
            {
                throw new Exception("Token Expired by time");
            }
            var token =await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, changePassword.NewPassword);
          await  _userManager.UpdateAsync(user);
            await _emailSender.SendEmailAsync(user.Email , "Change Password", "Your Password has been changed successfully");
            return "Password Changed Successfully";
        }
    }
}
