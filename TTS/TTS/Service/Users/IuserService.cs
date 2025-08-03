using TTS.Dto;
using TTS.Models.User;

namespace TTS.Service.Users
{
    public interface IuserService
    {
        Task<List<User>> GetAllAsync();
        Task<User> CreateUserAsync(Userdto userDto); 
        Task<bool> IsUsernameExistsAsync(string userName);
        Task<LoginResponseDto> LoginAsync(string userName, string password);



    }
}
