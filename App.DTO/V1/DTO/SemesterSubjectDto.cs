namespace App.DTO.V1.DTO;

public class SemesterSubjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description{ get; set; }
    public int Eap { get; set; }
    public bool Assigned { get; set; }
}