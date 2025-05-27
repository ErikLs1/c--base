namespace App.BLL.DTO.Identity;

public class JwtResponseBllDto
{
    public string Jwt { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
    public string Role { get; set; } = default!;
}