using RepoPattern.Models.Car;
using RepoPattern.Repositories.CarRepository;
using RepoPattern.Services.CarService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarService, CarService>();
var app = builder.Build();


// these are just using the repo directly 
app.MapPost("/cars", (Car c, ICarRepository ics) => {
    try
    {
        var createdCar = ics.AddCar(c);
        return Results.Created("Created Car.", createdCar);
    }
    catch(Exception e) 
    {
        return Results.Problem(e.Message, statusCode: 400);
    }
});

app.MapGet("/cars", (ICarRepository ics) => {
    try
    {
        return Results.Json(ics.GetCars());
    }
    catch(Exception e) 
    {
        return Results.Problem(e.Message, statusCode: 400);
    }
});

// this is using a service to further abstract the business logic layer from the data connection layer
app.MapPut("/cars/rent/{id}", (ICarService ics, String id) => {
    try
    {
        return Results.Ok(ics.RentCar(id));
    }
    catch(Exception e) 
    {
        return Results.Problem(e.Message, statusCode: 400);
    }
});

app.Run();