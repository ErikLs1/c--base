using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Enrollment : BaseEntity
{
    public Guid StudentId { get; set; }
    public Person? Student { get; set; }
    
    public Guid SchoolId { get; set; }
    public School? School { get; set; }
    
    public Guid SemesterSubjectId { get; set; }
    public SemesterSubject? SemesterSubject { get; set; }
    
    public decimal? FinalGrade { get; set; }
    
    [MaxLength(20)]
    public string? Status { get; set; }
    
    public DateTime EnrollmentDate { get; set; }
    public bool StudentAccepted { get; set; } = false;
    
    public ICollection<TaskGrade>? TaskGrades { get; set; }
}