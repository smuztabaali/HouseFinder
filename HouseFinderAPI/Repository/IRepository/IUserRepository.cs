using HouseFinderAPI.Models;
using HouseFinderAPI.Models.Dto;

namespace HouseFinderAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequest);
        Task<User> Registration(RegistrationRequestDto registrationRequest);
    }
}
