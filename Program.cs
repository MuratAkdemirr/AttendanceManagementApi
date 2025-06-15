using AttendanceManagementApi.Data;
using AttendanceManagementApi.Models.Entities;
using FirstWebApp;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddOpenApi();

var app = builder.Build();
app.MapOpenApi();
app.MapScalarApiReference();

app.MapGet("/", () => "Hello World!");

var crudGroup = app.MapGroup("/Crud").WithOpenApi();

    
    
crudGroup.MapPost("/Create/User", (AppDbContext db, User newUser) =>
{
    if (!ValidationHelper.ValidateModel(newUser, out var validationResults))
    {
        return Results.BadRequest(validationResults);
    }

    db.Users.Add(newUser);
    db.SaveChanges();
    return Results.Ok(newUser);
});
crudGroup.MapPost("/Create/Classroom", (AppDbContext db, Classroom newClassroom) =>
{
    if (!ValidationHelper.ValidateModel(newClassroom, out var validationResults))
    {
        return Results.BadRequest(validationResults);
    }
    
    db.Classrooms.Add(newClassroom);
    db.SaveChanges();
    return Results.Ok(newClassroom);
});



app.Run();