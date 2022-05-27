using AutoMapper;
using FoodDelivery_Backend.Models.Base;
using FoodDelivery_Backend.Models.ViewModel;

namespace FoodDelivery_Backend.Profiles {
    public class ModelToResource : Profile {
        public ModelToResource() {
            CreateMap<Role, VMRole>();
            CreateMap<User, VMUser>();
            CreateMap<Food, VMFood>();
            CreateMap<CategoryFood, VMCategoryFood>();
            CreateMap<Restaurant, VMRestaurant>();
            CreateMap<CategoryRestaurant, VMCategoryRestaurant>();
        }
    }
}