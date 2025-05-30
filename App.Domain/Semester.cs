using Base.Domain;

namespace App.Domain;

public class Semester : BaseEntity
{
    public string SemesterName { get; set; } = default!;
    public DateOnly SemesterYear { get; set; }
    public ICollection<SemesterSubject>? SemesterSubjects { get; set; } = default!;
}