namespace FoodDelivery_Backend.Models.ViewModel {
    public class VMRestaurant {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public VMCategoryRestaurant CategoryRestaurant { get; set; }
    }
}