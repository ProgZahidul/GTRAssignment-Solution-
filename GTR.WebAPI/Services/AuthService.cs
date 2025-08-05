using GTR.WebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace GTR.WebAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthResponse> RegisterAsync(AuthRequest model)
        {
            if (model == null)
                throw new NullReferenceException("Register model is null");

            if (model.Password != model.ConfirmPassword)
                return new AuthResponse
                {
                    Message = "Confirm password doesn't match the password",
                    IsSuccess = false
                };

            var user = new ApplicationUser
            {
                Email = model.Username,
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return new AuthResponse
                {
                    Message = "User created successfully!",
                    IsSuccess = true
                };
            }

            return new AuthResponse
            {
                Message = "User did not create",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<AuthResponse> LoginAsync(AuthModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                return new AuthResponse
                {
                    Message = "There is no user with that Username",
                    IsSuccess = false
                };
            }

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
                return new AuthResponse
                {
                    Message = "Invalid password",
                    IsSuccess = false
                };

            var claims = new[]
            {
                new Claim("Email", model.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthResponse
            {
                Message = tokenAsString,
                IsSuccess = true,
                Expiration = token.ValidTo,
                Token = tokenAsString,
                Username = user.UserName,
                UserId = user.Id
            };
        }
    }

}
