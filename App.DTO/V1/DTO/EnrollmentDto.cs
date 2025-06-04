namespace App.DTO.V1.DTO;

public class EnrollmentDto
{
    public Guid Id { get; set; }
    public string SubjectName { get; set; } = default!;
    public string SubjectCode { get; set; } = default!;
    public DateTime EnrollmentDate{ get; set; }
    public string Status { get; set; } = default!;
    public bool StudentAccepted { get; set; }
}