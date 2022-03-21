using System;
using FoodDelivery_Backend.Base;

namespace FoodDelivery_Backend.Models.Base {
    public class Food : IEntityBase {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int PreparationMinutes { get; set; }

        // Navigation properties
        public int CategoryFoodId { get; set; }
        public CategoryFood CategoryFood { get; set; }
        public OrderDetail OrderDetail { get; set; }

    }
}