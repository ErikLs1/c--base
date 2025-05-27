using App.DAL.Contracts;
using App.DAL.EF.Repositories;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUow : BaseUow<AppDbContext>, IAppUow
{
    public AppUow(AppDbContext uowDbContext) : base(uowDbContext)
    {
    }
    
    private IPersonRepository? _personRepository;
    public IPersonRepository PersonRepository =>
        _personRepository ??= new PersonRepository(UowDbContext);
    
    private IRefreshTokenRepository? _refreshTokenRepository;
    public IRefreshTokenRepository RefreshTokenRepository =>
        _refreshTokenRepository ??= new RefreshTokenRepository(UowDbContext);
}