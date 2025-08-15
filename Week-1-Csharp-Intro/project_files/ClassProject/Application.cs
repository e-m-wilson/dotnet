using System.Data.SqlTypes;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.VisualBasic;
using models.car;

namespace application;

public class Application {

    public static Car addCar() {
        Console.WriteLine("Enter Make:");
        string make = Console.ReadLine()!;
        Console.WriteLine("Enter Model:");
        string model = Console.ReadLine()!;
        Console.WriteLine("Enter Year:");
        string year = Console.ReadLine()!;
        return new Car(make, model, year);
    }

    public static void listCars(List<Car> carList) {
        foreach(Car c in carList ) {
                Console.WriteLine(c.Id + "is a :" + c.make + c.model); 
            }
    }

    public static Car findCar(List<Car> cList) {
        Console.WriteLine("Please Enter Id:");
        string id = Console.ReadLine()!;
        Car c = cList.Find(myCar => myCar.Id == id)!;
        return c;
    }

    public static void editCar(Car c) {
        Console.WriteLine("Enter Make:");
        string make = Console.ReadLine()!;
        Console.WriteLine("Enter Model:");
        string model = Console.ReadLine()!;
        Console.WriteLine("Enter Year:");
        string year = Console.ReadLine()!;
        c.make = make;
        c.model = model;
        c.year = year;
    }

    public static void deleteCar(Car c, List<Car> cList) {
        cList.Remove(c);
    }

    public static void addInvoice(Car c, Dictionary<Car, List<String>> invoices) {
        Console.WriteLine("How much for today's invoice?");
        string amount = Console.ReadLine()!;
        if(!invoices.ContainsKey(c)) {
               invoices.Add(c, new List<String>{DateTime.Now.ToString("d") + ":" + amount}); 
            } else {
                invoices[c].Add(DateTime.Now.ToString("d") + ":" + amount);
            }
    }

    public static void printInvoices(Dictionary<Car, List<String>> invoices) {

        foreach(Car c in invoices.Keys) {

            Console.WriteLine("Details for Car: " + c.Id);
            foreach(string s in invoices[c]) {
                Console.WriteLine(s);
            }
        }
    }

    public async static void saveCars(List<Car> cList) {
        string path = Directory.GetCurrentDirectory() + @"/MyTextFile.txt";

     
        using(StreamWriter sw = File.CreateText(path))
        {
            await sw.WriteLineAsync(JsonSerializer.Serialize(cList));
        }
        
    }

    public static void fetchByYear(List<Car> cList) {
        Console.WriteLine("Please enter a year to search:");
        string year = Console.ReadLine()!;
        List<Car> carList = cList.Where(c => c.year == year).ToList();
        foreach(Car c in carList) {
            Console.WriteLine("Car with Id is a match: " + c.Id);
        }

    }
}