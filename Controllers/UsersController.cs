using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoodDelivery_Backend.Models.Base;
using FoodDelivery_Backend.Models.Save;
using FoodDelivery_Backend.Models.ViewModel;
using FoodDelivery_Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery_Backend.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase {
        private readonly IUsersService _service;
        private readonly IMapper _mapper;
        public UsersController(IUsersService service, IMapper mapper) {
            _service = service;
            _mapper = mapper;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var users = await _service.GetAll(u => u.Role);
            var vmUsers = _mapper.Map<IEnumerable<VMUser>>(users);
            return Ok(vmUsers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            var user = await _service.Get(id, u => u.Role);
            if (user == null) return NotFound();
            var vmUser = _mapper.Map<VMUser>(user);
            return Ok(vmUser);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SaveUser saveUser) {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(saveUser.Password);
            var user = _mapper.Map<User>(saveUser);
            user.Password = hashedPassword;
            user.RoleId = 1;
            await _service.Add(user);
            var vmUser = _mapper.Map<VMUser>(user);
            return CreatedAtAction(nameof(Add), new { id = vmUser.Id }, vmUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SaveUser saveUser) {
            var newUser = _mapper.Map<User>(saveUser);
            var newHashedPassword = BCrypt.Net.BCrypt.HashPassword(saveUser.Password);
            newUser.Password = newHashedPassword;
            newUser.Id = id;
            var result = await _service.Update(id, newUser);
            if (result == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var user = await _service.Get(id);
            if (user == null) return NotFound();
            await _service.Delete(id);
            return NoContent();
        }
    }
}