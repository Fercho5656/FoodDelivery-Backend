using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoodDelivery_Backend.Models.Base;
using FoodDelivery_Backend.Models.Save;
using FoodDelivery_Backend.Models.ViewModel;
using FoodDelivery_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery_Backend.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase {
        private readonly IRolesService _service;
        private readonly IMapper _mapper;
        public RolesController(IRolesService service, IMapper mapper) {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var roles = await _service.GetAll();
            var vmRoles = _mapper.Map<IEnumerable<VMRole>>(roles);
            return Ok(vmRoles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            var role = await _service.Get(id);
            if (role == null) return NotFound();
            var vmRole = _mapper.Map<VMRole>(role);
            return Ok(vmRole);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SaveRole saveRole) {
            var role = _mapper.Map<Role>(saveRole);
            await _service.Add(role);
            var vmRole = _mapper.Map<VMRole>(role);
            return CreatedAtAction(nameof(Add), new { id = vmRole.Id }, vmRole);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SaveRole saveRole) {
            var newRole = _mapper.Map<Role>(saveRole);
            newRole.Id = id;
            var result = await _service.Update(id, newRole);
            if (result == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var role = await _service.Get(id);
            if (role == null) return NotFound();
            await _service.Delete(id);
            return NoContent();
        }
    }
}