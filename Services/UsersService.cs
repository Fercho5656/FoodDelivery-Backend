using System.Threading.Tasks;
using FoodDelivery_Backend.Base;
using FoodDelivery_Backend.Context;
using FoodDelivery_Backend.Models.Base;
using Microsoft.EntityFrameworkCore;
namespace FoodDelivery_Backend.Services {
    public class UsersService : EntityBaseRepository<User>, IUsersService {
        private readonly AppDbContext _context;
        public UsersService(AppDbContext context) : base(context) {
            _context = context;
        }

        public async Task<User> GetByEmail(string email) {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;
            return await Get(user.Id, u => u.Role);
        }
    }
}