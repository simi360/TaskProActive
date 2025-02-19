using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TaskProActive.Data;
using TaskProActive.Models;
using TaskProActive.Repositories;
using TaskProActive.DTO;

namespace TaskProActive.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, AppDbContext context, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> RegisterUserAsync(RegisterRequestDto request)
        {
            // Check if the user already exists
            var existingUser = await _userRepository.GetByUsernameAsync(request.Username);
            if (existingUser != null) return false;

            // Hash the password
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User { Username = request.Username, PasswordHash = passwordHash, Name = request.Name };
            await _userRepository.AddUserAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string> LoginAsync(LoginRequestDto req)
        {
            var user = await _userRepository.GetByUsernameAsync(req.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
            {
                return null;
            }

            // Generate JWT token using settings from configuration
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var secretKey = _configuration["Jwt:SecretKey"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer,
                audience,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
