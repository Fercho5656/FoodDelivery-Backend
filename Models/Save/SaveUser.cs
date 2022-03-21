using System;

namespace FoodDelivery_Backend.Models.Save {
    public class SaveUser {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
    }
}