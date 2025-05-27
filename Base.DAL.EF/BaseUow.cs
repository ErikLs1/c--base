using Base.DAL.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class BaseUow<TDbContext> : IBaseUow
    where TDbContext : DbContext
{
    protected readonly TDbContext UowDbContext;

    public BaseUow(TDbContext context)
    {
        UowDbContext = context;
    }
    
    public async Task<int> SaveChangesAsync()
    {
        return await UowDbContext.SaveChangesAsync();
    }
}