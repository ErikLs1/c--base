using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Task : BaseEntity
{
    public Guid SemesterSubjectId { get; set; }
    public SemesterSubject? SemesterSubject { get; set; }
    
    [Required, MaxLength(100)]
    public string TaskName { get; set; } = default!;
    
    public int MaxPoints { get; set; }
    
    [MaxLength(1000)]
    public string? Description { get; set; }
    
    public ICollection<TaskGrade>? TaskGrades { get; set; }
}