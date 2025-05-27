using WebService.API.Data.Entity;
using WebService.API.Models;
using WebService.API.Models.AuthModels;
using WebService.API.Models.UserModels;

namespace WebService.API.Services.IServices
{
    public interface IAuthRepository
    {

        Task<ApiResponse> RegisterUser(RegisterUser model);

        Task<ApiResponse> LoginUser(AuthUser model);

        Task<ApiResponse> ConfirmEmail(string userId, string token);

        Task<ApiResponse> ForgetPassword(string email);

        Task<ApiResponse> ResetPassword(ResetPasswordModel model);
        Task<ApiResponse> Logout();
    }
}
