using Microsoft.AspNetCore.Identity;
using WebService.API.Data.Entity;
using WebService.API.Models;
using WebService.API.Models.UserModels;

namespace WebService.API.Services.IServices
{
    public interface IUserRepository
    {
        Task<ApiResponse> GetUsers();
        Task<ApiResponse> GetUserbyId(string id);
        Task<ApiResponse> CreateUser(RegisterUser model);

        Task<ApiResponse> UpdateUser(string id, UpdateUser user);
        Task<ApiResponse> DeleteUser(string id);
        public bool IsExist(string id);

    }
}
