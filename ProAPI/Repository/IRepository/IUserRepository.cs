using RestAPI.Models.DTOs.UserDto;
using RestAPI.Models.Entity;

namespace RestAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<ICollection<AppUser>> GetUsers();
        AppUser GetUser(string id);
        Task<int> Check(int cursoId, string userId);

        bool IsUniqueUser(string userName);
        bool Apuntarse(int cursoId, string userId);
        Task<ICollection<CursoEntity>> GetAllMyCourseAsync(string userId);
        Task<ICollection<AppUser>> GetAllEstudents(int cursoId);
        Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto);
        Task<UserLoginResponseDto> Register(UserRegistrationDto userRegistrationDto);
        Task<bool> DeleteAsync(string id);

    }
}
