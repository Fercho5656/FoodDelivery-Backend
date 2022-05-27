using FoodDelivery_Backend.Base;
using FoodDelivery_Backend.Context;
using FoodDelivery_Backend.Models.Base;

namespace FoodDelivery_Backend.Services {
    public class FoodService : EntityBaseRepository<Food>, IFoodService {
        public FoodService(AppDbContext context) : base(context) { }
    }
}