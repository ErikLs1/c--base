using App.BLL.DTO.Identity;
using App.DTO.Identity;

namespace App.DTO.V1.Mappers;

public class AccountMapper
{
    public RegisterBllDto? Map(RegisterDto? dto)
    {
        if (dto == null) return null;

        return new RegisterBllDto()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Password = dto.Password,
            Address = dto.Address,
            PhoneNumber = dto.PhoneNumber,
            Gender = dto.Gender,
            DateOfBirth = dto.DateOfBirth
        };
    }
    
    public JwtResponseBllDto? Map(JwtResponseDto? dto)
    {
        if (dto == null) return null;

        return new JwtResponseBllDto()
        {
            Jwt = dto.Jwt,
            RefreshToken = dto.RefreshToken,
            Role = dto.Role
        };
    }
    
    public JwtResponseDto? Map(JwtResponseBllDto? dto)
    {
        if (dto == null) return null;

        return new JwtResponseDto()
        {
            Jwt = dto.Jwt,
            RefreshToken = dto.RefreshToken,
            Role = dto.Role
        };
    }
    
    public LoginBllDto? Map(LoginDto? dto)
    {
        if (dto == null) return null;

        return new LoginBllDto()
        {
            Email = dto.Email,
            Password = dto.Password
        };
    }
    
    public RefreshTokenBllDto? Map(RefreshTokenDto? dto)
    {
        if (dto == null) return null;

        return new RefreshTokenBllDto()
        {
            Jwt = dto.Jwt,
            RefreshToken = dto.RefreshToken
        };
    }
}