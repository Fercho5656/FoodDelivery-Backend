using FoodDelivery_Backend.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery_Backend.Context {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions options) : base(options) {
        }

        public DbSet<CategoryFood> CategoryFoods { get; set; }
        public DbSet<CategoryRestaurant> CategoryRestaurants { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}