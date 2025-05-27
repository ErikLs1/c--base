using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Person : BaseEntityUser<AppUser>
{
    [Required]
    [MaxLength(50)]
    public string PersonFirstName { get; set; } = default!;
    
    [Required]
    [MaxLength(50)]
    public string PersonLastName { get; set; } = default!;
    
    [MaxLength(20)]
    public string PersonPhoneNumber { get; set; } = default!;
    
    [MaxLength(200)]
    public string PersonAddress { get; set; } = default!;
    
    [MaxLength(20)]
    public string PersonGender { get; set; } = default!;
    
    public DateOnly? PersonDateOfBirth { get; set; }
}