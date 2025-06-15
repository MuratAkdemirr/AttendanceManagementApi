using AttendanceManagementApi.Data;
using AttendanceManagementApi.Models.DTOs;
using AttendanceManagementApi.Models.Entities;


namespace FirstWebApp.Methods;

public class UserEndPoint
{
    public static void UserEndPoints()
    {
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        var userGroup = app.MapGroup("/User").WithOpenApi();

        userGroup.MapPost("/", (UserCreateDto dto, AppDbContext context) =>
            {
                if (!ValidationHelper.ValidateModel(dto, out var validationResults))
                    
                    return Results.BadRequest();

                if (context.Users.Any(x => x.EmailAddress == dto.EmailAdress))
                {
                    return Results.BadRequest("Bu e-posta adresi ile bir kayıt mevcut.");
                }

                var user = new User
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    EmailAddress = dto.EmailAdress,
                    PhoneNumber = dto.PhoneNumber,
                    Role = dto.Role
                };
                context.Users.Add(user);
                context.SaveChanges();
                return Results.Created($"/User/{user.Id}", user);
            })
            .WithSummary("Kullanıcı Ekleme")
            .WithDescription("Veritabanına kullanıcı kayıt etme işlemi");


        userGroup.MapDelete("/{id:int}", (int id, AppDbContext context) =>
            {
                var user = context.Users.Find(id);
                if (!ValidationHelper.ValidateModel(id, out var validationResults))
                    
                    return Results.BadRequest();

                context.Users.Remove(user);
                context.SaveChanges();
                return Results.Ok("Kullanıcı silindi");
            })
            .WithSummary("Kullanıcı silme")
            .WithDescription("Veritabanından kullanıcı silme işlemi.");

        userGroup.MapGet("/Read/{id:int}", (AppDbContext db, int id) =>
            {
                var user = db.Users.Find(id);
                return user is not null ? Results.Ok(user) : Results.NotFound();
            })
            .WithSummary("Kullanıcı görme")
            .WithDescription("Veritabanında kayıtlı kullanıcıyı id kullanarak okuma.");
        
    }
    
}