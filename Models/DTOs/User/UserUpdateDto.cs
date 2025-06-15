using System.ComponentModel.DataAnnotations;
using AttendanceManagementApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApp.Models.DTOs;

[Index(nameof(Email), IsUnique = true)]
[Index(nameof(PhoneNumber), IsUnique = true)]
public class UserUpdateDto
{
    [MaxLength(50),Required]
    public string FirstName { get; set; }
    [MaxLength(50),Required]
    public string LastName { get; set; }
    [MaxLength(50),Required]
    public string Email { get; set; }
    [MaxLength(50),Required]
    public string PhoneNumber { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; } = DateTime.Now;
    public Role Role { get; set; } = Role.Student;
    
}