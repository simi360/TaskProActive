namespace TaskProActive.DTO
{
    public class TokenDto
    {
        public string Token { get; set; }
        public UserDto User { get; set; } = new UserDto();
    }
}
