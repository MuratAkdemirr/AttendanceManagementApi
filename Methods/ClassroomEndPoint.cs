using AttendanceManagementApi.Data;
using AttendanceManagementApi.Models.Entities;
using FirstWebApp.Models.DTOs.Classroom;

namespace FirstWebApp.Methods;

public class ClassroomEndPoint
{
    public static void ClassroomEndPoints()
    {
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        var classroomGroup = app.MapGroup("/Classroom").WithOpenApi();

        classroomGroup.MapPost("/", (ClassroomCreateDto dto, AppDbContext context) =>
            {
                if (!ValidationHelper.ValidateModel(dto, out var validationResults))
                    return Results.BadRequest();

                if (context.Classrooms.Any(x => x.Name == dto.Name))
                {
                    return Results.BadRequest("Bu isimde sınıf mevcut");
                }

                var classroom = new Classroom
                {
                    Name = dto.Name,
                };
                context.Classrooms.Add(classroom);
                context.SaveChanges();
                return Results.Created($"/Classroom/{classroom.Id}", classroom);
            })
            .WithDescription("Sınıf ekleme işlemi")
            .WithDescription("Veritabanına sınıf kayıt etme işlemi");

        classroomGroup.MapDelete("/{id:int}", (int id, AppDbContext context) =>
        {
            var classroom = context.Classrooms.Find(id);
            if (classroom == null)
            {
                return Results.NotFound("Sınıf Bulunamadı.");
            }

            context.Classrooms.Remove(classroom);
            context.SaveChanges();
            return Results.Ok("Sınıf silindi.");

        });
    }
}