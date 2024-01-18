using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using TaskForge.Models;
using Task = TaskForge.Models.Task;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<TaskForgeDbContext>(builder.Configuration["TaskForgeDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3000",
                                "http://localhost:7273")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowAnyOrigin();
        });
});

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Register the user
app.MapPost("/register", (TaskForgeDbContext db, User user) =>
{
    db.Users.Add(user);
    db.SaveChanges();
    return Results.Created($"/user/user.Id", user);
});

// Check if the user is in the database
app.MapGet("/checkuser/{uid}", (TaskForgeDbContext db, string uid) =>
{
    var user = db.Users.Where(x => x.FirebaseUid == uid).ToList();
    if (uid == null)
    {
        return Results.NotFound();
    }
    else
    {
        return Results.Ok(user);
    }
});

// Get all users
app.MapGet("/user", (TaskForgeDbContext db) =>
{
    return db.Users.ToList();
});

// Get users by ID
app.MapGet("/user/{id}", (TaskForgeDbContext db, int id) =>
{
    var user = db.Users.Where(x => x.Id == id);
    return user;
});

// Get all tasks
app.MapGet("/task", (TaskForgeDbContext db) =>
{
    return db.Tasks.ToList();
});

//Get a single task
app.MapGet("/task/{id}", (TaskForgeDbContext db, int id) =>
{
    var tasks = db.Tasks.SingleOrDefault(x => x.Id == id);
    return tasks;
});

// Create a task
app.MapPost("/task", (TaskForgeDbContext db, Task task) =>
{
    db.Tasks.Add(task);
    db.SaveChanges();
    return Results.Created($"/tasks/{task.Id}", task);
});

// Delete a task
app.MapDelete("/task/{id}", (TaskForgeDbContext db, int id) =>
{
    Task task = db.Tasks.SingleOrDefault(task => task.Id == id);
    if (task == null)
    {
        return Results.NotFound();
    }
    db.Tasks.Remove(task);
    db.SaveChanges();
    return Results.NoContent();
});

//Update a task
app.MapPut("/task/{id}", (TaskForgeDbContext db, int id, Task task) =>
{
    Task taskToUpdate = db.Tasks.SingleOrDefault(t => t.Id == id);
    if (taskToUpdate == null)
    {
        return Results.NotFound();
    }
    taskToUpdate.Status = task.Status;
    taskToUpdate.Title = task.Title;
    taskToUpdate.Description = task.Description;
    taskToUpdate.DueDate = task.DueDate;
    taskToUpdate.Status = task.Status;
    db.SaveChanges();
    return Results.NoContent();
});

app.Run();
