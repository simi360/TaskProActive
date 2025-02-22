using System.Collections.Generic;
using System.Threading.Tasks;
using TaskProActive.DTO;

namespace TaskProActive.Services
{
    public interface IUserService
    {
        Task<bool> RegisterUserByAdminAsync(RegisterRequestDto request, int adminUserId);
        Task<UserDto> GetUserByIdAsync(int id);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<bool> UpdateUserAsync(int id, UserDto updatedUser, int modifiedBy);
        Task<bool> DeleteUserAsync(int id);
    }
}
