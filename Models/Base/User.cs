using System;
using System.Collections.Generic;
using FoodDelivery_Backend.Base;

namespace FoodDelivery_Backend.Models.Base {
    public class User : IEntityBase {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Birthday { get; set; }

        // Navigation properties
        public Role Role { get; set; }
        public int RoleId { get; set; }

        public List<Order> Orders { get; set; }

    }
}