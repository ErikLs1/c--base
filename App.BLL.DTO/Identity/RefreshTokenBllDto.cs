namespace App.BLL.DTO.Identity;

public class RefreshTokenBllDto
{
    public string Jwt { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}