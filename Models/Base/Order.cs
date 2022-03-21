using System;
using System.ComponentModel;
using FoodDelivery_Backend.Base;

namespace FoodDelivery_Backend.Models.Base {
    public class Order : IEntityBase {
        public int Id { get; set; }
        public float TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public User User { get; set; }
        public int UserId { get; set; }

        public Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }

        public OrderDetail OrderDetail { get; set; }

    }
}