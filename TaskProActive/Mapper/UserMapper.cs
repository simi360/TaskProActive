using TaskProActive.Models;
using TaskProActive.DTO;

namespace TaskProActive.Mapper
{
    public static class UserMapper
    {
        public static UserDto ToDto(User user)
        {
            if (user == null) return new UserDto();

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Role = user.Role,
                CreatedOn = user.CreatedOn,
                CreatedBy = user.CreatedBy ?? 0,
                ModifiedOn = user.ModifiedOn ?? DateTime.MinValue,
                ModifiedBy = user.ModifiedBy ?? 0,
            };
        }

        public static User ToModel(UserDto dto)
        {
            if (dto == null) return null;

            return new User
            {
                Id = dto.Id,
                Name = dto.Name,
                Username = dto.Username,
                PasswordHash = string.Empty, 
                Role = dto.Role,
                CreatedOn = dto.CreatedOn,
                CreatedBy = dto.CreatedBy,
                ModifiedOn = dto.ModifiedOn,
                ModifiedBy = dto.ModifiedBy,
            };
        }
    }
}
