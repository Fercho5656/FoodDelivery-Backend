using FoodDelivery_Backend.Base;
using FoodDelivery_Backend.Context;
using FoodDelivery_Backend.Models.Base;

namespace FoodDelivery_Backend.Services {
    public class CategoryFoodService : EntityBaseRepository<CategoryFood>, ICategoryFoodService {
        public CategoryFoodService(AppDbContext context) : base(context) { }

    }
}