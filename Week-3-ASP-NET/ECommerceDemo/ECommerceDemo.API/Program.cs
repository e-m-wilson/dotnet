using System.Text;
using ECommerceDemo.API;
using ECommerceDemo.Data;
using ECommerceDemo.Models;
using ECommerceDemo.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//Adding Identity to the builder
//Here inside of this AddIdentityCore method call, we will set password rules/requirements
builder
    .Services.AddIdentityCore<ApplicationUser>(options =>
    {
        //This is where my password rules go
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager<SignInManager<ApplicationUser>>();

// Add JWT Authentication
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]));

builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

//After we configure all of our needed Authorization settings/services above, we want to AddAuthorization()
//So that further down Program.cs the default UseAuthorization() middleware call actually does something.
builder.Services.AddAuthorization();

//Loading the string from my env file - HINT: There are other ways to do this
//Things like Secrets, AppSettings (dont forget to edit your gitignore for this one)
// and packages like DotNetEnv to load from envs more easily
string conn_string = File.ReadAllText("../conn_string.env");

// Add services to the container.
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAuthService, AuthService>(); // Remember to add the authservice

//Adding an IMemoryCache to the builder
builder.Services.AddMemoryCache();

//Adding CORS to the builder, with a simple config
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "LocalHostPolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:5174").AllowAnyHeader().AllowAnyMethod();
        }
    );
});

//Adding swagger support
builder.Services.AddEndpointsApiExplorer();

//Modifying this AddSwaggerGen() call to allow us to test/debug our Auth scheme setup in swagger
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer"
        }
    );
    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        }
    );
});

// Register EF Core with SQL Server connection string
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(conn_string));

//To add a filter, we modify the AddControllers() method call
//and ad it to it's options
builder.Services.AddControllers(options => options.Filters.Add<TimingFilter>());

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();

//Adding my new custom middleware
app.UseMiddleware<RequestTimingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger(); //Enables Swagger UI for API documentation
    app.UseSwaggerUI();
}


app.UseHttpsRedirection(); //Redirects HTTP requests to HTTPS

app.UseCors("LocalHostPolicy");

app.UseResponseCaching();

app.UseAuthorization(); //Uses Authorization middleware

//Below UseAuthorization, Im going to call my RolesInitializer's method
//and seed my user roles. After this runs once, I will comment it out to avoid any issues.

// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;

//     try
//     {
//         await RolesInitalizer.SeedRoles(services);
//     }
//     catch (Exception ex)
//     {
//         var logger = services.GetRequiredService<ILogger<Program>>();
//         logger.LogError(ex, "Error seeding roles");
//     }
// }

app.MapControllers(); //Maps controller endpoints

app.Run(); //Runs the application
