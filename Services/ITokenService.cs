using FoodDelivery_Backend.Models.ViewModel;

namespace FoodDelivery_Backend.Services {
    public interface ITokenService {
        string BuildToken(string key, string issuer, VMUser user);
        bool IsValidToken(string key, string issuer, string audience, string token);
    }
}