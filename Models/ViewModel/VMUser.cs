using System;

namespace FoodDelivery_Backend.Models.ViewModel {
    public class VMUser {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public VMRole Role { get; set; }

    }
}