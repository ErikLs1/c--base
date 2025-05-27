using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IRefreshTokenRepository :  IBaseRepository<RefreshTokenDalDto>
{
    Task<int> DeleteExpiredTokenAsync(Guid userId);
    
    Task<IList<RefreshTokenDalDto>> FindByTokenAsync(Guid userId, string token);

    Task<int> DeleteByTokenAsync(Guid userId, string token);
}