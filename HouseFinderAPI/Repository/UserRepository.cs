using AutoMapper;
using HouseFinderAPI.Data;
using HouseFinderAPI.Models;
using HouseFinderAPI.Models.Dto;
using HouseFinderAPI.Repository.IRepository;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HouseFinderAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private string SecretKey;

        public UserRepository(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            this.context = context;
            this.mapper = mapper;
            SecretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }
        public bool IsUniqueUser(string username)
        {
            var user = context.Users.FirstOrDefault(x => x.UserName == username);
            if (user != null)
            {
                return false;
            }
            return true;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequest)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == loginRequest.UserName && x.Password == loginRequest.Password);
            if (user == null)
            {
                return new LoginResponseDto
                {
                    Token = "",
                    User = null
                };

            }
            //JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.Role,user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            LoginResponseDto loginResponseDto = new()
            {
                Token = tokenHandler.WriteToken(token),
                User = user,
            };
            return loginResponseDto;
        }

        public async Task<User> Registration(RegistrationRequestDto registrationRequest)
        {
            User user = mapper.Map<User>(registrationRequest);

            user.Email= registrationRequest.Email.ToLower();
            user.Role= registrationRequest.Role.ToLower();

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }
    }
}
