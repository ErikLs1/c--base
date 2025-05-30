using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Subject : BaseEntity
{
    public Guid SchoolId { get; set; }
    public School? School { get; set; }
    
    [Required, MaxLength(100)]
    public string SubjectName { get; set; } = default!;
    
    [Required, MaxLength(50)]
    public string SubjectCode { get; set; } = default!;
    public int Eap { get; set; }
    public string? SubjectDescription { get; set; } = default!;
}