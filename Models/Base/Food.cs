using System;
using System.Collections.Generic;
using FoodDelivery_Backend.Base;

namespace FoodDelivery_Backend.Models.Base {
    public class Food : IEntityBase {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int Stars { get; set; }
        public int PreparationMinutes { get; set; }

        // Navigation properties
        public int CategoryFoodId { get; set; }
        public CategoryFood CategoryFood { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }

    }
}