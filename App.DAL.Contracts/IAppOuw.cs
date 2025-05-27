using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IAppUow : IBaseUow
{
    IPersonRepository PersonRepository { get; }
    IRefreshTokenRepository RefreshTokenRepository { get; }
}