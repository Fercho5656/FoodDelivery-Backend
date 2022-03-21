using AutoMapper;
using FoodDelivery_Backend.Models.Base;
using FoodDelivery_Backend.Models.Save;

namespace FoodDelivery_Backend.Profiles {
    public class ResourceToModel : Profile {
        public ResourceToModel() {
            CreateMap<SaveRole, Role>();
            CreateMap<SaveUser, User>();
        }
    }
}