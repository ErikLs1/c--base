using Base.Domain;

namespace App.Domain;

public class TaskGrade : BaseEntity
{
    public Guid TeacherId { get; set; }
    public Person? Teacher { get; set; }

    public Guid TaskId { get; set; }
    public Task? Task { get; set; }
    
    public Guid EnrollmentId { get; set; }
    public Enrollment? Enrollment { get; set; }

    public decimal TaskGradeValue { get; set; }
    public bool Submitted { get; set; }
    public DateTime GradedAt { get; set; }
}