using System.Text.Json;
using Library.Models;

namespace Library.Repositories;

public class JsonCheckoutRepository : ICheckoutRepository
{
    private readonly string _filePath;

    public JsonCheckoutRepository()
    {
        _filePath = "./5-Data-Files/checkouts.json";
    }

    // Adding a checkout record to our Json
    public void AddCheckout(List<Checkout> updatedCheckouts)
    {
        // Adding the checkout to the JSON
        SaveCheckouts(updatedCheckouts);
    }

    //Get the checkout list from the json
    public List<Checkout> GetAllCheckouts()
    {
        try
        {
            if (!File.Exists(_filePath))
                return new List<Checkout>();

            using var stream = File.OpenRead(_filePath);

            return JsonSerializer.Deserialize<List<Checkout>>(stream) ?? new List<Checkout>();
        }
        catch
        {
            throw new Exception("Failed to retrieve checkouts");
        }
    }

    //Save the checkout list to the Json
    public void SaveCheckouts(List<Checkout> checkoutList)
    {
        using var stream = File.Create(_filePath); //Creating the file
        JsonSerializer.Serialize(stream, checkoutList); //Our file will hold a list of Books, serialized to Json
    }
}
