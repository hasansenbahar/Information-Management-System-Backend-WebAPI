using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebService.API.Constants;
using WebService.API.Data.Entity;
using WebService.API.Helpers;
using WebService.API.Models;
using WebService.API.Models.AuthModels;
using WebService.API.Models.UserModels;
using WebService.API.Services.IServices;

namespace WebService.API.Services.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _config;
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;
        private IMailRepository _mailService;
        private readonly IUserRepository _useService;
        private readonly SignInManager<User> _signInManager;

        public AuthRepository(IConfiguration config,
                            UserManager<User> userManager,
                            IMailRepository mailService,
                            IUserRepository userService,
                            RoleManager<Role> roleManager,
                            SignInManager<User> signInManager)
        {
            _config = config;
            _userManager = userManager;
            _roleManager = roleManager;
            _mailService = mailService;
            _useService = userService;
            _signInManager = signInManager;

        }
        public async Task<ApiResponse> Logout()
        {
            await _signInManager.SignOutAsync();
            return new ApiResponse
            {
                IsSuccess = true,
                Result = TextResources.Logout,
            };
        }
        //Register User
        public async Task<ApiResponse> RegisterUser(RegisterUser model)
        {

            var User = new User
            {
                Email = model.Email,
                UserName = model.Username,
            };

            //user creation
            var createdUser = await _useService.CreateUser(model);

            //Mail Sending
            //if (createdUser.IsSuccess != false)
            //{
            //    string confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(User);

            //    var emailToken = Uri.EscapeDataString(confirmEmailToken);
            //    var encodedEmailToken = Encoding.UTF8.GetBytes(emailToken);
            //    var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

            //    var confirmUser = await _userManager.FindByEmailAsync(User.Email);

            //    string url = $"{_config["AppUrl"]}/api/auth/ConfirmEmail?userid={confirmUser.Id}&token={validEmailToken}";

            //    var mailContent = new MailRequest
            //    {
            //        ToEmail = User.Email,
            //        Subject = "Confirm your email",
            //        Body = $"<h1>Welcome to Identity Test API</h1>" + $"<p>Hi {User.UserName} !, Please confirm your email by <a href='{url}'>Clicking here</a></p><br><strong>Email Confirmation token for ID '" + confirmUser.Id + "' : <code>" + validEmailToken + "</code></strong>"
            //    };

            //    await _mailService.SendEmailAsync(mailContent);

            //    return new Response
            //    {
            //        IsSuccess = true,
            //        Result = "User created successfully! Please confirm the your Email!",
            //    };
            //}
            return new ApiResponse
            {
                IsSuccess = true,
                Result = "User created successfully!",
            };
            return new ApiResponse
            {
                IsSuccess = false,
                Result = "User Email Already Registered, Try Login(/api/auth/Authenticate)"
            };

        }

        //Login User
        public async Task<ApiResponse> LoginUser(AuthUser model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user is null)
            {
                return new ApiResponse
                {
                    Result = TextResources.NotFound,
                    IsSuccess = false,
                };
            }
            if (user.UserName != model.UserName)
            {
                return new ApiResponse
                {
                    Result = "There is no user with that Username! ",
                    IsSuccess = false,
                };
            }
            else
            {
                var result = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!result)
                    return new ApiResponse
                    {
                        Result = "Invalid password",
                        IsSuccess = false,
                    };


                var userRole = new List<string>(await _userManager.GetRolesAsync(user));
                //Generate Token JWT
                var Token = await GenerateToken(user);

                return new ApiResponse
                {
                    Result = Token,
                    IsSuccess = true,
                };
            }

        }

        //ConfirmEmail
        public async Task<ApiResponse> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Result = "User not found"
                };
            }

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            var normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);

                return new ApiResponse
                {
                    Result = "Email confirmed successfully!",
                    IsSuccess = true,
                };
            }

            return new ApiResponse
            {
                IsSuccess = false,
                Result = "Email did not confirm",
                //Errors = result.Errors.ToArray()
            };
        }

        //Forget Password
        public async Task<ApiResponse> ForgetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new ApiResponse
                {
                    IsSuccess = false,
                    Result = "No user associated with email",
                };

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);
            string pass = "Tester@123";
            string url = $"{_config["AppUrl"]}/api/Auth/ResetPassword?Email={email}&Token={validToken}&NewPassword={pass}&ConfirmPassword={pass}";

            var mailContent = new MailRequest
            {
                ToEmail = email,
                Subject = "Reset Password",
                Body = "<h1>Follow the instructions to reset your password</h1>" +
                $"<p>To reset your password, <br><br> 1. Copy the Link :  <a href='{url}'>{url}</a><br><br> 2. Navigate to API Testing Tools(Postman)<br><br> 3. Set the Method to 'POST' <br><br> 4. Make a Request <br><br> or Use SWAGGER <br><br> <strong>Reset Token : {validToken}</strong></p>"
            };

            await _mailService.SendEmailAsync(mailContent);

            return new ApiResponse
            {
                IsSuccess = true,
                Result = "Reset password URL has been sent to the email successfully!"
            };
        }

        //Reset Password
        public async Task<ApiResponse> ResetPassword(ResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return new ApiResponse
                {
                    IsSuccess = false,
                    Result = "No user associated with email",
                };

            if (model.NewPassword != model.ConfirmPassword)
                return new ApiResponse
                {
                    IsSuccess = false,
                    Result = "Password doesn't match its confirmation",
                };

            var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            IdentityResult result = await _userManager.ResetPasswordAsync(user, normalToken, model.NewPassword);

            if (result.Succeeded)
                return new ApiResponse
                {
                    Result = "Password has been reset successfully!",
                    IsSuccess = true,
                };

            return new ApiResponse
            {
                Result = "Something went wrong",
                IsSuccess = false,
                Errors = (List<string>)result.Errors.Select(e => e.Description),
            };
        }

        //Token Genereator
        private async Task<string> GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);

            var userRoles = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToArray();

            var userClaims = await _userManager.GetClaimsAsync(user).ConfigureAwait(false);

            var roleClaims = await _userManager.GetClaimsAsync(user).ConfigureAwait(false);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)

            }.Union(userClaims).Union(roleClaims).Union(userRoles);

            //claims.AddRange(userRole.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

            var tokenClaims = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenClaims);
            return tokenString;
        }


        //Authenticate

        //private User Authenticate(AuthUser auth)
        //{


        //    if (string.IsNullOrEmpty(auth.UserName) || string.IsNullOrEmpty(auth.Email) || string.IsNullOrEmpty(auth.Password))
        //        return null;

        //    var user = _userManger.Users.SingleOrDefault(x => x.Email == auth.Email);

        //     check if username exists
        //    if (user == null)
        //        return null;
        //     check if password is correct
        //    if (!VerifyPasswordHash(auth.Password, user.PasswordHash, user.PasswordSalt))
        //        return null;

        //     authentication successful
        //    return null;
        //}



        //private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        //{
        //    if (password == null) throw new ArgumentNullException("password");
        //    if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
        //    if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
        //    if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

        //    using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
        //    {
        //        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //        for (int i = 0; i < computedHash.Length; i++)
        //        {
        //            if (computedHash[i] != storedHash[i]) return false;
        //        }
        //    }

        //    return true;
        //}


    }
}
