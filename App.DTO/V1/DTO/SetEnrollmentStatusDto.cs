namespace App.DTO.V1.DTO;

public class SetEnrollmentStatusDto
{
    public Guid  EnrollmentId { get; set; }
    public bool  Accepted { get; set; }
}