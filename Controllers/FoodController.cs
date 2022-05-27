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
    public class FoodController : ControllerBase {
        private readonly IFoodService _service;
        private readonly IMapper _mapper;
        public FoodController(IFoodService service, IMapper mapper) {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var result = await _service.GetAll(food => food.CategoryFood);
            var vmFoods = _mapper.Map<IEnumerable<VMFood>>(result);
            return Ok(vmFoods);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            var result = await _service.Get(id, food => food.CategoryFood);
            if (result == null) return NotFound();
            var vmFood = _mapper.Map<VMFood>(result);
            return Ok(vmFood);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SaveFood saveFood) {
            var food = _mapper.Map<Food>(saveFood);
            await _service.Add(food);
            return CreatedAtAction(nameof(Add), new { id = food.Id }, food);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SaveFood saveFood) {
            var food = _mapper.Map<Food>(saveFood);
            food.Id = id;
            var result = await _service.Update(id, food);
            if (result == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var result = await _service.Get(id);
            if (result == null) return NotFound();
            await _service.Delete(id);
            return NoContent();
        }
    }
}