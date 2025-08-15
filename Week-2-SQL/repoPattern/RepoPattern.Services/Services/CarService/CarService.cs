using RepoPattern.Models.Car;
using RepoPattern.Repositories.CarRepository;

namespace RepoPattern.Services.CarService;

public class CarService : ICarService
{

    private readonly ICarRepository _carRepo;

    public CarService(ICarRepository carRepo)
    {
        _carRepo = carRepo;
    }

    public Task RentCar(String id) 
    {
        Car carForRent = _carRepo.GetCarById(id);
        
        if(!carForRent.IsOpenForRental)
            throw new Exception("Car not available for rental!");

        carForRent.IsOpenForRental = false;
        return _carRepo.UpdateCar(carForRent);
    }
}