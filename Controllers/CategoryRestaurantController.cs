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
    public class CategoryRestaurantController : ControllerBase {
        private readonly ICategoryRestaurantService _service;
        private readonly IMapper _mapper;
        public CategoryRestaurantController(ICategoryRestaurantService service, IMapper mapper) {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var result = await _service.GetAll();
            var vmCategoryRestaurants = _mapper.Map<IEnumerable<VMCategoryRestaurant>>(result);
            return Ok(vmCategoryRestaurants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            var result = await _service.Get(id);
            if (result == null) return NotFound();
            var vmCategoryRestaurant = _mapper.Map<VMCategoryRestaurant>(result);
            return Ok(vmCategoryRestaurant);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SaveCategoryRestaurant saveCategoryRestaurant) {
            var categoryRestaurant = _mapper.Map<CategoryRestaurant>(saveCategoryRestaurant);
            await _service.Add(categoryRestaurant);
            var newCategoryRestaurant = await _service.Get(categoryRestaurant.Id);
            var vmCategoryRestaurant = _mapper.Map<VMCategoryRestaurant>(newCategoryRestaurant);
            return CreatedAtAction(nameof(Add), new { id = vmCategoryRestaurant.Id }, vmCategoryRestaurant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SaveCategoryRestaurant saveCategoryRestaurant) {
            var categoryRestaurant = _mapper.Map<CategoryRestaurant>(saveCategoryRestaurant);
            categoryRestaurant.Id = id;
            var result = await _service.Update(id, categoryRestaurant);
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