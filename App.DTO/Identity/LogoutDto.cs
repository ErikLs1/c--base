using System.ComponentModel.DataAnnotations;

namespace App.DTO.Identity;

public class LogoutDto
{
    [MaxLength(128)]
    [Required]
    public string RefreshToken { get; set; } = default!;
}