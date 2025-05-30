using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class School : BaseEntity
{
    [Required, MaxLength(100)]
    public string SchoolName { get; set; } = default!;
    
    [MaxLength(255)]
    public string SchoolAddress { get; set; } = default!;
    public ICollection<Person>? Persons { get; set; }
    public ICollection<Subject>? Subjects { get; set; }
    public ICollection<SemesterSubject>? SemesterSubjects { get; set; }
    public ICollection<Enrollment>? Enrollments { get; set; }
}