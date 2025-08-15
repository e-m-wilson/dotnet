using RepoPattern.Models.Car;

namespace RepoPattern.Services.CarService;

public interface ICarService 
{

    public Task RentCar(String id);
}