using System.Collections.Generic;
using System.Threading.Tasks;
using TaskProActive.Models;

namespace TaskProActive.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddUserAsync(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
