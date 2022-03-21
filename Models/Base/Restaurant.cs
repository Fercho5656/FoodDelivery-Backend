using System;
using System.Collections.Generic;
using FoodDelivery_Backend.Base;

namespace FoodDelivery_Backend.Models.Base {
    public class Restaurant : IEntityBase {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public CategoryRestaurant CategoryRestaurant { get; set; }
        public int CategoryRestaurantId { get; set; }

        public List<Order> Orders { get; set; }
        
        

    }
}