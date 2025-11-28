using SmartCare.DAL.DTO.Requists;
using SmartCare.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.BLL.Services.Interfaces
{
    public interface IAuthenticationUser
    {
       Task<UserResponse> LoginAsync(LoginRequist loginRequist);
       Task<UserResponse> RegisterAsync(RegisterRequist registerRequist);
       Task<string> ConfirmEmail(string token, string UserId);
       Task<string> ResetPassword(ResetPassword resetPassword);
       Task<string> ChangePassword(ChangePassword changePassword);
    }
}
