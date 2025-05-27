using WebService.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebService.API.Models.UserModels;
using WebService.API.Models;
using System.Data;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WebService.API.Helpers;
using WebService.API.Services.IServices;
using WebService.API.Data.Entity;

namespace WebService.API.Services.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly WebAPIDb _context;
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMailRepository _mailService;

        public UserRepository(WebAPIDb context,
                           UserManager<User> userManager,
                           RoleManager<Role> roleManager,
                           IMailRepository mailService,
                           IConfiguration config)
        {
            _context = context;
            _config = config;
            _userManager = userManager;
            _roleManager = roleManager;
            _mailService = mailService;
        }

        //All Users
        public async Task<ApiResponse> GetUsers()
        {
            var userAll = await _userManager.Users.ToListAsync();
            return new ApiResponse
            {
                IsSuccess = true,
                Result = userAll
            };
        }

        //GetUserByID
        public async Task<ApiResponse> GetUserbyId(string id)
        {
            var userById = await _userManager.FindByIdAsync(id);
            return new ApiResponse
            {
                IsSuccess = true,
                Result = userById
            };
        }

        //Create User
        public async Task<ApiResponse> CreateUser(RegisterUser model)
        {
            if (model == null)
                throw new NullReferenceException("Data provided is NULL");

            if (model.Password != model.ConfirmPassword)
                return new ApiResponse
                {
                    Result = "Confirm password doesn't match the password",
                    IsSuccess = false,
                };

            //Is User Exist
            var userFound = await _userManager.FindByEmailAsync(model.Email);

            //-Not Exists
            if (userFound == null)
            {
                var identityUser = new User
                {
                    Email = model.Email,
                    UserName = model.Username,
                    NormalizedUserName = model.Username.Normalize(),
                    NormalizedEmail = model.Email.Normalize(),
                    PhoneNumber = model.PhoneNumber,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true

                };

                try
                {
                    var result = await _userManager.CreateAsync(identityUser, model.Password);

                    //Setting Roles
                    if (model.Role != null)
                    {
                        var roleCheck = await _roleManager.RoleExistsAsync(model.Role);
                        if (roleCheck != true)
                        {
                            await _userManager.AddToRoleAsync(identityUser, Convert.ToString("Basic"));
                        }
                        else
                        {
                            await _userManager.AddToRoleAsync(identityUser, Convert.ToString(model.Role));
                        }

                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(identityUser, Convert.ToString("Basic"));
                    }

                    return new ApiResponse
                    {
                        IsSuccess = true,
                        Result = result
                    };
                    //}
                }

                catch (DBConcurrencyException ex)
                {
                    return new ApiResponse()
                    {
                        IsSuccess = false,
                        Result = ex.Message
                    };
                }
            }
            //- User Exist
            return new ApiResponse
            {
                IsSuccess = false
            };

        }

        //Update User
        public async Task<ApiResponse> UpdateUser(string id, UpdateUser user)
        {
            if (user != null)
            {
                var findUser = await _userManager.FindByIdAsync(id);
                if (findUser != null)
                {

                    try
                    {
                        /*context.Users.Add(findUser.)*/
                        var updateUser = new User
                        {
                            Id = id,
                            UserName = user.Username,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNo
                        };
                        var up = _userManager.UpdateAsync(updateUser);
                        var updatedUser = await _userManager.FindByIdAsync(updateUser.Id);
                        var updatedUserResponse = new IdentityUser
                        {
                            Id = id,
                            UserName = updatedUser.UserName,
                            NormalizedUserName = updatedUser.Email,
                            Email = updatedUser.Email,
                            NormalizedEmail = updatedUser.Email,
                            PhoneNumber = updatedUser.PhoneNumber,

                        };
                        return new ApiResponse
                        {
                            IsSuccess = true,
                            Result = updatedUserResponse
                        };

                    }
                    catch (Exception ex)
                    {

                        return new ApiResponse
                        {
                            IsSuccess = false,
                            Result = ex.Message
                        };
                    }
                }
                return new ApiResponse()
                {
                    IsSuccess = false,
                    Result = "User not found!"
                };

            }
            return new ApiResponse()
            {
                IsSuccess = false,
                Result = "updating property should not null!"
            };

        }

        //Delete User
        public async Task<ApiResponse> DeleteUser(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            try
            {
                await _userManager.DeleteAsync(user);

                return new ApiResponse
                {
                    IsSuccess = true,
                    Result = " User " + id + " removed successfully!"
                };
            }
            catch
            {
                throw;
            }
            return null;

        }

        //User Exist
        public bool IsExist(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        //Additional Creating Password Hash
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}

