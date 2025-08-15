var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Adding swagger to my dependencies
builder.Services.AddEndpointsApiExplorer();

//Adding CORS to the builder, with a simple config
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AzurePolicy",
        policy =>
        {
            policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
        }
    );
});

builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "LibraryAPI";
    config.Title = "LibraryAPI";
    config.Version = "v1";
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapOpenApi();
app.UseOpenApi();

app.UseSwaggerUi(config =>
{
    config.DocumentTitle = "LibraryAPI";
    config.Path = "/swagger";
    config.DocumentPath = "/swagger/{documentName}/swagger.json";
    config.DocExpansion = "list";
});

app.UseCors("AzurePolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
