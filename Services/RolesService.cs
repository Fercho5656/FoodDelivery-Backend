using FoodDelivery_Backend.Base;
using FoodDelivery_Backend.Context;
using FoodDelivery_Backend.Models.Base;

namespace FoodDelivery_Backend.Services {
    public class RolesService : EntityBaseRepository<Role>, IRolesService {
        public RolesService(AppDbContext context) : base(context) { }
    }
}