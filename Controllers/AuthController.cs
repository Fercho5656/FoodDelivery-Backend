using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FoodDelivery_Backend.Models.Auth;
using FoodDelivery_Backend.Models.Base;
using FoodDelivery_Backend.Models.Save;
using FoodDelivery_Backend.Models.ViewModel;
using FoodDelivery_Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
            if (user == null) return Unauthorized(new { message = "Invalid email or password" });
            if (!BCrypt.Net.BCrypt.Verify(loginModel.Password, user.Password)) return Unauthorized(new { message = "Invalid email or password" });
            var vmUser = _mapper.Map<VMUser>(user);
            var token = _tokenService.BuildToken(_config["JWT:SecretKey"].ToString(), _config["JWT:ValidIssuer"].ToString(), vmUser);
            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] SaveUser saveUser) {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(saveUser.Password);
            var user = _mapper.Map<User>(saveUser);
            user.Password = hashedPassword;
            user.RoleId = 1;
            await _service.Add(user);
            var vmUser = _mapper.Map<VMUser>(user);
            return CreatedAtAction(nameof(Register), new { id = vmUser.Id }, vmUser);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("check")]
        public async Task<IActionResult> Check() {
            var value = new { message = "Authorized" };
            return Ok(value);
        }
    }
}