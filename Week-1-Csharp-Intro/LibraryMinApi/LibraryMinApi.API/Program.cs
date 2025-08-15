//Up here, like any .cs file, we can throw in our using statements for packages or namespaces
// that we may need
using Library.DTOs;
using Library.Models;
using Library.Repositories;
using Library.Services;
using Serilog;

//Creating a logger in our program.cs
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

// Here is our builder
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

//Adding Serilog for logging
builder.Services.AddSerilog();

//==== Dependency Injection Area ====

//Repos
builder.Services.AddSingleton<IMemberRepository, JsonMemberRepository>();
builder.Services.AddSingleton<IBookRepository, JsonBookRepository>();
builder.Services.AddSingleton<ICheckoutRepository, JsonCheckoutRepository>();

//Services
builder.Services.AddSingleton<ICheckoutService, CheckoutService>();

//Adding swagger to my dependencies
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "LibraryAPI";
    config.Title = "LibraryAPI";
    config.Version = "v1";
});

// Here the builder takes all of our DI and middleware stuff and creates our app.
var app = builder.Build();

//Instructing the app to use request logging with Serilog
app.UseSerilogRequestLogging();

//Telling the app to use swagger, pulling it from the DI container in ASP.NET
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "LibraryAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

//Book endpoints
app.MapGet(
    "/books",
    (IBookRepository repo) =>
    {
        try
        {
            return Results.Ok(repo.GetAllBooks());
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
);

app.MapPost(
    "/books",
    (Book book, IBookRepository repo) =>
    {
        try
        {
            var createdBook = repo.AddBook(book);
            return Results.Created($"/books/{createdBook.Isbn}", createdBook);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 400);
        }
    }
);

//Member endpoints
app.MapGet(
    "/members",
    (IMemberRepository repo) =>
    {
        try
        {
            return Results.Ok(repo.GetAllMembers());
        }
        catch (Exception ex)
        {
            // Using the Results prebuilt object to return the appropriate code
            // if anything goes wrong
            return Results.Problem(ex.Message);
        }
    }
);

app.MapPost(
    "/members",
    (Member memberToAdd, IMemberRepository repo) =>
    {
        try
        {
            return Results.Ok(repo.AddMember(memberToAdd));
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 400); //We can return specific codes with Results.Problem if we want to
        }
    }
);

//Checkout endpoints

app.MapPost(
    "/checkouts",
    (CheckoutRequestDTO checkoutRequest, ICheckoutService service) =>
    {
        try
        {
            //Some service layer method call goes here
            Checkout checkout = service.CheckoutBook(checkoutRequest);
            return Results.Created($"/checkouts/{checkout.CheckoutId}", checkout);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
);

app.MapPut(
    "/checkouts/return/{isbn}",
    (string isbn, ICheckoutService service) =>
    {
        try
        {
            //Here we will call our service layer method
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
);

// Finally, this is what runs our app.
app.Run();
