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
    public class RestaurantController : ControllerBase {
        private readonly IRestaurantService _restaurantService;
        private readonly IMapper _mapper;
        public RestaurantController(IRestaurantService restaurantService, IMapper mapper) {
            _restaurantService = restaurantService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var restaurants = await _restaurantService.GetAll(restaurant => restaurant.CategoryRestaurant);
            var vmRestaurants = _mapper.Map<IEnumerable<VMRestaurant>>(restaurants);
            return Ok(vmRestaurants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            var restaurant = await _restaurantService.Get(id, restaurant => restaurant.CategoryRestaurant);
            var vmRestaurant = _mapper.Map<VMRestaurant>(restaurant);
            return Ok(vmRestaurant);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaveRestaurant saveRestaurant) {
            var restaurant = _mapper.Map<Restaurant>(saveRestaurant);
            await _restaurantService.Add(restaurant);
            var newRestaurant = await _restaurantService.Get(restaurant.Id, restaurant => restaurant.CategoryRestaurant);
            var newVmRestaurant = _mapper.Map<VMRestaurant>(newRestaurant);
            return CreatedAtAction(nameof(Get), new { id = newVmRestaurant.Id }, newVmRestaurant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SaveRestaurant saveRestaurant) {
            var restaurant = _mapper.Map<Restaurant>(saveRestaurant);
            if (restaurant == null) return NotFound();
            restaurant.Id = id;
            await _restaurantService.Update(id, restaurant);
            var updatedRestaurant = await _restaurantService.Get(restaurant.Id, restaurant => restaurant.CategoryRestaurant);
            var updatedVmRestaurant = _mapper.Map<VMRestaurant>(updatedRestaurant);
            return Ok(updatedVmRestaurant);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var restaurant = await _restaurantService.Get(id);
            if (restaurant == null) return NotFound();
            await _restaurantService.Delete(id);
            return NoContent();
        }
    }
}