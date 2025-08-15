namespace models.car;

public class Car {

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string make { get; set; }
    public string model { get; set; }
    public string year { get; set; }

    
    
}