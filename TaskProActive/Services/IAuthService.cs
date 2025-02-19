using TaskProActive.DTO;

namespace TaskProActive.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterUserAsync(RegisterRequestDto userRegisterDto);
        Task<string> LoginAsync(LoginRequestDto userRegisterDto);
    }
}
