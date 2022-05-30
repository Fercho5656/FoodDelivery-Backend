using FoodDelivery_Backend.Base;
using FoodDelivery_Backend.Context;
using FoodDelivery_Backend.Models.Base;

namespace FoodDelivery_Backend.Services {
    public class CategoryRestaurantService : EntityBaseRepository<CategoryRestaurant>, ICategoryRestaurantService {
        public CategoryRestaurantService(AppDbContext dbContext) : base(dbContext) { }

    }
}