using Microsoft.EntityFrameworkCore;
using SchoolDemo.Data;
using SchoolDemo.Models;
using SchoolDemo.Repositories;
using SchoolDemo.Services;

var builder = WebApplication.CreateBuilder(args);

//Loading the string from my env file - HINT: There are other ways to do this
//Things like Secrets, AppSettings (dont forget to edit your gitignore for this one)
// and packages like DotNetEnv to load from envs more easily

string conn_string = File.ReadAllText("../conn_string.env");

//Adding swagger to my dependencies
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "SchoolDemo";
    config.Title = "School API";
    config.Version = "v0.1";
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Adding Our own services to the builder - starting with DbContext
builder.Services.AddDbContext<SchoolDbContext>(options => options.UseSqlServer(conn_string));

// Register Repositories
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();

// Register Services
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IInstructorService, InstructorService>();

var app = builder.Build();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

//Endpoints

// --- Student Endpoints ---
app.MapGet("/students", async (IStudentService service) => Results.Ok(await service.GetAllAsync()));

app.MapGet(
    "/students/{id}",
    async (int id, IStudentService service) =>
    {
        var student = await service.GetByIdAsync(id);
        return student is not null ? Results.Ok(student) : Results.NotFound();
    }
);

app.MapPost(
    "/students",
    async (Student student, IStudentService service) =>
    {
        await service.CreateAsync(student);
        return Results.Created($"/students/{student.Id}", student);
    }
);

// --- Course Endpoints ---
app.MapGet("/courses", async (ICourseService service) => Results.Ok(await service.GetAllAsync()));

app.MapGet(
    "/courses/{id}",
    async (int id, ICourseService service) =>
    {
        var course = await service.GetByIdAsync(id);
        return course is not null ? Results.Ok(course) : Results.NotFound();
    }
);

app.MapPost(
    "/courses",
    async (Course course, ICourseService service) =>
    {
        await service.CreateAsync(course);
        return Results.Created($"/courses/{course.Id}", course);
    }
);

// --- Instructor Endpoints ---
app.MapGet(
    "/instructors",
    async (IInstructorService service) => Results.Ok(await service.GetAllAsync())
);

app.MapGet(
    "/instructors/{id}",
    async (int id, IInstructorService service) =>
    {
        var instructor = await service.GetByIdAsync(id);
        return instructor is not null ? Results.Ok(instructor) : Results.NotFound();
    }
);

app.MapPost(
    "/instructors",
    async (Instructor instructor, IInstructorService service) =>
    {
        await service.CreateAsync(instructor);
        return Results.Created($"/instructors/{instructor.Id}", instructor);
    }
);

//Running my app
app.Run();
