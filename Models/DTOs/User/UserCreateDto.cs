using System.ComponentModel.DataAnnotations;
using AttendanceManagementApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagementApi.Models.DTOs;

[Index(nameof(EmailAdress), IsUnique = true)]
[Index(nameof(PhoneNumber), IsUnique = true)]
public class UserCreateDto
{
    [MaxLength(50),Required]
    public string FirstName { get; set; }
    [MaxLength(50),Required]
    public string LastName { get; set; }
    [MaxLength(50),Required]
    public string EmailAdress { get; set; }
    [MaxLength(50),Required]
    public string PhoneNumber { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; } = DateTime.Now;
    public Role Role { get; set; } = Role.Student;
}