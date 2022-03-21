using System;

namespace FoodDelivery_Backend.Base {
    public interface IEntityBase {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}