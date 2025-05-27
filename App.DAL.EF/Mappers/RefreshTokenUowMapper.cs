using App.DAL.DTO;
using App.Domain.Identity;
using Base.Contracts;

namespace App.DAL.EF.Mappers;

public class RefreshTokenUowMapper : IMapper<RefreshTokenDalDto, AppRefreshToken>
{
    public RefreshTokenDalDto? Map(AppRefreshToken? entity)
    {
        if (entity == null) return null;

        return new RefreshTokenDalDto()
        {
            Id = entity.Id,
            UserId = entity.UserId,
            RefreshToken = entity.RefreshToken,
            Expiration = entity.Expiration,
            PreviousRefreshToken = entity.PreviousRefreshToken,
            PreviousExpiration = entity.PreviousExpiration
        };
    }

    public AppRefreshToken? Map(RefreshTokenDalDto? dto)
    {
        if (dto == null) return null;

        return new AppRefreshToken()
        {
            Id = dto.Id,
            UserId = dto.UserId,
            RefreshToken = dto.RefreshToken!,
            Expiration = dto.Expiration,
            PreviousRefreshToken = dto.PreviousRefreshToken,
            PreviousExpiration = dto.PreviousExpiration
        };
    }
}