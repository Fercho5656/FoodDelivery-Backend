using FoodDelivery_Backend.Base;
using FoodDelivery_Backend.Context;
using FoodDelivery_Backend.Models.Base;

namespace FoodDelivery_Backend.Services {
    public class RestaurantService : EntityBaseRepository<Restaurant>, IRestaurantService {
        public RestaurantService(AppDbContext dbContext) : base(dbContext) { }

    }
}