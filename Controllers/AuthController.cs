using System.Threading.Tasks;
using AutoMapper;
using FoodDelivery_Backend.Models.Auth;
using FoodDelivery_Backend.Models.ViewModel;
using FoodDelivery_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FoodDelivery_Backend.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase {
        private readonly IUsersService _service;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        public AuthController(IUsersService service, IMapper mapper, IConfiguration config, ITokenService tokenService) {
            _service = service;
            _mapper = mapper;
            _config = config;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel) {
            var user = await _service.GetByEmail(loginModel.Email);
            if (user == null) return Unauthorized();
            if (!BCrypt.Net.BCrypt.Verify(loginModel.Password, user.Password)) return Unauthorized();
            var vmUser = _mapper.Map<VMUser>(user);
            var token = _tokenService.BuildToken(_config["JWT:SecretKey"].ToString(), _config["JWT:ValidIssuer"].ToString(), vmUser);
            return Ok(new { token });
        }

    }
}