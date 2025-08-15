using Microsoft.EntityFrameworkCore;
using models.car;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CarDb>(opt => opt.UseInMemoryDatabase("CarList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config => {
    config.DocumentName = "CarAPI";
    config.Title = "CarAPI";
    config.Version = "v1";
});
var app = builder.Build();
if(app.Environment.IsDevelopment()) {
    app.UseOpenApi();
    app.UseSwaggerUi(config => {
        config.DocumentTitle = "CarAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}


app.MapGet("/cars", async (CarDb db) =>
    await db.Cars.ToListAsync());

app.MapPost("/cars", async (Car c, CarDb db) => {
    db.Cars.Add(c);
    await db.SaveChangesAsync();
});

app.MapGet("/cars/year/{year}", async (CarDb db, string year) => {
    return await db.Cars.Where(c => c.year == year).ToListAsync();
});

app.Run();