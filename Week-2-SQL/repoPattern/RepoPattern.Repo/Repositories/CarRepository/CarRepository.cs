using System.Text.Json;
using RepoPattern.Models.Car;

namespace RepoPattern.Repositories.CarRepository;

public class CarRepository : ICarRepository
{
    string path = Directory.GetCurrentDirectory() + @"/cars.json";
    public async Task AddCar(Car c) 
    {

        if(File.Exists(path)) 
        {
            List<Car> carList = new List<Car>();
            using(StreamReader sr = File.OpenText(path)) 
            {
                carList = JsonSerializer.Deserialize<List<Car>>(sr.ReadToEnd())!;
            }

            using(StreamWriter sw = File.CreateText(path))
            {
                carList.Add(c);
                await sw.WriteLineAsync(JsonSerializer.Serialize(carList));
            }
        } 
        else 
        {
             using(StreamWriter sw = File.AppendText(path))
            {
                await sw.WriteLineAsync("[" + JsonSerializer.Serialize(c) + "]");
            }
        }
        
    }

    public List<Car> GetCars() {
        
        List<Car> carList = new List<Car>();
        if(File.Exists(path)) {
            
            using(StreamReader sr = File.OpenText(path)) {
                carList = JsonSerializer.Deserialize<List<Car>>(sr.ReadToEnd())!;
            }
        }
        return carList;
    }

    public Car GetCarById(String id) 
    {
        return GetCars().Find(c => c.Id == id)!;
    }

    public async Task UpdateCar(Car updatedCar) 
    {

        List<Car> cList = GetCars();
        Car c = cList.Find(c => c.Id == updatedCar.Id)!;
        cList.Remove(c);
        cList.Add(updatedCar);
        
        using(StreamWriter sw = File.CreateText(path))
            {
                await sw.WriteLineAsync(JsonSerializer.Serialize(cList));
            }
    }
}