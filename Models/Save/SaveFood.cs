using System;

namespace FoodDelivery_Backend.Models.Save {
    public class SaveFood {
        public string Name { get; set; }
        public float Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int Stars { get; set; }
        public int PreparationMinutes { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CategoryFoodId { get; set; }
    }
}