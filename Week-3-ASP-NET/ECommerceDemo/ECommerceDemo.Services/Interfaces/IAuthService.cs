using ECommerceDemo.Models;

namespace ECommerceDemo.Services;

public interface IAuthService
{
    //This method, when implemented, will be what generates our JWT that our AuthController
    //returns to the front end (or swagger/postman) when somebody logs into our API.
    //That JWT will then need to be provided on all subsequent calls to any protected endpoints
    //as an auth header.
    Task<string> GenerateToken(ApplicationUser user);
}
