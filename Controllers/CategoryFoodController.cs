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
    public class CategoryFoodController : ControllerBase {
        private readonly ICategoryFoodService _service;
        private readonly IMapper _mapper;
        public CategoryFoodController(ICategoryFoodService service, IMapper mapper) {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var categoryFoods = await _service.GetAll();
            var vmCategoryFoods = _mapper.Map<IEnumerable<VMCategoryFood>>(categoryFoods);
            return Ok(vmCategoryFoods);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            var categoryFood = await _service.Get(id);
            if (categoryFood == null) return NotFound();
            var vmCategoryFood = _mapper.Map<VMCategoryFood>(categoryFood);
            return Ok(vmCategoryFood);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SaveCategoryFood saveCategoryFood) {
            var categoryFood = _mapper.Map<CategoryFood>(saveCategoryFood);
            await _service.Add(categoryFood);
            return CreatedAtAction(nameof(Add), new { id = categoryFood.Id }, categoryFood);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SaveCategoryFood saveCategoryFood) {
            var categoryFood = _mapper.Map<CategoryFood>(saveCategoryFood);
            categoryFood.Id = id;
            var result = await _service.Update(id, categoryFood);
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