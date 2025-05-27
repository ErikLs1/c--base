using App.DAL.Contracts;
using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.Domain.Identity;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class RefreshTokenRepository :  BaseRepository<RefreshTokenDalDto, AppRefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(DbContext repositoryDbContext) : base(repositoryDbContext, new RefreshTokenUowMapper())
    {
    }

    public async Task<int> DeleteExpiredTokenAsync(Guid userId)
    {
        return await GetQuery(userId)
            .Where(t => t.UserId == userId && t.Expiration < DateTime.UtcNow)
            .ExecuteDeleteAsync();
    }

    public async Task<IList<RefreshTokenDalDto>> FindByTokenAsync(Guid userId, string token)
    {
        return await GetQuery(userId)
            .AsNoTracking()
            .Where(x => 
                (x.RefreshToken == token && x.Expiration > DateTime.UtcNow) ||
                (x.PreviousRefreshToken == token && x.PreviousExpiration > DateTime.UtcNow)
            )
            .Select(t => Mapper.Map(t)!)
            .ToListAsync();
    }

    public async Task<int> DeleteByTokenAsync(Guid userId, string token)
    {
        return await GetQuery(userId)
            .Where(x =>
                (x.RefreshToken == token) ||
                (x.PreviousRefreshToken == token))
            .ExecuteDeleteAsync();
    }
}