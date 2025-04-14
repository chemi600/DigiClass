using RestAPI.Models.Entity;

namespace RestAPI.Models.DTOs.UserDto
{
    public class UserLoginResponseDto
    {
        public AppUser User { get; set; }
        public string Token { get; set; }

        public string Name { get; set; }
        public string Role { get; set; }

    }
}
