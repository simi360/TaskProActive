using TaskProActive.Data;
using TaskProActive.DTO;
using TaskProActive.Mapper;
using TaskProActive.Models;
using TaskProActive.Repositories;

namespace TaskProActive.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext _context;

        public UserService(IUserRepository userRepository, AppDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public async Task<bool> RegisterUserByAdminAsync(RegisterRequestDto request, int adminUserId)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(request.Username);
            if (existingUser != null)
                return false;

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Username = request.Username,
                PasswordHash = passwordHash,
                Name = request.Name,
                CreatedBy = adminUserId,
                ModifiedBy = adminUserId,
            };

            await _userRepository.AddUserAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return UserMapper.ToDto(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users?.Select(u => UserMapper.ToDto(u)) ?? new List<UserDto>();
        }

        public async Task<bool> UpdateUserAsync(int id, UserDto updatedUser, int modifiedBy)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return false;

            user.Name = updatedUser.Name ?? user.Name;
            user.Username = updatedUser.Username ?? user.Username;
            user.Role = updatedUser.Role ?? user.Role;

            user.ModifiedOn = DateTime.UtcNow;
            user.ModifiedBy = modifiedBy;

            _userRepository.UpdateUser(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return false;

            _userRepository.DeleteUser(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
