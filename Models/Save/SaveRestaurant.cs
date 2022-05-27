namespace FoodDelivery_Backend.Models.Save {
    public class SaveRestaurant {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int CategoryRestaurantId { get; set; }
    }
}