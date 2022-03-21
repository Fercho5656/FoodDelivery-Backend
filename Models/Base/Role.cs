using System;
using System.Collections.Generic;
using FoodDelivery_Backend.Base;

namespace FoodDelivery_Backend.Models.Base {
    public class Role : IEntityBase {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public List<User> Users { get; set; }
    }
}