using HouseFinderAPI.Models.Dto;
using HouseFinderAPI.Models;
using HouseFinderAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HouseFinderAPI.Controllers
{
    [Route("api/UserAuth")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private APIResponse response;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.response = new APIResponse();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            if (!ModelState.IsValid) {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }
           
            var loginResponse = await userRepository.Login(model);
            if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMessage = new List<string> { "Username or Password is invalid" };
                return BadRequest(response);
            }
            response.IsSuccess = true;
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Result = loginResponse;
            return Ok(response);

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
               
                return BadRequest(response);
            }
            bool isExist = userRepository.IsUniqueUser(model.UserName);
            if (!isExist)
            {
                response.IsSuccess = false;
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.ErrorMessage = new List<string> { "Username already exist." };
                return BadRequest(response);

            }
            var user = await userRepository.Registration(model);
            if (user == null)
            {
                response.IsSuccess = false;
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.ErrorMessage.Add("Failed to register");
                return BadRequest(response);
            }
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Ok(response);

        }
    }
}
