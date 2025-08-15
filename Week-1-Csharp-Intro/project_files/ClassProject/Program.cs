using System.ComponentModel;
using System.Reflection.Metadata;
using System.Text.Json;
using application;
using models.car;

// See https://aka.ms/new-console-template for more information

List<string> options = ["1. Create Car", "2. List Cars", "3. Edit Car", "4. Delete Car", "5. Add Invoice", "6. Print Invoices", "7. Search by Year", "0. Exit"];
Console.WriteLine("Please choose from the following:");


List<Car> carList = new List<Car>();
Dictionary<Car, List<String>> invoices = new Dictionary<Car, List<string>>();

string path = Directory.GetCurrentDirectory() + @"/MyTextFile.txt";
if(File.Exists(path)) {
    
    using(StreamReader sr = File.OpenText(path)) {
        carList = JsonSerializer.Deserialize<List<Car>>(sr.ReadToEnd())!;
    }
}

while(true) {
    foreach(string s in options) {
        Console.WriteLine(s);
    }


    string selection = Console.ReadLine();  

    switch(selection) {
        case "1": {
            carList.Add(Application.addCar());
            break;
        }
        case "2": {
            Application.listCars(carList);
            break;
        }
        case "3": {
            Application.editCar(Application.findCar(carList));
            break;
        }
        case "4": {
            Application.deleteCar(Application.findCar(carList), carList);
            break;
        }
         case "5": {
            Car c = Application.findCar(carList);
            Application.addInvoice(c, invoices);         
            break;
        }
         case "6": {
            Application.printInvoices(invoices);
            break;
        }
         case "7": {
            Application.fetchByYear(carList);
            break;
         }
        case "0": {
          Application.saveCars(carList);  
            Environment.Exit(0);
            break;
        }
        default: {
            Console.WriteLine("Please only enter value 1-2, or 0");
            break;
        }
    }
}
