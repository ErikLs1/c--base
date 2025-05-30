using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Person : BaseEntityUser<AppUser>
{
    public Guid? SchoolId { get; set; }
    public School? School { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string PersonFirstName { get; set; } = default!;
    
    [Required]
    [MaxLength(50)]
    public string PersonLastName { get; set; } = default!;
    
    [MaxLength(20)]
    public string PersonPhoneNumber { get; set; } = default!;
    
    [MaxLength(200)]
    public string PersonAddress { get; set; } = default!;
    
    [MaxLength(20)]
    public string PersonGender { get; set; } = default!;
    
    public DateOnly? PersonDateOfBirth { get; set; }
    
    public ICollection<SemesterSubject>? SemesterSubjects { get; set; }
    public ICollection<Enrollment>? Enrollments { get; set; }
    public ICollection<TaskGrade>? TaskGrades { get; set; }
}