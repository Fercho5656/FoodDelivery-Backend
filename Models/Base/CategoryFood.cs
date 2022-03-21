using System;
using System.Collections.Generic;
using FoodDelivery_Backend.Base;

namespace FoodDelivery_Backend.Models.Base {
    public class CategoryFood : IEntityBase {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }

        // Navigation properties
        public List<Food> Foods { get; set; }
    }
}