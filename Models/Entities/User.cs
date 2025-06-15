using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagementApi.Models.Entities;

[Index(nameof(EmailAddress), IsUnique = true)]
public class User
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; }
    [MaxLength(100)]
    public string LastName { get; set; }
    [MaxLength(100)]
    public string EmailAddress { get; set; }
    [MaxLength(100)]
    public string PhoneNumber { get; set; }
    public Role Role { get; set; } = Role.Student;
    public ICollection<Classroom> Classrooms { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; } = DateTime.Now;
    
    public string GetFullName() => $"{FirstName} {LastName}";
}

public enum Role
{
    Student,
    Teacher,
}

// roller ayrı class olabilir
// böyle yaparsak rolleri api üzerinden yönetebiliriz

// roller enum olabilir
// eğer enum olursa kod içinde okuması nispeten daha kolaylaşır
// aynı zaman yeni rol eklemek istersek projeyi düzenlememiz gerekir. yani ekleyip, build alcaz

// rol kullanmayıp person'ı baz alarak, öğrenci ve öğretmen sınıfları oluşturabiliriz
// 1 hiç rol ile uğraşmak istemiyoruz
// 2 yoklama içine giren kaydın öğretmen olup olmadığını paranoya haline getirdik

/*
    1 kullanıcı endpointleri ekleme kullanıcılar rollü
    - öğrenci yönetimi (Crud)
    -id ile aradığımız kişi user olabilir ama öğrenci olmayabilir Dolayısıyla roller ile 
    öğretmenleride ekleyelim
    
    2 sınıf ve ders endpointlerini ekleyelim
    - dersi eklerken hangi sınıfta olaması gerektiğini de söylememiz lazım
    - SortOrder ile dersleri listelerken sıralamayı düzgün listeliyoruz
    - aynı zamanda dersleri sıraladığımız bir endpoıint olmalı -> dizi gönderip 
    hangi dersin hangi sırada olduğunu söyleceksiniz.    
    
    Bonus: Scallar Ui üzerinden göster -> detay ekleyin -> hasanın sunumu
 */
