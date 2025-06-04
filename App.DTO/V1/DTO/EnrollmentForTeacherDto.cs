namespace App.DTO.V1.DTO;

public class EnrollmentForTeacherDto
{
    public Guid EnrollmentId { get; set; }
    public string SubjectName { get; set; } = default!;
    public Guid StudentId { get; set; }
    public string StudentName { get; set; } = default!;
    public DateTime EnrollmentDate{ get; set; }
    public string Status { get; set; } = default!;
    public bool StudentAccepted { get; set; }
}