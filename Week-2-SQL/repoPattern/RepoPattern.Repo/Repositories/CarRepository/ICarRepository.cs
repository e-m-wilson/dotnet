namespace RepoPattern.Repositories.CarRepository;
using RepoPattern.Models.Car;

public interface ICarRepository
{

    public Task AddCar(Car c);
    public List<Car> GetCars();

    public Car GetCarById(String Id);

    public Task UpdateCar(Car c);

}
