using FoodDelivery_Backend.Base;
using FoodDelivery_Backend.Context;
using FoodDelivery_Backend.Models.Base;

namespace FoodDelivery_Backend.Services {
    public class UsersService : EntityBaseRepository<User>, IUsersService {
        public UsersService(AppDbContext context) : base(context) { }
    }
}