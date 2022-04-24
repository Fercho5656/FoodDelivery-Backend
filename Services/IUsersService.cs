using System.Threading.Tasks;
using FoodDelivery_Backend.Base;
using FoodDelivery_Backend.Models.Base;

namespace FoodDelivery_Backend.Services {
    public interface IUsersService : IEntityBaseRepository<User> {
        Task<User> GetByEmail(string email);
    }
}