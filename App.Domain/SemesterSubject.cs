using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class SemesterSubject : BaseEntity
{
    public Guid? TeacherId { get; set; }
    public Person? Teacher { get; set; }
    
    public Guid SchoolId { get; set; }
    public School? School { get; set; }
    
    public Guid SemesterId { get; set; }
    public Semester? Semester { get; set; }
    
    public Guid SubjectId { get; set; }
    public Subject? Subject { get; set; }
    
    [MaxLength(50)]
    public string? Room { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate   { get; set; }
    public ICollection<Enrollment>? Enrollments { get; set; }
    public ICollection<Task>? Tasks { get; set; }
}