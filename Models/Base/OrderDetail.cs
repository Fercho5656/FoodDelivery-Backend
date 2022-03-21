using System.ComponentModel;
using System;
using FoodDelivery_Backend.Base;

namespace FoodDelivery_Backend.Models.Base {
    public class OrderDetail : IEntityBase {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties

        public int FoodId { get; set; }
        public Food Food { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}